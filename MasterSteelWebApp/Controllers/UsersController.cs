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
    public class UsersController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetUserLoginMobile")]
        public MS_Users GetUserLoginMobile(string userName, string password)
        {
            MS_Users result;
            result = new MS_Users();

            result = GetUserLoginMobileData(userName, password);
            return result;
        }

        private MS_Users GetUserLoginMobileData(string userName, string password)
        {
            MS_Users result;
            result = new MS_Users();

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
                    cmd.CommandText = "spLoginUserMobile";

                    SqlParameter sqlParamUserName = new SqlParameter();
                    sqlParamUserName.ParameterName = "userName";
                    sqlParamUserName.DbType = DbType.String;
                    sqlParamUserName.Value = userName;
                    cmd.Parameters.Add(sqlParamUserName);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = password;
                    cmd.Parameters.Add(sqlParamPassword);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.Token = reader["Token"].ToString();
                            result.UserID = reader["UserID"].ToString();
                            result.UserName = reader["UserName"].ToString();
                            result.Status = reader["Status"].ToString();
                            result.Mail = reader["Mail"].ToString();
                            result.VerifikasiMail = Convert.ToBoolean( reader["VerifikasiMail"] );
                            result.Type = reader["Type"].ToString();
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
        [ActionName("CheckToken")]
        public int CheckToken(string token)
        {
            int result = 0;

            result = CheckTokenData(token);
            return result;
        }

        private int CheckTokenData(string token)
        {
            int result = 0;

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
                    cmd.CommandText = "spCheckToken";

                    SqlParameter sqlParamToken = new SqlParameter();
                    sqlParamToken.ParameterName = "token";
                    sqlParamToken.DbType = DbType.String;
                    sqlParamToken.Value = token;
                    cmd.Parameters.Add(sqlParamToken);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = Convert.ToInt32( reader["Total"] ) ;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result = 0;
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
