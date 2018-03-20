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
		private string GetConnectionString(string serverName, string userName, string passwordName)
		{
			string _connectionString = @"Data Source=" + serverName + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
			if (userName.Trim(' ') != "" && passwordName.Trim(' ') != "")
			{
				_connectionString = String.Format(@"server={0};uid={1};pwd={2};", serverName, userName, passwordName);
			}

			return _connectionString;
		}

		static public List<string> GetServers()
		{
			List<string> _serversName = new List<string>();
			DataTable _servers = SqlDataSourceEnumerator.Instance.GetDataSources();
			for (int i = 0; i < _servers.Rows.Count; i++)
			{
				if ((_servers.Rows[i]["InstanceName"] as string) != null)
					_serversName.Add(_servers.Rows[i]["ServerName"] + "\\" + _servers.Rows[i]["InstanceName"]);
				else
					_serversName.Add((_servers.Rows[i]["ServerName"]).ToString());
			}
			List<string> _localDbServers = Directory.GetDirectories(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances", Environment.UserName)).ToList();
			foreach(string localDbServer in _localDbServers) {
				_serversName.Add(@"(localdb)\" + new DirectoryInfo(localDbServer).Name);
			}
			//_serversName.Add(@"(localdb)\MSSQLLocalDB");
			return _serversName;
		}

		static public List<string> GetDatabases(string serverName, string userName, string passwordName)
		{
			List<string> _databasesName = new List<string>();
			ServerRepository _serverRepository = new ServerRepository();
			string _connectionString = _serverRepository.GetConnectionString(serverName, userName, passwordName);
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
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

		static public List<string> GetDatabases(string connectionString)
		{
			List<string> _databasesName = new List<string>();
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
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


		static public string GetScript(string serverName, string databaseName, string userName, string password, bool createDatabase = false, bool getSchemaEntity = true, string connectionString = "")
		{
			var _scriptSql = new StringBuilder();
			ServerRepository _serverRepository = new ServerRepository();
			if (createDatabase == true)
			{
				_scriptSql.Append(_serverRepository.ScriptCreateDataBase(serverName, databaseName, userName, password, connectionString));
				_scriptSql.Append(String.Format("USE [{0}]" + Environment.NewLine + "GO" + Environment.NewLine, databaseName));
			}
			_scriptSql.Append(_serverRepository.ScriptContentDatabase(serverName, databaseName, userName, password, getSchemaEntity, connectionString));
			return _scriptSql.ToString();
		}

		static public List<string> RestoreContentInDatabase(string serverName, string databaseName, string filePath)
		{
			string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + databaseName + ";Data Source=" + serverName;
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


		private string ScriptContentDatabase(string serverName, string databaseName, string userName, string password, bool getSchemaEntity = true, string connectionString = "")
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
				_server = new Server(new ServerConnection(new SqlConnection(GetConnectionString(serverName, userName, password))));
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

		private string ScriptCreateDataBase(string serverName, string databaseName, string userName, string password, string connectionString = "")
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
				_server = new Server(new ServerConnection(new SqlConnection(GetConnectionString(serverName, userName, password))));
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
	}
}
