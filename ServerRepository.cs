using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace SQLBackup
{
	class ServerRepository
	{
		private string sysDatabase = "sys.databases";
		private string server_name;
		private string user_name;
		private string password;

		public string ServerName
		{
			get {
				return server_name;
			}
			set {
				server_name = value;
			}
		}

		public string UserName {
			get {
				return user_name;
			}
			set {
				user_name = value;
			}
		}

		public string Password {
			get {
				return password;
			}
			set {
				password = value;
			}
		}

		private string ConnectionString {
			get {
				if (string.IsNullOrEmpty(ServerName.Trim(' '))) {
					return "";
				}
				if (UserName.Trim(' ') != "" && Password.Trim(' ') != "")
				{
					return String.Format(@"server={0};uid={1};pwd={2};", ServerName, UserName, Password);
				}

				return @"Data Source=" + ServerName + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
			}
		}


		public List<string> GetServers()
		{
			List<string> servers_name = new List<string>();
			DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
			for (int i = 0; i < servers.Rows.Count; i++)
			{
				if ((servers.Rows[i]["InstanceName"] as string) != null)
					servers_name.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
				else
					servers_name.Add((servers.Rows[i]["ServerName"]).ToString());
			}
			List<string> _localDbServers = Directory.GetDirectories(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances", Environment.UserName)).ToList();
			foreach(string localDbServer in _localDbServers) {
				servers_name.Add(@"(localdb)\" + new DirectoryInfo(localDbServer).Name);
			}
			//_serversName.Add(@"(localdb)\MSSQLLocalDB");
			return servers_name;
		}

		public List<string> GetDatabases()
		{
			return _getDatabases(ConnectionString);
		}

		public List<string> GetDatabases(string connection_string)
		{
			return _getDatabases(connection_string);
		}


		public string GetScript(string databaseName, bool createDatabase = false, bool getSchemaEntity = true, string connectionString = "")
		{
			var _scriptSql = new StringBuilder();
			if (createDatabase == true)
			{
				_scriptSql.Append(_scriptCreatedDataBase(databaseName, connectionString));
				_scriptSql.Append(String.Format("USE [{0}]" + Environment.NewLine + "GO" + Environment.NewLine, databaseName));
			}
			_scriptSql.Append(_scriptContentDatabase(databaseName, getSchemaEntity, connectionString));
			return _scriptSql.ToString();
		}

		public List<string> RestoreContentInDatabase(string databaseName, string filePath)
		{
			string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + databaseName + ";Data Source=" + ServerName;
			List<string> messeges = new List<string>();

			string script = File.ReadAllText(filePath);

			SqlConnection conn = new SqlConnection(connectionString);
			conn.FireInfoMessageEventOnUserErrors = true;

			Server server = new Server(new ServerConnection(conn));

			string[] singleCommand = Regex.Split(script, "^GO", RegexOptions.Multiline);
			StringCollection sql = new StringCollection();
			foreach (string t in singleCommand)
			{
				if (t.Trim().Length > 0) sql.Add(t.Trim());
			}
			conn.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
			{
				foreach (SqlError info in e.Errors)
				{
					messeges.Add(info.ToString());
				}

			};

			int[] result = server.ConnectionContext.ExecuteNonQuery(sql, ExecutionTypes.ContinueOnError);

			return messeges;
		}


		private string _scriptContentDatabase(string databaseName, bool getSchemaEntity = true, string connectionString = "")
		{
			//List<string> _dataBaseParams = new List<string>() { "ANSI_NULLS ON", "QUOTED_IDENTIFIER ON" };
			var _scriptSql = new StringBuilder();
			Server _server;
			if (connectionString.Trim(' ') != "")
			{
				_server = new Server(new ServerConnection(new SqlConnection(connectionString)));
			}
			else
			{
				_server = new Server(new ServerConnection(new SqlConnection(ConnectionString)));
			}

			var _database = _server.Databases[databaseName];
			var _scripter = new Scripter(_server);

			_scripter.Options.WithDependencies = true;
			_scripter.Options.IncludeHeaders = true;
			_scripter.Options.ScriptSchema = true;
			_scripter.Options.ScriptData = true;
			_scripter.Options.ScriptDrops = false;
			_scripter.Options.IncludeIfNotExists = true;

			var smoObjects = new Urn[1];
			foreach (Table t in _database.Tables)
			{
				if (getSchemaEntity == true)
				{
					smoObjects[0] = t.Urn;
					if (t.IsSystemObject == false)
					{
						foreach (string s in _scripter.EnumScript(new Urn[] { t.Urn }))
						{
							string str = s;
							//foreach (string dbParams in _dataBaseParams)
							//{
							//	if (s.Contains(dbParams))
							//	{
							//		str = s.Insert(s.IndexOf(dbParams) + dbParams.Length, " ");
							//	}
							//}
							_scriptSql.Append(str + Environment.NewLine);
						}
					}
				}
				else
				{
					if (t.Name.StartsWith("en_") == false)
					{
						smoObjects[0] = t.Urn;
						if (t.IsSystemObject == false)
						{
							foreach (string s in _scripter.EnumScript(new Urn[] { t.Urn }))
							{
								string str = s;
								//foreach (string dbParams in _dataBaseParams)
								//{
								//	if (s.Contains(dbParams))
								//	{
								//		str = s.Insert(s.IndexOf(dbParams) + dbParams.Length, " ");
								//	}
								//}
								_scriptSql.Append(str);
							}
						}
					}
				}
			}

			return _scriptSql.ToString();
		}

		private string _scriptCreatedDataBase(string databaseName, string connectionString = "")
		{
			//List<string> _dataBaseParams = new List<string>() { "end", "Cyrillic_General_CI_AS", "SQL_Latin1_General_CP1_CI_AS" };
			string _deleteString = "COLLATE Cyrillic_General_CI_AS";
			var _scriptSql = new StringBuilder();
			Server _server;
			if (connectionString.Trim(' ') != "")
			{
				_server = new Server(new ServerConnection(new SqlConnection(connectionString)));
			}
			else
			{
				_server = new Server(new ServerConnection(new SqlConnection(ConnectionString)));
			}
			var _database = _server.Databases[databaseName];
			var _createDatabaseScript = _database.Script();
			_scriptSql.Append("USE [master]" + Environment.NewLine + "GO" + Environment.NewLine);
			foreach (string s in _createDatabaseScript)
			{
				string str = s;
				//foreach (string dbParams in _dataBaseParams)
				//{
				//	if (s.Contains(dbParams))
				//	{
				//		if(dbParams == "end")
				//			str = s.Insert(s.IndexOf(dbParams) + dbParams.Length, Environment.NewLine + "GO" + Environment.NewLine);
				//		else
				//			str = s.Insert(s.IndexOf(dbParams) + dbParams.Length, " ");
				//	}
				//}
				if (str.Contains(_deleteString))
				{
					str = str.Remove(str.IndexOf(_deleteString), _deleteString.Length);
				}
				if (str.Contains("ALTER DATABASE [" + databaseName + "] SET COMPATIBILITY_LEVEL = 100"))
				{
					str = "GO" + Environment.NewLine + str;
					string fuck = "GO" + Environment.NewLine + "ALTER DATABASE [" + databaseName + "] SET COMPATIBILITY_LEVEL = 100";
					str = str.Insert(s.IndexOf(fuck) + fuck.Length + 1, "GO");
				}
				if (str.Contains("end"))
				{
					str = s.Insert(s.IndexOf("end") + 3, Environment.NewLine + "GO");
				}
				_scriptSql.Append(str + Environment.NewLine);
			}

			return _scriptSql.ToString();
		}

		private List<string> _getDatabases(string connection_string) {
			List<string> _databasesName = new List<string>();
			using (SqlConnection con = new SqlConnection(connection_string))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT name from " + sysDatabase, con))
				{
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							_databasesName.Add(dr[0].ToString());
						}
					}
				}
			}
			return _databasesName;
		}

		public ServerRepository(string server_name, string user_name, string password) {
			ServerName = server_name;
			UserName = user_name;
			Password = password;
		}

		public ServerRepository(string server_name)
		{
			ServerName = server_name;
		}

		public ServerRepository()
		{

		}
	}
}
