using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing;

namespace BitDiffer.Client.Controls
{
	public partial class WindowStatePersister : Component, ISupportInitialize
	{
		private Form _parentForm;
		PersistedWindowStateSettings _pws = new PersistedWindowStateSettings();

		public WindowStatePersister()
		{
			InitializeComponent();
		}

		public WindowStatePersister(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public Form ParentForm
		{
			get { return _parentForm; }
			set { _parentForm = value; }
		}

		public override ISite Site
		{
			set
			{
				base.Site = value;

				if (value != null)
				{
					IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;

					if (host != null)
					{
						IComponent root = host.RootComponent;

						if (root is Form)
						{
							_parentForm = (Form)host.RootComponent;
						}
						else
						{
							throw new InvalidOperationException("This component can only be used on Forms");
						}
					}
				}
			}
		}

		public void BeginInit()
		{
		}

		public void EndInit()
		{
			if (_parentForm != null)
			{
				if (_pws.CallUpgrade)
				{
					_pws.Upgrade();
					_pws.CallUpgrade = false;
					_pws.Save();
				}

				if (_pws.Bounds != Rectangle.Empty)
				{
					_parentForm.Bounds = _pws.Bounds;
					_parentForm.StartPosition = FormStartPosition.Manual;
				}

				_parentForm.WindowState = _pws.WindowState;

				ValidateFormLocation();

				_parentForm.SizeChanged += new EventHandler(form_SizeChanged);
				_parentForm.Move += new EventHandler(form_Moved);
				_parentForm.FormClosing += new FormClosingEventHandler(form_Closing);
			}
		}

		void form_SizeChanged(object sender, EventArgs e)
		{
			_pws.WindowState = _parentForm.WindowState;

			if (_parentForm.WindowState == FormWindowState.Normal)
			{
				_pws.Bounds = _parentForm.Bounds;
			}
		}

		void form_Moved(object sender, EventArgs e)
		{
			if (_parentForm.WindowState == FormWindowState.Normal)
			{
				_pws.Bounds = _parentForm.Bounds;
			}
		}

		void form_Closing(object sender, FormClosingEventArgs e)
		{
			_pws.Save();
		}

		private void ValidateFormLocation()
		{
			Rectangle bounds = bounds = Screen.GetWorkingArea(_parentForm);

			if (_parentForm.Top < bounds.Top)
			{
				_parentForm.Top = bounds.Top;
			}

			if (_parentForm.Top + 40 > bounds.Bottom)
			{
				_parentForm.Top = bounds.Bottom - 40;
			}

			if (_parentForm.Right - 80 < bounds.Left)
			{
				_parentForm.Left = bounds.Left - _parentForm.Width + 80;
			}

			if (_parentForm.Left + 60 > bounds.Right)
			{
				_parentForm.Left = bounds.Right - 60;
			}
		}
	}

	public class PersistedWindowStateSettings : ApplicationSettingsBase
	{
		[UserScopedSetting]
		[DefaultSettingValue("True")]
		public bool CallUpgrade
		{
			get { return (bool)this["CallUpgrade"]; }
			set { this["CallUpgrade"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("Normal")]
		public FormWindowState WindowState
		{
			get { return (FormWindowState)this["WindowState"]; }
			set { this["WindowState"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("0,0,0,0")]
		public Rectangle Bounds
		{
			get { return (Rectangle)this["Bounds"]; }
			set { this["Bounds"] = value; }
		}
	}
}
