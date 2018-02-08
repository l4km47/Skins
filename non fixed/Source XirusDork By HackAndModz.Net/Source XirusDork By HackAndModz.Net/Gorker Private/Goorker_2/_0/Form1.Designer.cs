namespace Goorker_2._0
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : global::MetroFramework.Forms.MetroForm
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020A3 File Offset: 0x000002A3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002524 File Offset: 0x00000724
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Goorker_2._0.Form1));
			this.metroLabel1 = new global::MetroFramework.Controls.MetroLabel();
			this.metroLabel2 = new global::MetroFramework.Controls.MetroLabel();
			this.metroLabel3 = new global::MetroFramework.Controls.MetroLabel();
			this.GenButton = new global::MetroFramework.Controls.MetroButton();
			this.CheckedListBox1 = new global::System.Windows.Forms.CheckedListBox();
			this.Shopping = new global::MetroFramework.Controls.MetroCheckBox();
			this.Forums = new global::MetroFramework.Controls.MetroCheckBox();
			this.Porn = new global::MetroFramework.Controls.MetroCheckBox();
			this.Gaming = new global::MetroFramework.Controls.MetroCheckBox();
			this.Music = new global::MetroFramework.Controls.MetroCheckBox();
			this.metroPanel1 = new global::MetroFramework.Controls.MetroPanel();
			this.Custom = new global::MetroFramework.Controls.MetroCheckBox();
			this.Sports = new global::MetroFramework.Controls.MetroCheckBox();
			this.NumericUpDown1 = new global::System.Windows.Forms.NumericUpDown();
			this.metroLabel4 = new global::MetroFramework.Controls.MetroLabel();
			this.customdorks = new global::MetroFramework.Controls.MetroTextBox();
			this.metroLabel5 = new global::MetroFramework.Controls.MetroLabel();
			this.metroButton1 = new global::MetroFramework.Controls.MetroButton();
			this.openFileDialog_0 = new global::System.Windows.Forms.OpenFileDialog();
			this.metroPanel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.NumericUpDown1).BeginInit();
			base.SuspendLayout();
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.Location = new global::System.Drawing.Point(5, 60);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new global::System.Drawing.Size(105, 19);
			this.metroLabel1.TabIndex = 3;
			this.metroLabel1.Text = "Select Countries:";
			this.metroLabel1.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.Location = new global::System.Drawing.Point(143, 60);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new global::System.Drawing.Size(113, 19);
			this.metroLabel2.TabIndex = 5;
			this.metroLabel2.Text = "Select Categories:";
			this.metroLabel2.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.metroLabel3.AutoSize = true;
			this.metroLabel3.Location = new global::System.Drawing.Point(5, 238);
			this.metroLabel3.Name = "metroLabel3";
			this.metroLabel3.Size = new global::System.Drawing.Size(96, 19);
			this.metroLabel3.TabIndex = 8;
			this.metroLabel3.Text = "Dorks Amount:";
			this.metroLabel3.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.GenButton.Location = new global::System.Drawing.Point(169, 237);
			this.GenButton.Name = "GenButton";
			this.GenButton.Size = new global::System.Drawing.Size(95, 21);
			this.GenButton.TabIndex = 11;
			this.GenButton.Text = "Generate!";
			this.GenButton.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.GenButton.UseSelectable = true;
			this.GenButton.Click += new global::System.EventHandler(this.GenButton_Click);
			this.CheckedListBox1.BackColor = global::System.Drawing.Color.FromArgb(17, 17, 17);
			this.CheckedListBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.CheckedListBox1.CheckOnClick = true;
			this.CheckedListBox1.ForeColor = global::System.Drawing.Color.Yellow;
			this.CheckedListBox1.FormattingEnabled = true;
			this.CheckedListBox1.Items.AddRange(new object[]
			{
				".de",
				".co.uk",
				".es",
				".cz",
				".ro",
				".pl",
				".us",
				".ru",
				".it",
				".com",
				".net",
				".fr",
				".au",
				".tr",
				".eu",
				".com.tr",
				".fi",
				".br",
				".bg",
				".ee"
			});
			this.CheckedListBox1.Location = new global::System.Drawing.Point(14, 81);
			this.CheckedListBox1.Name = "CheckedListBox1";
			this.CheckedListBox1.Size = new global::System.Drawing.Size(120, 152);
			this.CheckedListBox1.TabIndex = 13;
			this.CheckedListBox1.ThreeDCheckBoxes = true;
			this.CheckedListBox1.UseCompatibleTextRendering = true;
			this.Shopping.AutoSize = true;
			this.Shopping.ForeColor = global::System.Drawing.Color.Yellow;
			this.Shopping.Location = new global::System.Drawing.Point(3, 45);
			this.Shopping.Name = "Shopping";
			this.Shopping.Size = new global::System.Drawing.Size(74, 15);
			this.Shopping.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Shopping.TabIndex = 16;
			this.Shopping.Text = "Shopping";
			this.Shopping.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Shopping.UseCustomForeColor = true;
			this.Shopping.UseSelectable = true;
			this.Forums.AutoSize = true;
			this.Forums.ForeColor = global::System.Drawing.Color.Yellow;
			this.Forums.Location = new global::System.Drawing.Point(3, 3);
			this.Forums.Name = "Forums";
			this.Forums.Size = new global::System.Drawing.Size(63, 15);
			this.Forums.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Forums.TabIndex = 17;
			this.Forums.Text = "Forums";
			this.Forums.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Forums.UseCustomForeColor = true;
			this.Forums.UseSelectable = true;
			this.Porn.AutoSize = true;
			this.Porn.ForeColor = global::System.Drawing.Color.Yellow;
			this.Porn.Location = new global::System.Drawing.Point(3, 24);
			this.Porn.Name = "Porn";
			this.Porn.Size = new global::System.Drawing.Size(48, 15);
			this.Porn.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Porn.TabIndex = 18;
			this.Porn.Text = "Porn";
			this.Porn.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Porn.UseCustomForeColor = true;
			this.Porn.UseSelectable = true;
			this.Gaming.AutoSize = true;
			this.Gaming.ForeColor = global::System.Drawing.Color.Yellow;
			this.Gaming.Location = new global::System.Drawing.Point(3, 66);
			this.Gaming.Name = "Gaming";
			this.Gaming.Size = new global::System.Drawing.Size(65, 15);
			this.Gaming.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Gaming.TabIndex = 19;
			this.Gaming.Text = "Gaming";
			this.Gaming.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Gaming.UseCustomForeColor = true;
			this.Gaming.UseSelectable = true;
			this.Music.AutoSize = true;
			this.Music.ForeColor = global::System.Drawing.Color.Yellow;
			this.Music.Location = new global::System.Drawing.Point(3, 87);
			this.Music.Name = "Music";
			this.Music.Size = new global::System.Drawing.Size(55, 15);
			this.Music.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Music.TabIndex = 20;
			this.Music.Text = "Music";
			this.Music.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Music.UseCustomForeColor = true;
			this.Music.UseSelectable = true;
			this.metroPanel1.Controls.Add(this.Custom);
			this.metroPanel1.Controls.Add(this.Sports);
			this.metroPanel1.Controls.Add(this.Gaming);
			this.metroPanel1.Controls.Add(this.Music);
			this.metroPanel1.Controls.Add(this.Shopping);
			this.metroPanel1.Controls.Add(this.Porn);
			this.metroPanel1.Controls.Add(this.Forums);
			this.metroPanel1.HorizontalScrollbarBarColor = true;
			this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
			this.metroPanel1.HorizontalScrollbarSize = 10;
			this.metroPanel1.Location = new global::System.Drawing.Point(147, 81);
			this.metroPanel1.Name = "metroPanel1";
			this.metroPanel1.Size = new global::System.Drawing.Size(117, 150);
			this.metroPanel1.TabIndex = 21;
			this.metroPanel1.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.metroPanel1.VerticalScrollbarBarColor = true;
			this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
			this.metroPanel1.VerticalScrollbarSize = 10;
			this.Custom.AutoSize = true;
			this.Custom.ForeColor = global::System.Drawing.Color.Yellow;
			this.Custom.Location = new global::System.Drawing.Point(3, 129);
			this.Custom.Name = "Custom";
			this.Custom.Size = new global::System.Drawing.Size(65, 15);
			this.Custom.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Custom.TabIndex = 22;
			this.Custom.Text = "Custom";
			this.Custom.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Custom.UseCustomForeColor = true;
			this.Custom.UseSelectable = true;
			this.Sports.AutoSize = true;
			this.Sports.ForeColor = global::System.Drawing.Color.Yellow;
			this.Sports.Location = new global::System.Drawing.Point(3, 108);
			this.Sports.Name = "Sports";
			this.Sports.Size = new global::System.Drawing.Size(56, 15);
			this.Sports.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Sports.TabIndex = 21;
			this.Sports.Text = "Sports";
			this.Sports.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.Sports.UseCustomForeColor = true;
			this.Sports.UseSelectable = true;
			this.NumericUpDown1.Location = new global::System.Drawing.Point(101, 238);
			global::System.Windows.Forms.NumericUpDown arg_A13_0 = this.NumericUpDown1;
			int[] array = new int[4];
			array[0] = 20000;
			arg_A13_0.Maximum = new decimal(array);
			global::System.Windows.Forms.NumericUpDown arg_A30_0 = this.NumericUpDown1;
			array = new int[4];
			array[0] = 100;
			arg_A30_0.Minimum = new decimal(array);
			this.NumericUpDown1.Name = "NumericUpDown1";
			this.NumericUpDown1.Size = new global::System.Drawing.Size(62, 20);
			this.NumericUpDown1.TabIndex = 21;
			global::System.Windows.Forms.NumericUpDown arg_A7E_0 = this.NumericUpDown1;
			array = new int[4];
			array[0] = 100;
			arg_A7E_0.Value = new decimal(array);
			this.metroLabel4.AutoSize = true;
			this.metroLabel4.Location = new global::System.Drawing.Point(5, 318);
			this.metroLabel4.Name = "metroLabel4";
			this.metroLabel4.Size = new global::System.Drawing.Size(251, 19);
			this.metroLabel4.TabIndex = 22;
			this.metroLabel4.Text = "(Note): Max. Generating Amount is 20000";
			this.metroLabel4.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.customdorks.CustomButton.Image = null;
			this.customdorks.CustomButton.Location = new global::System.Drawing.Point(122, 1);
			this.customdorks.CustomButton.Name = "";
			this.customdorks.CustomButton.Size = new global::System.Drawing.Size(21, 21);
			this.customdorks.CustomButton.Style = global::MetroFramework.MetroColorStyle.Blue;
			this.customdorks.CustomButton.TabIndex = 1;
			this.customdorks.CustomButton.Theme = global::MetroFramework.MetroThemeStyle.Light;
			this.customdorks.CustomButton.UseSelectable = true;
			this.customdorks.CustomButton.Visible = false;
			this.customdorks.Lines = new string[0];
			this.customdorks.Location = new global::System.Drawing.Point(121, 264);
			this.customdorks.MaxLength = 32767;
			this.customdorks.Name = "customdorks";
			this.customdorks.PasswordChar = '\0';
			this.customdorks.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
			this.customdorks.SelectedText = "";
			this.customdorks.SelectionLength = 0;
			this.customdorks.SelectionStart = 0;
			this.customdorks.ShortcutsEnabled = true;
			this.customdorks.Size = new global::System.Drawing.Size(144, 23);
			this.customdorks.TabIndex = 23;
			this.customdorks.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.customdorks.UseSelectable = true;
			this.customdorks.WaterMarkColor = global::System.Drawing.Color.FromArgb(109, 109, 109);
			this.customdorks.WaterMarkFont = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Pixel);
			this.metroLabel5.AutoSize = true;
			this.metroLabel5.Location = new global::System.Drawing.Point(5, 266);
			this.metroLabel5.Name = "metroLabel5";
			this.metroLabel5.Size = new global::System.Drawing.Size(116, 19);
			this.metroLabel5.TabIndex = 24;
			this.metroLabel5.Text = "Custom Keywords:";
			this.metroLabel5.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.metroButton1.Location = new global::System.Drawing.Point(5, 294);
			this.metroButton1.Name = "metroButton1";
			this.metroButton1.Size = new global::System.Drawing.Size(259, 21);
			this.metroButton1.TabIndex = 25;
			this.metroButton1.Text = "Import Keywords from text file";
			this.metroButton1.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			this.metroButton1.UseSelectable = true;
			this.metroButton1.Click += new global::System.EventHandler(this.metroButton1_Click);
			this.openFileDialog_0.FileName = "openFileDialog1";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(269, 343);
			base.Controls.Add(this.metroButton1);
			base.Controls.Add(this.metroLabel5);
			base.Controls.Add(this.customdorks);
			base.Controls.Add(this.metroLabel4);
			base.Controls.Add(this.NumericUpDown1);
			base.Controls.Add(this.metroPanel1);
			base.Controls.Add(this.CheckedListBox1);
			base.Controls.Add(this.GenButton);
			base.Controls.Add(this.metroLabel3);
			base.Controls.Add(this.metroLabel2);
			base.Controls.Add(this.metroLabel1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Form1";
			base.Resizable = false;
			base.ShadowType = global::MetroFramework.Forms.MetroFormShadowType.DropShadow;
			base.Style = global::MetroFramework.MetroColorStyle.Yellow;
			this.Text = "Gorker Private";
			base.Theme = global::MetroFramework.MetroThemeStyle.Dark;
			base.Load += new global::System.EventHandler(this.Form1_Load);
			this.metroPanel1.ResumeLayout(false);
			this.metroPanel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.NumericUpDown1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000002 RID: 2
		private global::System.ComponentModel.IContainer icontainer_0 = null;

		// Token: 0x04000003 RID: 3
		private global::MetroFramework.Controls.MetroLabel metroLabel1;

		// Token: 0x04000004 RID: 4
		private global::MetroFramework.Controls.MetroLabel metroLabel2;

		// Token: 0x04000005 RID: 5
		private global::MetroFramework.Controls.MetroLabel metroLabel3;

		// Token: 0x04000006 RID: 6
		private global::MetroFramework.Controls.MetroButton GenButton;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.CheckedListBox CheckedListBox1;

		// Token: 0x04000008 RID: 8
		private global::MetroFramework.Controls.MetroCheckBox Shopping;

		// Token: 0x04000009 RID: 9
		private global::MetroFramework.Controls.MetroCheckBox Forums;

		// Token: 0x0400000A RID: 10
		private global::MetroFramework.Controls.MetroCheckBox Porn;

		// Token: 0x0400000B RID: 11
		private global::MetroFramework.Controls.MetroCheckBox Gaming;

		// Token: 0x0400000C RID: 12
		private global::MetroFramework.Controls.MetroCheckBox Music;

		// Token: 0x0400000D RID: 13
		private global::MetroFramework.Controls.MetroPanel metroPanel1;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.NumericUpDown NumericUpDown1;

		// Token: 0x0400000F RID: 15
		private global::MetroFramework.Controls.MetroCheckBox Sports;

		// Token: 0x04000010 RID: 16
		private global::MetroFramework.Controls.MetroLabel metroLabel4;

		// Token: 0x04000011 RID: 17
		private global::MetroFramework.Controls.MetroCheckBox Custom;

		// Token: 0x04000012 RID: 18
		private global::MetroFramework.Controls.MetroTextBox customdorks;

		// Token: 0x04000013 RID: 19
		private global::MetroFramework.Controls.MetroLabel metroLabel5;

		// Token: 0x04000014 RID: 20
		private global::MetroFramework.Controls.MetroButton metroButton1;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.OpenFileDialog openFileDialog_0;
	}
}
