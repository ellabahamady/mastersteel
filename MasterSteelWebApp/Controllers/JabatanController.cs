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
    public class JabatanController : ApiController
    {
        private DbProviderFactory dbProviderFactory;


        [HttpGet]
        [ActionName("GetAllJabatan")]
        public IEnumerable<Jabatan> GetAllJabatan()
        {
            List<Jabatan> result;
            result = new List<Jabatan>();

            result = JabatanGetAllData();
            return result;
        }

        private List<Jabatan> JabatanGetAllData()
        {
            List<Jabatan> result;
            result = new List<Jabatan>();

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
                    cmd.CommandText = "spGetJabatanAll";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Jabatan jabatan = new Jabatan();
                                jabatan.Id = reader["Id"].ToString();
                                jabatan.Name = reader["Name"].ToString();

                                result.Add(jabatan);
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
        [ActionName("GetJabatan")]
        public Jabatan GetJabatan(string id)
        {
            Jabatan result = new Jabatan();
            result = JabatanGetById(id);
            return result;
        }

        private Jabatan JabatanGetById(string id)
        {
            Jabatan result;
            result = new Jabatan();

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
                    cmd.CommandText = "spGetJabatanById";

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
        [ActionName("SaveJabatan")]
        public String SaveJabatan([FromBody] Jabatan jabatan)
        {
            string result;
            result = SaveJabatanData(jabatan);
            return result;
        }

        private String SaveJabatanData(Jabatan jabatan)
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
                    cmd.CommandText = "spInsertJabatan";

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = jabatan.Name;
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
        [ActionName("UpdateJabatan")]
        public String UpdateJabatan([FromBody] Jabatan jabatan)
        {
            string result;
            result = UpdateJabatanData(jabatan);
            return result;
        }

        private String UpdateJabatanData(Jabatan jabatan)
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
                    cmd.CommandText = "spUpdateJabatanById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = jabatan.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = jabatan.Name;
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
        [ActionName("DeleteJabatan")]
        public String DeleteJabatan(string id)
        {
            string result;
            result = DeleteJabatanData(id);
            return result;
        }

        private String DeleteJabatanData(string id)
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
                    cmd.CommandText = "spDeleteJabatanById";

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