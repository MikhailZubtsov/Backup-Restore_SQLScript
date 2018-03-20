namespace SQLBackup
{
	partial class SQLBackup
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ServerName_Cb = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DatabaseName_Cb = new System.Windows.Forms.ComboBox();
			this.Backup = new System.Windows.Forms.Button();
			this.ProgressBackup = new System.Windows.Forms.ProgressBar();
			this.OptionCreateDB = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.OptionSchemaEntity = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.Username_Tb = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.Password_Tb = new System.Windows.Forms.TextBox();
			this.Errors = new System.Windows.Forms.TextBox();
			this.SelectFile = new System.Windows.Forms.Button();
			this.FilePath = new System.Windows.Forms.Label();
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.Connect_Btn = new System.Windows.Forms.Button();
			this.StatusConnect_Tb = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.ConnectionString_Tb = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.RestoreInfo_Lbl = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Title_lbl = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ServerName_Cb
			// 
			this.ServerName_Cb.FormattingEnabled = true;
			this.ServerName_Cb.Location = new System.Drawing.Point(133, 92);
			this.ServerName_Cb.Name = "ServerName_Cb";
			this.ServerName_Cb.Size = new System.Drawing.Size(448, 21);
			this.ServerName_Cb.TabIndex = 0;
			this.ServerName_Cb.SelectedIndexChanged += new System.EventHandler(this.serverName_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Server*:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 292);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Database:";
			// 
			// DatabaseName_Cb
			// 
			this.DatabaseName_Cb.FormattingEnabled = true;
			this.DatabaseName_Cb.Location = new System.Drawing.Point(132, 289);
			this.DatabaseName_Cb.Name = "DatabaseName_Cb";
			this.DatabaseName_Cb.Size = new System.Drawing.Size(448, 21);
			this.DatabaseName_Cb.TabIndex = 3;
			// 
			// Backup
			// 
			this.Backup.BackColor = System.Drawing.Color.Black;
			this.Backup.ForeColor = System.Drawing.Color.White;
			this.Backup.Location = new System.Drawing.Point(482, 369);
			this.Backup.Name = "Backup";
			this.Backup.Size = new System.Drawing.Size(99, 87);
			this.Backup.TabIndex = 4;
			this.Backup.Text = "Backup";
			this.Backup.UseVisualStyleBackColor = false;
			this.Backup.Click += new System.EventHandler(this.Backup_Click);
			// 
			// ProgressBackup
			// 
			this.ProgressBackup.Location = new System.Drawing.Point(12, 369);
			this.ProgressBackup.Name = "ProgressBackup";
			this.ProgressBackup.Size = new System.Drawing.Size(464, 21);
			this.ProgressBackup.TabIndex = 5;
			// 
			// OptionCreateDB
			// 
			this.OptionCreateDB.AutoSize = true;
			this.OptionCreateDB.Location = new System.Drawing.Point(132, 333);
			this.OptionCreateDB.Name = "OptionCreateDB";
			this.OptionCreateDB.Size = new System.Drawing.Size(118, 19);
			this.OptionCreateDB.TabIndex = 8;
			this.OptionCreateDB.Text = "Create database";
			this.OptionCreateDB.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 335);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "Options for script:";
			// 
			// OptionSchemaEntity
			// 
			this.OptionSchemaEntity.AutoSize = true;
			this.OptionSchemaEntity.Location = new System.Drawing.Point(256, 333);
			this.OptionSchemaEntity.Name = "OptionSchemaEntity";
			this.OptionSchemaEntity.Size = new System.Drawing.Size(164, 19);
			this.OptionSchemaEntity.TabIndex = 10;
			this.OptionSchemaEntity.Text = "Just SchemaEntity tables";
			this.OptionSchemaEntity.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 134);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 15);
			this.label5.TabIndex = 11;
			this.label5.Text = "Username:";
			// 
			// Username_Tb
			// 
			this.Username_Tb.Location = new System.Drawing.Point(133, 131);
			this.Username_Tb.Name = "Username_Tb";
			this.Username_Tb.Size = new System.Drawing.Size(448, 20);
			this.Username_Tb.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 173);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 15);
			this.label6.TabIndex = 13;
			this.label6.Text = "Password:";
			// 
			// Password_Tb
			// 
			this.Password_Tb.Location = new System.Drawing.Point(133, 170);
			this.Password_Tb.Name = "Password_Tb";
			this.Password_Tb.Size = new System.Drawing.Size(448, 20);
			this.Password_Tb.TabIndex = 14;
			// 
			// Errors
			// 
			this.Errors.Location = new System.Drawing.Point(12, 396);
			this.Errors.Multiline = true;
			this.Errors.Name = "Errors";
			this.Errors.Size = new System.Drawing.Size(464, 60);
			this.Errors.TabIndex = 17;
			// 
			// SelectFile
			// 
			this.SelectFile.BackColor = System.Drawing.Color.Black;
			this.SelectFile.ForeColor = System.Drawing.Color.White;
			this.SelectFile.Location = new System.Drawing.Point(132, 316);
			this.SelectFile.Name = "SelectFile";
			this.SelectFile.Size = new System.Drawing.Size(448, 23);
			this.SelectFile.TabIndex = 22;
			this.SelectFile.Text = "Select a file";
			this.SelectFile.UseVisualStyleBackColor = false;
			this.SelectFile.Visible = false;
			this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
			// 
			// FilePath
			// 
			this.FilePath.AutoSize = true;
			this.FilePath.Location = new System.Drawing.Point(130, 342);
			this.FilePath.Name = "FilePath";
			this.FilePath.Size = new System.Drawing.Size(0, 15);
			this.FilePath.TabIndex = 23;
			this.FilePath.Visible = false;
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.FileName = "openFileDialog1";
			// 
			// Connect_Btn
			// 
			this.Connect_Btn.BackColor = System.Drawing.Color.Black;
			this.Connect_Btn.ForeColor = System.Drawing.Color.White;
			this.Connect_Btn.Location = new System.Drawing.Point(482, 250);
			this.Connect_Btn.Name = "Connect_Btn";
			this.Connect_Btn.Size = new System.Drawing.Size(98, 21);
			this.Connect_Btn.TabIndex = 24;
			this.Connect_Btn.Text = "Connect";
			this.Connect_Btn.UseVisualStyleBackColor = false;
			this.Connect_Btn.Click += new System.EventHandler(this.Connect_Btn_Click);
			// 
			// StatusConnect_Tb
			// 
			this.StatusConnect_Tb.Location = new System.Drawing.Point(132, 248);
			this.StatusConnect_Tb.Multiline = true;
			this.StatusConnect_Tb.Name = "StatusConnect_Tb";
			this.StatusConnect_Tb.Size = new System.Drawing.Size(344, 35);
			this.StatusConnect_Tb.TabIndex = 25;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(130, 74);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(231, 15);
			this.label9.TabIndex = 26;
			this.label9.Text = "* - choose database if restore just content";
			this.label9.Visible = false;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 222);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(104, 15);
			this.label12.TabIndex = 36;
			this.label12.Text = "ConnectionString:";
			// 
			// ConnectionString_Tb
			// 
			this.ConnectionString_Tb.Location = new System.Drawing.Point(132, 222);
			this.ConnectionString_Tb.Name = "ConnectionString_Tb";
			this.ConnectionString_Tb.Size = new System.Drawing.Size(448, 20);
			this.ConnectionString_Tb.TabIndex = 35;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(335, 193);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(25, 15);
			this.label13.TabIndex = 37;
			this.label13.Text = "OR";
			// 
			// RestoreInfo_Lbl
			// 
			this.RestoreInfo_Lbl.AutoSize = true;
			this.RestoreInfo_Lbl.Location = new System.Drawing.Point(315, 369);
			this.RestoreInfo_Lbl.Name = "RestoreInfo_Lbl";
			this.RestoreInfo_Lbl.Size = new System.Drawing.Size(161, 15);
			this.RestoreInfo_Lbl.TabIndex = 38;
			this.RestoreInfo_Lbl.Text = "Include CREATE DATABASE";
			this.RestoreInfo_Lbl.Visible = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(592, 24);
			this.menuStrip1.TabIndex = 39;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// backupToolStripMenuItem
			// 
			this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
			this.backupToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.backupToolStripMenuItem.Text = "Backup";
			this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
			// 
			// restoreToolStripMenuItem
			// 
			this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
			this.restoreToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.restoreToolStripMenuItem.Text = "Restore";
			this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
			// 
			// Title_lbl
			// 
			this.Title_lbl.BackColor = System.Drawing.Color.LightGray;
			this.Title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.05405F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Title_lbl.Location = new System.Drawing.Point(0, 24);
			this.Title_lbl.Name = "Title_lbl";
			this.Title_lbl.Size = new System.Drawing.Size(592, 39);
			this.Title_lbl.TabIndex = 40;
			this.Title_lbl.Text = "Backup";
			this.Title_lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// SQLBackup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(592, 470);
			this.Controls.Add(this.Title_lbl);
			this.Controls.Add(this.RestoreInfo_Lbl);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.ConnectionString_Tb);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.StatusConnect_Tb);
			this.Controls.Add(this.Connect_Btn);
			this.Controls.Add(this.FilePath);
			this.Controls.Add(this.SelectFile);
			this.Controls.Add(this.Errors);
			this.Controls.Add(this.Password_Tb);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.Username_Tb);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.OptionSchemaEntity);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.OptionCreateDB);
			this.Controls.Add(this.ProgressBackup);
			this.Controls.Add(this.Backup);
			this.Controls.Add(this.DatabaseName_Cb);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ServerName_Cb);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "SQLBackup";
			this.Text = "Backup/Restore SQL";
			this.Load += new System.EventHandler(this.SQLBackup_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox ServerName_Cb;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox DatabaseName_Cb;
		private System.Windows.Forms.Button Backup;
		private System.Windows.Forms.ProgressBar ProgressBackup;
		private System.Windows.Forms.CheckBox OptionCreateDB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox OptionSchemaEntity;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox Username_Tb;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox Password_Tb;
		private System.Windows.Forms.TextBox Errors;
		private System.Windows.Forms.Button SelectFile;
		private System.Windows.Forms.Label FilePath;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		private System.Windows.Forms.Button Connect_Btn;
		private System.Windows.Forms.TextBox StatusConnect_Tb;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox ConnectionString_Tb;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label RestoreInfo_Lbl;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
		private System.Windows.Forms.Label Title_lbl;
	}
}

