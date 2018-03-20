using Microsoft.SqlServer.Management.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLBackup
{
	public partial class SQLBackup : Form
	{
		private string _form = "Backup";
		public SQLBackup()
		{
			InitializeComponent();
		}

		private void SQLBackup_Load(object sender, EventArgs e)
		{
			List<string> _servers;
			_servers = ServerRepository.GetServers();
			foreach (string server in _servers) {
				ServerName_Cb.Items.Add(server);
			}
			RestoreInfo_Lbl.Text = "";
		}

		private void serverName_SelectedIndexChanged(object sender, EventArgs e)
		{
			Username_Tb.Text = "";
			Password_Tb.Text = "";
		}


		private void Backup_Click(object sender, EventArgs e)
		{
			if (_form == "Backup")
			{
				ProgressBackup.Value = 0;
				Errors.Text = "";
				Backup.Enabled = false;
				Backup.BackColor = Color.Gray;
				bool _getSchemaEntity = !OptionSchemaEntity.Checked;
				bool _createDatabase = OptionCreateDB.Checked;
				string _serverName = ServerName_Cb.Text;
				string _databaseName = DatabaseName_Cb.Text;
				string _userName = Username_Tb.Text;
				string _password = Password_Tb.Text;
				string _connectionString = ConnectionString_Tb.Text.Trim(' ');
				try
				{
					SaveFileDialog saveFileDialog1 = new SaveFileDialog();
					saveFileDialog1.Filter = "Sql files|*.sql";
					saveFileDialog1.Title = "Save an Sql File";
					saveFileDialog1.ShowDialog();

					if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
					{
						string _path = saveFileDialog1.FileName;

						if (!_path.EndsWith(".sql"))
							_path += ".sql";

						Thread backgroundThread = new Thread(
							new ThreadStart(() =>
							{
								string _scriptSql = ServerRepository.GetScript(_serverName, _databaseName, _userName, _password, _createDatabase, _getSchemaEntity, _connectionString);

								if (File.Exists(_path))
									File.Delete(_path);

								using (FileStream fs = File.Create(_path))
								{
									Byte[] info = new UTF8Encoding(true).GetBytes(_scriptSql);
									fs.Write(info, 0, info.Length);
								}
								Errors.BeginInvoke(new Action(() =>
								{
									Errors.Text = "Complete!";
								}));
								ProgressBackup.BeginInvoke(new Action(() =>
								{
									ProgressBackup.Value = 100;
								}));
								Backup.BeginInvoke(new Action(() =>
								{
									Backup.BackColor = Color.Gray;
									Backup.Enabled = true;
								}));
							}
						));
						backgroundThread.Start();

						while (backgroundThread.IsAlive)
						{
							for (int i = 0; i <= 100; i++)
							{
								Thread.Sleep(100);
								ProgressBackup.Value = i;
							}
						}
					}
				}

				catch (Exception ex)
				{
					Errors.Text = ex.Message;
				}
				Backup.Enabled = true;
			}
			else {
				Errors.Text = "";
				try
				{
					List<string> messages = ServerRepository.RestoreContentInDatabase(ServerName_Cb.Text, DatabaseName_Cb.Text, FilePath.Text);
					for (int i = 0; i < messages.Count; i++)
					{
						if (i == 0)
						{
							Errors.Text = messages[i];
						}
						else
						{
							Errors.Text = Errors.Text + Environment.NewLine + messages[i];
						}
					}
				}
				catch (Exception ex)
				{
					Errors.Text = ex.Message;
				}
			}
		}

		private void RestoreServerName_SelectedIndexChanged(object sender, EventArgs e)
		{
			Username_Tb.Text = "";
			Password_Tb.Text = "";
		}

		private void SelectFile_Click(object sender, EventArgs e)
		{
			Stream fileStream = null;
			OpenFileDialog.Filter = "Sql files|*.sql";
			if (OpenFileDialog.ShowDialog() == DialogResult.OK && (fileStream = OpenFileDialog.OpenFile()) != null)
			{
				FilePath.Text = OpenFileDialog.FileName;
				using (StreamReader script = new StreamReader(OpenFileDialog.FileName))
				{
					//while (true)
					//{
					//	string temp = script.ReadLine();
					//	if (temp == null) break;
					//	SQLScript_Tb.Text += temp + Environment.NewLine;
					//}

					if (script.ReadToEnd().Contains("CREATE DATABASE"))
					{
						RestoreInfo_Lbl.Text = "Include CREATE DATABASE";
					}
					else {
						RestoreInfo_Lbl.Text = "";
					}
					//string command;
					//while ((command = script.ReadLine()) != null)
					//{
					//	SQLScript_Tb.Text = SQLScript_Tb + command + Environment.NewLine;
					//}
				}
			}

		}

		private void Connect_Btn_Click(object sender, EventArgs e)
		{
			try
			{
				DatabaseName_Cb.Items.Clear();
				List<string> _databases = new List<string>();
				if (ConnectionString_Tb.Text.Trim(' ') == "" && ServerName_Cb.Text.Trim(' ') == "") {
					StatusConnect_Tb.Text = "Select a server or write a connection string";
				}
				else if (ConnectionString_Tb.Text.Trim(' ') != "")
				{
					_databases = ServerRepository.GetDatabases(ConnectionString_Tb.Text.Trim(' '));
					StatusConnect_Tb.Text = "Complete!";
				}
				else
				{
					_databases = ServerRepository.GetDatabases(ServerName_Cb.Text, Username_Tb.Text, Password_Tb.Text);
					StatusConnect_Tb.Text = "Complete!";
				}

				foreach (string database in _databases)
				{
					DatabaseName_Cb.Items.Add(database);
				}
			}
			catch (Exception _e)
			{
				StatusConnect_Tb.Text = String.Format("Error: {0}", _e.Message);
			}
		}

		private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_form = "Restore";
			Backup.Text = _form;
			Title_lbl.Text = _form;
			label4.Visible = false;
			OptionCreateDB.Visible = false;
			OptionCreateDB.Checked = false;
			OptionSchemaEntity.Visible = false;
			OptionSchemaEntity.Checked = false;
			ProgressBackup.Visible = false;
			ProgressBackup.Value = 0;
			label9.Visible = true;
			RestoreInfo_Lbl.Visible = true;
			SelectFile.Visible = true;
			FilePath.Visible = true;
			Errors.Text = "";
			StatusConnect_Tb.Text = "";
			ServerName_Cb.Text = "";
			DatabaseName_Cb.Items.Clear();
			DatabaseName_Cb.Text = "";
		}

		private void backupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_form = "Backup";
			Backup.Text = _form;
			Title_lbl.Text = _form;
			label4.Visible = true;
			OptionCreateDB.Visible = true;
			OptionSchemaEntity.Visible = true;
			ProgressBackup.Visible = true;
			label9.Visible = false;
			RestoreInfo_Lbl.Visible = false;
			SelectFile.Visible = false;
			FilePath.Visible = false;
			Errors.Text = "";
			StatusConnect_Tb.Text = "";
			ServerName_Cb.Text = "";
			DatabaseName_Cb.Items.Clear();
			DatabaseName_Cb.Text = "";
		}
	}
}
