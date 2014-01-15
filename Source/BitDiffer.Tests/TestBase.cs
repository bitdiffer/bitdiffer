using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	public class TestBase
	{
		private static Dictionary<string, AssemblyDetail> _infCache = new Dictionary<string, AssemblyDetail>();

		protected AssemblyDetail ExtractAll(string assemblyFile)
		{
			return ExtractAll(assemblyFile, DiffConfig.Default);
		}

		protected AssemblyDetail ExtractAll(string assemblyFile, DiffConfig config)
		{
			string cacheKey = assemblyFile + "|" + config.ToString();

			if (config.IsolationLevel == AppDomainIsolationLevel.AutoDetect)
			{
				config.IsolationLevel = AppDomainIsolationLevel.Medium;
			}

			if (!_infCache.ContainsKey(cacheKey))
			{
				lock (typeof(TestBase))
				{
					if (!_infCache.ContainsKey(cacheKey))
					{
						if (!File.Exists(assemblyFile))
						{
							Assert.Inconclusive("Unable to locate subject assembly");
						}

						AssemblyManager am = AssemblyManagerFactory.Create(config.IsolationLevel);
						AssemblyDetail inf = am.ExtractAssemblyInf(assemblyFile, config);

						_infCache.Add(cacheKey, inf);
					}
				}
			}

			return (AssemblyDetail)_infCache[cacheKey].Clone();
		}

		protected AttributeDetail ExtractAttribute(string assemblyFile, string typeName)
		{
			return ExtractAttribute(assemblyFile, typeName, DiffConfig.Default);
		}

		protected AttributeDetail ExtractAttribute(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<AttributeDetail>(assemblyFile, "Attributes", typeName, config);
		}

		protected EnumDetail ExtractEnum(string assemblyFile, string typeName)
		{
			return ExtractEnum(assemblyFile, typeName, DiffConfig.Default);
		}

		protected EnumDetail ExtractEnum(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<EnumDetail>(assemblyFile, Subjects.NamespaceOne, typeName, config);
		}

		protected ReferenceDetail ExtractReference(string assemblyFile, string typeName)
		{
			return ExtractReference(assemblyFile, typeName, DiffConfig.Default);
		}

		protected ReferenceDetail ExtractReference(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<ReferenceDetail>(assemblyFile, "References", typeName, config);
		}

		protected ResourceDetail ExtractResource(string assemblyFile, string typeName)
		{
			return ExtractResource(assemblyFile, typeName, DiffConfig.Default);
		}

		protected ResourceDetail ExtractResource(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<ResourceDetail>(assemblyFile, "Resources", typeName, config);
		}

		protected InterfaceDetail ExtractInterface(string assemblyFile, string typeName)
		{
			return ExtractInterface(assemblyFile, typeName, DiffConfig.Default);
		}

		protected InterfaceDetail ExtractInterface(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<InterfaceDetail>(assemblyFile, Subjects.NamespaceOne, typeName, config);
		}

		protected ClassDetail ExtractClass(string assemblyFile, string typeName)
		{
			return ExtractClass(assemblyFile, typeName, DiffConfig.Default);
		}

		protected ClassDetail ExtractClass(string assemblyFile, string typeName, DiffConfig config)
		{
			return ExtractItem<ClassDetail>(assemblyFile, Subjects.NamespaceOne, typeName, config);
		}

		protected NamespaceDetail ExtractNamespace(string assemblyFile, string namespaceName)
		{
			return ExtractNamespace(assemblyFile, namespaceName, DiffConfig.Default);
		}

		protected NamespaceDetail ExtractNamespace(string assemblyFile, string namespaceName, DiffConfig config)
		{
			return ExtractItem<NamespaceDetail>(assemblyFile, namespaceName, config);
		}

		protected MethodDetail ExtractMethod(string assemblyFile, string typeName, string methodName)
		{
			return ExtractMethod(assemblyFile, typeName, methodName, DiffConfig.Default);
		}

		protected MethodDetail ExtractMethod(string assemblyFile, string typeName, string methodName, DiffConfig config)
		{
			ClassDetail ci = ExtractClass(assemblyFile, typeName, config);

			MethodDetail value = ListOperations.FindOrReturnMissing(ci.FilterChildren<MethodDetail>(), methodName);
			Log.Verbose("Extracted method : {0}", value);
			return value;
		}

		protected MethodDetail ExtractOperator(string assemblyFile, string typeName, string OperatorName)
		{
			return ExtractOperator(assemblyFile, typeName, OperatorName, DiffConfig.Default);
		}

		protected MethodDetail ExtractOperator(string assemblyFile, string typeName, string OperatorName, DiffConfig config)
		{
			ClassDetail ci = ExtractClass(assemblyFile, typeName, config);

			MethodDetail value = ListOperations.FindOrReturnMissing(ci.FilterChildren<OperatorDetail>(), OperatorName);
			Log.Verbose("Extracted Operator : {0}", value);
			return value;
		}

		protected FieldDetail ExtractField(string assemblyFile, string typeName, string fieldName)
		{
			return ExtractField(assemblyFile, typeName, fieldName, DiffConfig.Default);
		}

		protected FieldDetail ExtractField(string assemblyFile, string typeName, string fieldName, DiffConfig config)
		{
			ClassDetail ci = ExtractClass(assemblyFile, typeName, config);

			FieldDetail value = ListOperations.FindOrReturnMissing(ci.FilterChildren<FieldDetail>(), fieldName);
			Log.Verbose("Extracted field : {0}", value);
			return value;
		}

		protected PropertyDetail ExtractProperty(string assemblyFile, string typeName, string propertyName)
		{
			return ExtractProperty(assemblyFile, typeName, propertyName, DiffConfig.Default);
		}

		protected PropertyDetail ExtractProperty(string assemblyFile, string typeName, string propertyName, DiffConfig config)
		{
			ClassDetail ci = ExtractClass(assemblyFile, typeName, config);

			PropertyDetail value = ListOperations.FindOrReturnMissing(ci.FilterChildren<PropertyDetail>(), propertyName);
			Log.Verbose("Extracted property : {0}", value);
			return value;
		}

		protected EventDetail ExtractEvent(string assemblyFile, string typeName, string eventName)
		{
			return ExtractEvent(assemblyFile, typeName, eventName, DiffConfig.Default);
		}

		protected EventDetail ExtractEvent(string assemblyFile, string typeName, string eventName, DiffConfig config)
		{
			ClassDetail ci = ExtractClass(assemblyFile, typeName, config);

			EventDetail value = ListOperations.FindOrReturnMissing(ci.FilterChildren<EventDetail>(), eventName);
			Log.Verbose("Extracted event : {0}", value);
			return value;
		}

		protected T ExtractItem<T>(string assemblyFile, string parentName, string typeName) where T : ICanAlign, new()
		{
			return ExtractItem<T>(assemblyFile, parentName, typeName, DiffConfig.Default);
		}

		protected T ExtractItem<T>(string assemblyFile, string parentName, string typeName, DiffConfig config) where T : ICanAlign, new()
		{
			AssemblyDetail all = ExtractAll(assemblyFile, config);

			foreach (ICanCompare parent in all.Children)
			{
				if (parent.Name == parentName)
				{
					T t = ListOperations.FindOrReturnMissing(parent.FilterChildren<T>(), typeName);
					Log.Verbose("Extracted Item {0} : {1}", t.GetType().Name, t.ToString());
					return t;
				}
			}

			Assert.Inconclusive("Specified parent {0} was not found", parentName);
			return default(T);
		}

		protected T ExtractItem<T>(string assemblyFile, string name, DiffConfig config) where T : ICanAlign, new()
		{
			AssemblyDetail all = ExtractAll(assemblyFile, config);

			T t = ListOperations.FindOrReturnMissing(all.FilterChildren<T>(), name);
			Log.Verbose("Extracted Item {0} : {1}", t.GetType().Name, t.ToString());
			return t;
		}

		protected void Align(RootDetail t1, RootDetail t2)
		{
			List<List<ICanAlign>> lists = new List<List<ICanAlign>>();
			lists.Add(new List<ICanAlign>());
			lists.Add(new List<ICanAlign>());

			lists[0].Add(t1);
			lists[1].Add(t2);

			ListOperations.AlignListsNoParent(lists.ToArray());
		}

		protected void CheckForAttribute(RootDetail rd)
		{
			IEnumerator<AttributeDetail> ie = rd.FilterChildren<AttributeDetail>().GetEnumerator();
			Assert.IsTrue(ie.MoveNext());
			Log.Verbose("An attribute extracted: {0}", ie.Current.ToString());
			Assert.IsFalse(ie.MoveNext());
		}
	}
}
