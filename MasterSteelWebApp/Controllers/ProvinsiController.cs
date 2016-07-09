using MasterSteelWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MasterSteelWebApp.Controllers
{
    public class ProvinsiController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        //tambah method baru

        [HttpGet]
        [ActionName("CountRowProvinsi")]
        public int CountRowProvinsi(string token)
        {
            int result;
            result = 0;

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = CountRowDataProvinsi();
            return result;
        }

        private int CountRowDataProvinsi()
        {
            int result;
            result = 0;

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
                    cmd.CommandText = "spCountRowProvinsi";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = Convert.ToInt32(reader["CountRow"]);
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
        [ActionName("GetLastUpdateProvinsi")]
        public MS_Provinsi GetLastUpdateProvinsi(string token)
        {
            MS_Provinsi result;
            result = new MS_Provinsi();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetLastUpdateDataProvinsi();
            return result;
        }

        private MS_Provinsi GetLastUpdateDataProvinsi()
        {
            MS_Provinsi result;
            result = new MS_Provinsi();

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
                    cmd.CommandText = "spGetLastUpdateProvinsi";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.ProvinsiID = reader["ProvinsiID"].ToString();
                            result.Description = reader["Description"].ToString();
                            result.CountryID = reader["CountryID"].ToString();
                            result.Status = reader["Status"].ToString();
                            result.UpdateAt = reader["UpdateAt"].ToString();

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
        [ActionName("GetAllProvinsi")]
        public IEnumerable<MS_Provinsi> GetAllProvinsi(string token)
        {
            List<MS_Provinsi> result;
            result = new List<MS_Provinsi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllDataProvinsi();
            return result;
        }

        private List<MS_Provinsi> GetAllDataProvinsi()
        {
            List<MS_Provinsi> result;
            result = new List<MS_Provinsi>();

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
                    cmd.CommandText = "spGetAllProvinsi";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_Provinsi province = new MS_Provinsi();
                                province.ProvinsiID = reader["ProvinsiID"].ToString();
                                province.Description = reader["Description"].ToString();
                                province.CountryID = reader["CountryID"].ToString();
                                //province.Status = reader["Status"].ToString();
                                //province.UpdateAt = reader["UpdateAt"].ToString();

                                result.Add(province);
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

        //tambah method baru

        [HttpGet]
        [ActionName("GetProvinsiByCountryID")]
        public IEnumerable<MS_Provinsi> GetProvinsiByCountryID(string token, string countryID)
        {
            List<MS_Provinsi> result;
            result = new List<MS_Provinsi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetProvinsiDataByCountryID(countryID);
            return result;
        }

        private List<MS_Provinsi> GetProvinsiDataByCountryID(string countryID)
        {
            List<MS_Provinsi> result;
            result = new List<MS_Provinsi>();

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
                    cmd.CommandText = "spSelectProvinsiByCountryID";

                    SqlParameter sqlParamCountryId = new SqlParameter();
                    sqlParamCountryId.ParameterName = "countryID";
                    sqlParamCountryId.DbType = DbType.String;
                    sqlParamCountryId.Value = countryID;
                    cmd.Parameters.Add(sqlParamCountryId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_Provinsi province = new MS_Provinsi();
                                province.ProvinsiID = reader["ProvinsiID"].ToString();
                                province.Description = reader["Description"].ToString();

                                result.Add(province);
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
    }
}
