using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BitDiffer.Client.Properties;
using BitDiffer.Client.Models;
using System.IO;

namespace BitDiffer.Client.Controls
{
    public class FileSystemDataGridView : DataGridView
    {
        private FileSystemItemType _itemType;

        public FileSystemDataGridView()
        {
            if (Program.IsRuntime)
            {
                this.BackgroundColor = Color.White;
                this.ColumnHeadersVisible = false;
                this.RowHeadersVisible = false;
                this.AllowUserToResizeRows = false;
                this.AllowUserToResizeColumns = false;
				this.GridColor = Color.FromKnownColor(KnownColor.ControlLight);

                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn(false);
                imageColumn.Width = 20;
                imageColumn.ValuesAreIcons = false;
                imageColumn.Image = Resources.browse_document;
                imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn();
                textBoxColumn.Width = 50;
                textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                DataGridViewLinkColumn browseColumn = new DataGridViewLinkColumn();
                browseColumn.TrackVisitedState = false;
                browseColumn.Width = 65;
                browseColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                DataGridViewLinkColumn delColumn = new DataGridViewLinkColumn();
                delColumn.TrackVisitedState = false;
                delColumn.Width = 65;
                delColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                this.Columns.Add(imageColumn);
                this.Columns.Add(textBoxColumn);
                this.Columns.Add(browseColumn);
                this.Columns.Add(delColumn);

                this.Rows.Clear();

                this.CurrentCell = this[1, 0];
            }
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);

            this.Rows[e.RowIndex].SetValues((_itemType == FileSystemItemType.File) ? Resources.browse_document : Resources.browse_folder, "", "Add...", "");
        }

        protected override void OnRowValidated(DataGridViewCellEventArgs e)
        {
            base.OnRowValidated(e);

            if (e.RowIndex != this.NewRowIndex)
            {
                this[2, e.RowIndex].Value = "Edit";
                this[3, e.RowIndex].Value = "Delete";
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentClick(e);

            if (e.ColumnIndex == 3)
            {
                if (this.NewRowIndex != e.RowIndex)
                {
                    this.Rows.RemoveAt(e.RowIndex);
                }
            }
            else if (e.ColumnIndex == 2)
            {
                string value = "";

                if (_itemType == FileSystemItemType.File)
                {
                    OpenFileDialog fd = new OpenFileDialog();
                    fd.CheckFileExists = true;
                    fd.Filter = "Assembly Files (*.exe, *.dll)|*.exe;*.dll|All Files (*.*)|*.*";

					if (e.RowIndex == this.NewRowIndex)
					{
						string lastSelected = UserPrefs.LastSelectedAssemblyFolder;

						if (!string.IsNullOrEmpty(lastSelected))
						{
							fd.InitialDirectory = Path.GetDirectoryName(lastSelected);
						}
						else
						{
							fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
						}
					}
					else
                    {
                        fd.FileName = this[1, e.RowIndex].Value.ToString();
                    }

                    if (fd.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

					UserPrefs.LastSelectedAssemblyFolder = fd.FileName;
                    value = fd.FileName;
                }
                else
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Select Folder";
                    fbd.ShowNewFolderButton = false;

					if (e.RowIndex == this.NewRowIndex)
					{
						string lastSelected = UserPrefs.LastSelectedAssemblyFolder;

						if (!string.IsNullOrEmpty(lastSelected))
						{
							fbd.SelectedPath = Path.GetDirectoryName(lastSelected);
						}
					}
					else
                    {
                        fbd.SelectedPath = this[1, e.RowIndex].Value.ToString();
                    }

                    if (fbd.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

					UserPrefs.LastSelectedAssemblyFolder = fbd.SelectedPath;
                    value = fbd.SelectedPath;
                }

                if (e.RowIndex == this.NewRowIndex)
                {
                    AddItem(value);
                }
                else
                {
                    this[1, e.RowIndex].Value = value;
                }

				this.CurrentCell = this[1, e.RowIndex];
            }
        }

        private void AddItem(string value)
        {
            int index = this.Rows.Add();
            this.Rows[index].SetValues((_itemType == FileSystemItemType.File) ? Resources.browse_document : Resources.browse_folder, value, "Edit", "Delete");
        }

        public FileSystemItemType ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;

				if (Program.IsRuntime)
				{
					((DataGridViewImageColumn)Columns[0]).Image = (value == FileSystemItemType.File) ? Resources.browse_document : Resources.browse_folder;
					this.Rows.Clear();
					this.CurrentCell = this[1, 0];
				}
            }
        }

        public List<string> Items
        {
            get
            {
                List<string> values = new List<string>();

                for (int i=0; i<this.RowCount; i++)
                {
                    if (i != this.NewRowIndex)
                    {
                        values.Add(this[1, i].Value.ToString());
                    }
                }

                return values;
            }

            set
            {
                foreach (string item in value)
                {
                    AddItem(item);
                }
            }
        }
    }

    public enum FileSystemItemType
    {
        File,
        Folder
    }
}
