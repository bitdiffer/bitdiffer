namespace BitDiffer.Client.Controls
{
	partial class CompareViewFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareViewFilter));
            this.label2 = new System.Windows.Forms.Label();
            this.cbPublic = new System.Windows.Forms.CheckBox();
            this.cbChangedOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIgnoreAssemAttrs = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCompareImplementation = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbProtected = new System.Windows.Forms.CheckBox();
            this.cbInternal = new System.Windows.Forms.CheckBox();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(623, 74);
            this.label2.TabIndex = 14;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // cbPublic
            // 
            this.cbPublic.Location = new System.Drawing.Point(16, 38);
            this.cbPublic.Name = "cbPublic";
            this.cbPublic.Size = new System.Drawing.Size(90, 26);
            this.cbPublic.TabIndex = 0;
            this.cbPublic.Text = "Public";
            this.cbPublic.UseVisualStyleBackColor = true;
            // 
            // cbChangedOnly
            // 
            this.cbChangedOnly.Location = new System.Drawing.Point(16, 148);
            this.cbChangedOnly.Name = "cbChangedOnly";
            this.cbChangedOnly.Size = new System.Drawing.Size(249, 26);
            this.cbChangedOnly.TabIndex = 4;
            this.cbChangedOnly.Text = "Changed Items Only";
            this.cbChangedOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(623, 74);
            this.label1.TabIndex = 17;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // cbIgnoreAssemAttrs
            // 
            this.cbIgnoreAssemAttrs.Location = new System.Drawing.Point(16, 341);
            this.cbIgnoreAssemAttrs.Name = "cbIgnoreAssemAttrs";
            this.cbIgnoreAssemAttrs.Size = new System.Drawing.Size(249, 26);
            this.cbIgnoreAssemAttrs.TabIndex = 6;
            this.cbIgnoreAssemAttrs.Text = "Ignore Assembly Attribute Changes";
            this.cbIgnoreAssemAttrs.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(623, 59);
            this.label3.TabIndex = 19;
            this.label3.Text = "Select this option to ignore changes in assembly attributes. This option may be u" +
                "seful if you do not want to be alerted to changes to assembly attributes (i.e., " +
                "assembly version and file version).";
            // 
            // cbCompareImplementation
            // 
            this.cbCompareImplementation.Location = new System.Drawing.Point(16, 258);
            this.cbCompareImplementation.Name = "cbCompareImplementation";
            this.cbCompareImplementation.Size = new System.Drawing.Size(249, 26);
            this.cbCompareImplementation.TabIndex = 5;
            this.cbCompareImplementation.Text = "Compare Method Implementations";
            this.cbCompareImplementation.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(623, 47);
            this.label4.TabIndex = 21;
            this.label4.Text = "Select this option to analyze and compare implementations of methods and property" +
                " accessors. Clear this option if you are not interested in changes in implementa" +
                "tion.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(468, 14);
            this.label5.TabIndex = 23;
            this.label5.Text = "Set View Preferences. These options can also be quickly accessed from the toolbar" +
                ".";
            // 
            // cbProtected
            // 
            this.cbProtected.Location = new System.Drawing.Point(128, 38);
            this.cbProtected.Name = "cbProtected";
            this.cbProtected.Size = new System.Drawing.Size(91, 26);
            this.cbProtected.TabIndex = 1;
            this.cbProtected.Text = "Protected";
            this.cbProtected.UseVisualStyleBackColor = true;
            // 
            // cbInternal
            // 
            this.cbInternal.Location = new System.Drawing.Point(262, 38);
            this.cbInternal.Name = "cbInternal";
            this.cbInternal.Size = new System.Drawing.Size(71, 26);
            this.cbInternal.TabIndex = 2;
            this.cbInternal.Text = "Internal";
            this.cbInternal.UseVisualStyleBackColor = true;
            // 
            // cbPrivate
            // 
            this.cbPrivate.Location = new System.Drawing.Point(376, 38);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(83, 26);
            this.cbPrivate.TabIndex = 3;
            this.cbPrivate.Text = "Private";
            this.cbPrivate.UseVisualStyleBackColor = true;
            // 
            // CompareViewFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbPrivate);
            this.Controls.Add(this.cbInternal);
            this.Controls.Add(this.cbProtected);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCompareImplementation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbIgnoreAssemAttrs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbChangedOnly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPublic);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CompareViewFilter";
            this.Size = new System.Drawing.Size(692, 468);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox cbPublic;
		private System.Windows.Forms.CheckBox cbChangedOnly;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbIgnoreAssemAttrs;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox cbCompareImplementation;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbProtected;
        private System.Windows.Forms.CheckBox cbInternal;
        private System.Windows.Forms.CheckBox cbPrivate;

	}
}
