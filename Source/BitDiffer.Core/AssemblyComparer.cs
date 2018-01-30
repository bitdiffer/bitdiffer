using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.IO;
using System.Threading;
using BitDiffer.Common.Exceptions;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Core
{
    public class AssemblyComparer
    {
        private DiffConfig _config;
        private ComparisonFilter _filter;
        private IHandleProgress _progress;
        private ThreadPoolWait _tpw = new ThreadPoolWait();

        public AssemblyComparer()
            : this(null)
        {
        }

        public AssemblyComparer(IHandleProgress progress)
        {
            _progress = progress;
        }

        public AssemblyComparison CompareAssemblies(ComparisonSet set)
        {
            if (set.Mode == ComparisonMode.CompareFiles)
            {
                return CompareAssemblyFiles(set.Config, set.Filter, set.Items.ToArray());
            }
            else
            {
                return CompareAssemblyDirectories(set.RecurseSubdirectories, set.Config, set.Filter, set.Items.ToArray());
            }
        }

        public AssemblyComparison CompareAssemblyFiles(params string[] assemblyFiles)
        {
            return CompareAssemblyFiles(DiffConfig.Default, ComparisonFilter.Default, assemblyFiles);
        }

        public AssemblyComparison CompareAssemblyFiles(DiffConfig config, ComparisonFilter filter, params string[] assemblyFiles)
        {
            _config = config;
            _filter = filter;

            if (_progress != null)
            {
                _progress.SetMaxRange(assemblyFiles.Length);
            }

            if (assemblyFiles.Length == 0)
            {
                return null;
            }

            AppDomainIsolationLevel level = _config.IsolationLevel;
            if (level == AppDomainIsolationLevel.AutoDetect)
            {
                level = IsolationDetector.AutoDetectIsolationLevelFiles(assemblyFiles);
            }

            AssemblyManager manager = AssemblyManagerFactory.Create(level);
            AssemblyComparison ac = new AssemblyComparison();
            ac.Groups.Add(DoCompareFiles(manager, assemblyFiles));

            manager.AllExtractionsComplete();

            FilterResultSet(ac, filter);

            return ac;
        }

        public AssemblyComparison CompareAssemblyDirectories(bool recurse, DiffConfig config, ComparisonFilter filter, params string[] assemblyDirectories)
        {
            _config = config;
            _filter = filter;

            int totalFiles = 0;
            List<List<ICanAlign>> allEntries = new List<List<ICanAlign>>();

            SearchOption option = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            foreach (string dir in assemblyDirectories)
            {
                List<ICanAlign> entry = new List<ICanAlign>();

                entry.AddRange(AssemblyDirectoryEntry.From(dir, Directory.GetFiles(dir, "*.dll", option)));
                entry.AddRange(AssemblyDirectoryEntry.From(dir, Directory.GetFiles(dir, "*.exe", option)));

                totalFiles += entry.Count;
                allEntries.Add(entry);
            }

            if (allEntries.Count == 0)
            {
                return null;
            }

            AppDomainIsolationLevel level = _config.IsolationLevel;
            if (level == AppDomainIsolationLevel.AutoDetect)
            {
                level = IsolationDetector.AutoDetectIsolationLevelDirs(assemblyDirectories);
            }

            ListOperations.AlignListsNoParent(allEntries.ToArray());

            AssemblyManager manager = AssemblyManagerFactory.Create(level);

            AssemblyComparison ac = new AssemblyComparison();

            if (_progress != null)
            {
                _progress.SetMaxRange(totalFiles);
            }

            for (int j = 0; j < allEntries[0].Count; j++)
            {
                List<string> thisRun = new List<string>();

                for (int i = 0; i < allEntries.Count; i++)
                {
                    if (allEntries[i][j].Status == Status.Present)
                    {
                        thisRun.Add(((AssemblyDirectoryEntry)allEntries[i][j]).Path);
                    }
                }

                ac.Groups.Add(DoCompareFiles(manager, thisRun.ToArray()));
            }

            manager.AllExtractionsComplete();

            FilterResultSet(ac, filter);

            return ac;
        }

        private void FilterResultSet(AssemblyComparison ac, ComparisonFilter filter)
        {
            if (ac == null || filter == null)
            {
                return;
            }

            if (filter.ChangedItemsOnly)
            {
                var removeList = ac.Groups.ToList().Where(g => g.Change == ChangeType.None && !g.HasErrors);
                removeList.ToList().ForEach(i => ac.Groups.Remove(i));
            }

        }

        private AssemblyGroup DoCompareFiles(AssemblyManager manager, string[] assemblyFiles)
        {
            string[] assemblyFilesResolved = new string[assemblyFiles.Length];

            for (int i = 0; i < assemblyFiles.Length; i++)
            {
                if (Path.IsPathRooted(assemblyFiles[i]))
                {
                    assemblyFilesResolved[i] = Path.GetFullPath(assemblyFiles[i]);
                }
                else
                {
                    assemblyFilesResolved[i] = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), assemblyFiles[i]));
                }
            }

            AssemblyGroup group = new AssemblyGroup();

            foreach (string file in assemblyFilesResolved)
            {
                if (group.Name == null)
                {
                    group.Name = Path.GetFileName(file);
                }

                if (Path.GetFileName(file) != group.Name)
                {
                    group.Name = "(Multiple Names)";
                    break;
                }
            }

            foreach (string fileName in assemblyFilesResolved)
            {
                AssemblyComparerThread act = new AssemblyComparerThread(manager, group, fileName, _progress);

                if (_config.Multithread)
                {
                    _tpw.QueueUserWorkItem(Execute, act);
                }
                else
                {
                    Execute(act);
                }
            }

            if (_config.Multithread)
            {
                _tpw.WaitOne();

                List<AssemblyDetail> sorted = new List<AssemblyDetail>();

                foreach (string fileName in assemblyFilesResolved)
                {
                    AssemblyDetail sortedDetail = group.Assemblies.Find(delegate(AssemblyDetail detail) { return string.Compare(detail.Location, fileName, true) == 0; });

                    if (sortedDetail == null)
                    {
                        Log.Error("Unable to sort assembly " + fileName);
                        //Log.Error(" List has " + string.Join(",", group.Assemblies.ConvertAll(delegate(AssemblyDetail detail) { return detail.Location; }).ToArray()));
                    }
                    else
                    {
                        sorted.Add(sortedDetail);
                    }
                }

                group.Assemblies.Clear();
                group.Assemblies.AddRange(sorted);
            }

            ListOperations.AlignLists(group.Assemblies);

            group.PerformCompare(_filter);

            return group;
        }

        internal void Execute(object state)
        {
            AssemblyComparerThread act = (AssemblyComparerThread)state;

            try
            {
                if (act.Progress != null)
                {
                    if (act.Progress.CancelRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    act.Progress.UpdateProgress(new ProgressStatus("Analyzing " + act.FileName, true));
                }

                AssemblyDetail assembly = act.Manager.ExtractAssemblyInf(act.FileName, _config);
                assembly.Name = act.Group.Name;

                lock (GetType())
                {
                    act.Group.Assemblies.Add(assembly);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to load assembly : {0}", act.FileName);
                act.Group.HasErrors = true;
                act.Group.ErrorDetail = ex.GetNestedExceptionMessage();
                Log.Error(act.Group.ErrorDetail);
            }
        }

    }
}
