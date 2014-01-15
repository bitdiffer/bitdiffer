using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Configuration;
using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Controls
{
	public partial class ReferencePaths : UserControl
	{
        public ReferencePaths()
		{
			InitializeComponent();
		}

		internal void LoadOptions(DiffConfig config)
		{
            if (string.IsNullOrEmpty(config.ReferenceDirectories))
            {
                gridFolders.Items = new List<string>();
            }
            else
            {
                gridFolders.Items = new List<string>(config.ReferenceDirectories.Split(';'));
            }
		}

        internal void SaveOptions(DiffConfig config)
		{
            config.ReferenceDirectories = string.Join(";", gridFolders.Items);
        }
	}
}
