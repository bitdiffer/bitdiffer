namespace BitDiffer.Client.Controls
{
	partial class Configuration
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
			this.label1 = new System.Windows.Forms.Label();
			this.cbIsolationLevel = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbContext = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cbResolvePref = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbThreading = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(381, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "AppDomain Isolation Level:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbIsolationLevel
			// 
			this.cbIsolationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIsolationLevel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cbIsolationLevel.FormattingEnabled = true;
			this.cbIsolationLevel.Items.AddRange(new object[] {
            "Automatic",
            "High",
            "Medium"});
			this.cbIsolationLevel.Location = new System.Drawing.Point(227, 38);
			this.cbIsolationLevel.Name = "cbIsolationLevel";
			this.cbIsolationLevel.Size = new System.Drawing.Size(166, 22);
			this.cbIsolationLevel.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 77);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(623, 87);
			this.label2.TabIndex = 2;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 205);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(623, 70);
			this.label3.TabIndex = 5;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// cbContext
			// 
			this.cbContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbContext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cbContext.FormattingEnabled = true;
			this.cbContext.Items.AddRange(new object[] {
            "Reflection",
            "Execution"});
			this.cbContext.Location = new System.Drawing.Point(227, 166);
			this.cbContext.Name = "cbContext";
			this.cbContext.Size = new System.Drawing.Size(166, 22);
			this.cbContext.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(381, 24);
			this.label4.TabIndex = 3;
			this.label4.Text = "Assembly Load Context:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 320);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(623, 45);
			this.label5.TabIndex = 8;
			this.label5.Text = "Determines the order in which BitDiffer will attempt to resolve referenced assemb" +
				"lies; select which location to search first when a reference must be resolved.";
			// 
			// cbResolvePref
			// 
			this.cbResolvePref.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbResolvePref.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cbResolvePref.FormattingEnabled = true;
			this.cbResolvePref.Items.AddRange(new object[] {
            "Prefer Local Folder",
            "Prefer GAC"});
			this.cbResolvePref.Location = new System.Drawing.Point(227, 282);
			this.cbResolvePref.Name = "cbResolvePref";
			this.cbResolvePref.Size = new System.Drawing.Size(166, 22);
			this.cbResolvePref.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 281);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(381, 24);
			this.label6.TabIndex = 6;
			this.label6.Text = "Assembly Resolution Order:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 406);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(623, 55);
			this.label7.TabIndex = 11;
			this.label7.Text = resources.GetString("label7.Text");
			// 
			// cbThreading
			// 
			this.cbThreading.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbThreading.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cbThreading.FormattingEnabled = true;
			this.cbThreading.Items.AddRange(new object[] {
            "Multithread",
            "Single Thread"});
			this.cbThreading.Location = new System.Drawing.Point(227, 367);
			this.cbThreading.Name = "cbThreading";
			this.cbThreading.Size = new System.Drawing.Size(166, 22);
			this.cbThreading.TabIndex = 10;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 366);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(381, 24);
			this.label8.TabIndex = 9;
			this.label8.Text = "Threading Model:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(12, 12);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(458, 14);
			this.label9.TabIndex = 12;
			this.label9.Text = "Configure low-level AppDomain and Assembly load, resolve, and threading options.";
			// 
			// CompareOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label9);
			this.Controls.Add(this.cbIsolationLevel);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.cbThreading);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbResolvePref);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbContext);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CompareOptions";
			this.Size = new System.Drawing.Size(692, 468);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbIsolationLevel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbContext;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbResolvePref;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbThreading;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;

	}
}
