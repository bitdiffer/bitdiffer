using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Controls
{
	public partial class SelectAssemblies : UserControl
	{
		public SelectAssemblies()
		{
			InitializeComponent();
		}

		internal void LoadSet(ComparisonSet set)
		{
			cbMode.SelectedIndex = (int)set.Mode;
            cbRecurse.Checked = set.RecurseSubdirectories;

			if (cbMode.SelectedIndex == 0)
			{
				gridFiles.Items = set.Items;
			}
			else
			{
				gridFolders.Items = set.Items;
				gridFolders.BringToFront();
			}
		}

		internal void SaveSet(ComparisonSet set)
		{
			set.Mode = (ComparisonMode)cbMode.SelectedIndex;

			if (cbMode.SelectedIndex == 0)
			{
				set.Items = gridFiles.Items;
                set.RecurseSubdirectories = false;
			}
			else
			{
				set.Items = gridFolders.Items;
                set.RecurseSubdirectories = cbRecurse.Checked;
			}
		}

		private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbMode.SelectedIndex == 0)
			{
				lbDescription.Text = "This mode allows you to select specific assemblies and compare them. This is the ideal way to compare and detect changes to different versions of any particular assembly that you are interested in.";
				gridFiles.BringToFront();
                cbRecurse.Hide();
			}
			else
			{
				lbDescription.Text = "This mode allows you to select directories, and then compare all the assemblies in the directories you selected. Assemblies with the same names will be matched together for comparison and the results will then be presented side by side. This is the ideal way to compare entires builds of a product.";
				gridFolders.BringToFront();
                cbRecurse.Show();
            }
		}
	}
}
