using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public class ClassBecomesInternal
	{

	}

	internal class ClassBecomesPublic
	{
	}

	public class ClassBecomesStatic
	{
	}

	public class ClassAddsAttribute
	{
	}

	[Description("ClassRemovesAttributeAndBecomesInternal")]
	public class ClassRemovesAttributeAndBecomesInternal
	{
	}

	public class ClassPublicMethodChanges
	{
		private int _f1;

		public event EventHandler StuffHappening;

		public int F1
		{
			get { return _f1; }
			set { _f1 = value; }
		}

		public string Method1()
		{
			if (StuffHappening != null)
			{
				StuffHappening(this, EventArgs.Empty);
			}

			return "M1";
		}

		public string Method2()
		{
			return "M2";
		}
	}

	public class ClassInternalMethodChanges
	{
		private int _f1;

		public event EventHandler StuffHappening;

		public int F1
		{
			get { return _f1; }
			set { _f1 = value; }
		}

		public string Method1()
		{
			if (StuffHappening != null)
			{
				StuffHappening(this, EventArgs.Empty);
			}

			return "M1";
		}

		internal string Method2()
		{
			return "M2";
		}
	}

	public class ClassPropertySetBecomesInternal
	{
		private int _f1;

		public event EventHandler StuffHappening;

		public int F1
		{
			get { return _f1; }
			set { _f1 = value; }
		}

		public string Method1()
		{
			if (StuffHappening != null)
			{
				StuffHappening(this, EventArgs.Empty);
			}

			return "M1";
		}

		public string Method2()
		{
			return "M2";
		}
	}

	public class ClassFieldAddsAttribute
	{
		private int _f1;

		public event EventHandler StuffHappening;

		public int F1
		{
			get { return _f1; }
			set { _f1 = value; }
		}

		public string Method1()
		{
			if (StuffHappening != null)
			{
				StuffHappening(this, EventArgs.Empty);
			}

			return "M1";
		}

		public string Method2()
		{
			return "M2";
		}
	}

	public class ClassNoChange
	{
		private int _f1;

		public event EventHandler StuffHappening;

		public int F1
		{
			get { return _f1; }
			set { _f1 = value; }
		}

		public string Method1()
		{
			if (StuffHappening != null)
			{
				StuffHappening(this, EventArgs.Empty);
			}

			return "M1";
		}

		public string Method2()
		{
			return "M2";
		}
	}
}
