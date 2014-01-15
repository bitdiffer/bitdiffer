using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Configuration;

namespace BitDiffer.Client.Controls
{
	public partial class CompareViewFilter : UserControl
	{
		public CompareViewFilter()
		{
			InitializeComponent();
		}

		internal void LoadFilter(ComparisonFilter cf)
		{
			cbChangedOnly.Checked = cf.ChangedItemsOnly;
			cbPublic.Checked = cf.IncludePublic;
            cbProtected.Checked = cf.IncludeProtected;
            cbInternal.Checked = cf.IncludeInternal;
            cbPrivate.Checked = cf.IncludePrivate;
            cbIgnoreAssemAttrs.Checked = cf.IgnoreAssemblyAttributeChanges;
			cbCompareImplementation.Checked = cf.CompareMethodImplementations;
		}

		internal void SaveFilter(ComparisonFilter cf)
		{
			cf.ChangedItemsOnly = cbChangedOnly.Checked;
			cf.IncludePublic = cbPublic.Checked;
            cf.IncludeProtected = cbProtected.Checked;
            cf.IncludeInternal = cbInternal.Checked;
            cf.IncludePrivate = cbPrivate.Checked;
            cf.IgnoreAssemblyAttributeChanges = cbIgnoreAssemAttrs.Checked;
			cf.CompareMethodImplementations = cbCompareImplementation.Checked;
		}
	}
}
