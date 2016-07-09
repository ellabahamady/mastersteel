using MasterSteelWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace MasterSteelWebApp.Controllers
{
    public class ClientController : ApiController
    {
        private DbProviderFactory dbProviderFactory;


        [HttpGet]
        [ActionName("GetAllClients")]
        public IEnumerable<Client> GetAllClients()
        {
            List<Client> result;
            result = new List<Client>();

            result = ClientsGetAllData();
            return result;
        }

        private List<Client> ClientsGetAllData()
        {
            List<Client> result;
            result = new List<Client>();

            SettingsApp settingsApp = new SettingsApp();
            dbProviderFactory = DbProviderFactories.GetFactory(settingsApp.MasterSteelConnectionString.ProviderName);

            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = settingsApp.MasterSteelConnectionString.ConnectionString;

                    DbCommand cmd = dbProviderFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetClientsAll";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Client client = new Client();
                                client.Id = reader["Id"].ToString();
                                client.Name = reader["Name"].ToString();

                                result.Add(client);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpGet]
        [ActionName("GetClient")]
        public Client GetClient(string id)
        {
            Client result = new Client();
            result = ClientGetById(id);
            return result;
        }

        private Client ClientGetById(string id)
        {
            Client result;
            result = new Client();

            SettingsApp settingsApp = new SettingsApp();
            dbProviderFactory = DbProviderFactories.GetFactory(settingsApp.MasterSteelConnectionString.ProviderName);

            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = settingsApp.MasterSteelConnectionString.ConnectionString;

                    DbCommand cmd = dbProviderFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetClientById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = id;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.Id = reader["Id"].ToString();
                            result.Name = reader["Name"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpPost]
        [ActionName("SaveClient")]
        public String SaveClient([FromBody] Client client)
        {
            string result;
            result = SaveClientData(client);
            return result;
        }

        private String SaveClientData(Client client)
        {
            String result;
            result = "Fail";

            SettingsApp settingsApp = new SettingsApp();
            dbProviderFactory = DbProviderFactories.GetFactory(settingsApp.MasterSteelConnectionString.ProviderName);

            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = settingsApp.MasterSteelConnectionString.ConnectionString;

                    DbCommand cmd = dbProviderFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spInsertClient";

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = client.Name;
                    cmd.Parameters.Add(sqlParamName);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpPut]
        [ActionName("UpdateClient")]
        public String UpdateClient([FromBody] Client client)
        {
            string result;
            result = UpdateClientData(client);
            return result;
        }

        private String UpdateClientData(Client client)
        {
            String result;
            result = "Fail";

            SettingsApp settingsApp = new SettingsApp();
            dbProviderFactory = DbProviderFactories.GetFactory(settingsApp.MasterSteelConnectionString.ProviderName);

            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = settingsApp.MasterSteelConnectionString.ConnectionString;

                    DbCommand cmd = dbProviderFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spUpdateClientById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = client.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = client.Name;
                    cmd.Parameters.Add(sqlParamName);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpDelete]
        [ActionName("DeleteClient")]
        public String DeleteClient(string id)
        {
            string result;
            result = DeleteClientData(id);
            return result;
        }

        private String DeleteClientData(string id)
        {
            String result;
            result = "Fail";

            SettingsApp settingsApp = new SettingsApp();
            dbProviderFactory = DbProviderFactories.GetFactory(settingsApp.MasterSteelConnectionString.ProviderName);

            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                try
                {
                    conn.ConnectionString = settingsApp.MasterSteelConnectionString.ConnectionString;

                    DbCommand cmd = dbProviderFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spDeleteClientById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = id;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }
    }
}