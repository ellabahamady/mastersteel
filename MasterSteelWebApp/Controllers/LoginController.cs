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
    public class LoginController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpPost]
        public Login GetStatusLogin([FromBody] Login login)
        {
            Login result = new Login();

            result = GetLoginStatus(login);
            //result.StatusLogin = "true";
            //result.Result = "true";
            //result.NIK = "";
            //result.Password = "";
            //result.NIK = login.NIK;
            //result.DeviceId = login.DeviceId;
            //result.Password = login.Password;
            return result;
        }

        private Login GetLoginStatus(Login login)
        {
            Login result;
            result = new Login();

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
                    cmd.CommandText = "spStatusLogin";

                    SqlParameter sqlParamNIK = new SqlParameter();
                    sqlParamNIK.ParameterName = "nik";
                    sqlParamNIK.DbType = DbType.String;
                    sqlParamNIK.Value = login.NIK;
                    cmd.Parameters.Add(sqlParamNIK);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = login.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    SqlParameter sqlParamDeviceId = new SqlParameter();
                    sqlParamDeviceId.ParameterName = "deviceId";
                    sqlParamDeviceId.DbType = DbType.String;
                    sqlParamDeviceId.Value = login.DeviceId;
                    cmd.Parameters.Add(sqlParamDeviceId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.StatusLogin = reader["StatusLogin"].ToString();
                            result.Result = reader["Result"].ToString();
                            result.Email = reader["Email"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                    result.Result = ex.Message;
                }
                finally
                {
//                    result = "Success";
//                    ResendVerifyEmail(user);

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        private Login GetLoginStatus2(Login login)
        {
            Login result;
            result = new Login();

            result.NIK = login.NIK;
            result.Password = login.Password;
            result.DeviceId = login.DeviceId;

            return result;

        }

        [HttpPost]
        [ActionName("GetUserLogin")]
        //[AcceptVerbs("POST")]
        public User GetUserLogin([FromBody] User user)
        {
            User result;
            result = new User();
            result = GetLoginUser(user);
            return result;
        }
        private User GetLoginUser(User user)
        {
            User result;
            result = new User();

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
                    cmd.CommandText = "spCheckUser";

                    SqlParameter sqlParamNIK = new SqlParameter();
                    sqlParamNIK.ParameterName = "nik";
                    sqlParamNIK.DbType = DbType.String;
                    sqlParamNIK.Value = user.NIK;
                    cmd.Parameters.Add(sqlParamNIK);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.NIK = reader["NIK"].ToString();
                            result.Id = reader["Id"].ToString();
                            result.Nama = reader["Nama"].ToString();
                            result.JabatanId = reader["JabatanId"].ToString();
                            result.JabatanName = reader["JabatanName"].ToString();
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

        /* 
         [HttpPost]
        [ActionName("GetUserLogin")]
        //[AcceptVerbs("POST")]
        public User GetUserLogin([FromBody] User user)
        {
            User result;
            result = new User();
            result = GetLoginUser(user);
            return result;
        }
        private User GetLoginUser(User user)
        {
            User result;
            result = new User();

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
                    cmd.CommandText = "spUserLogin";

                    SqlParameter sqlParamNIK = new SqlParameter();
                    sqlParamNIK.ParameterName = "nik";
                    sqlParamNIK.DbType = DbType.String;
                    sqlParamNIK.Value = user.NIK;
                    cmd.Parameters.Add(sqlParamNIK);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.NIK = reader["NIK"].ToString();
                            result.Id = reader["Id"].ToString();
                            result.Nama = reader["Nama"].ToString();
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
         */

    }
}
