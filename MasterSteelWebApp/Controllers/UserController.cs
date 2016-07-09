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
    public class UserController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        bool verifyEmail;

        [HttpGet]
        [ActionName("GetAllUsers")]
        public IEnumerable<MS_Users> GetAllUsers(string token)
        {
            List<MS_Users> result;
            result = new List<MS_Users>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = UsersGetAllData();
            return result;
        }

        private List<MS_Users> UsersGetAllData()
        {
            List<MS_Users> result;
            result = new List<MS_Users>();

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
                    cmd.CommandText = "spGetUsersAll";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                MS_Users user = new MS_Users();
                                user.UserGenerateID = reader["UserGenerateID"].ToString();
                                user.UserID = reader["UserID"].ToString();
                                user.UserName = reader["UserName"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.Mail = reader["Mail"].ToString();
                                user.Type = reader["Type"].ToString();
                                result.Add(user);
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
        [ActionName("ChangeToken")]
        public String ChangeToken(string userID, string token)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = ChangeTokenData(userID);
            return result;
        }

        private String ChangeTokenData(string userID)
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
                    cmd.CommandText = "spUserChangeToken";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "userID";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = userID;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader["Result"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = "Fail";
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
        [ActionName("ValidateToken")]
        public String ValidateToken(string token)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = ValidateTokenData(token);
            return result;
        }

        private String ValidateTokenData(string token)
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
                    cmd.CommandText = "spValidateUserToken";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "token";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = token;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader["Result"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = "Fail";
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
        [ActionName("GetAllClient")]
        public IEnumerable<MS_Users> GetAllClient(string token)
        {
            List<MS_Users> result;
            result = new List<MS_Users>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllClientData();
            return result;
        }

        private List<MS_Users> GetAllClientData()
        {
            List<MS_Users> result;
            result = new List<MS_Users>();

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
                    cmd.CommandText = "spGetClient";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                MS_Users user = new MS_Users();
                                user.UserGenerateID = reader["UserGenerateID"].ToString();
                                user.UserID = reader["UserID"].ToString();
                                user.UserName = reader["UserName"].ToString();
                                result.Add(user);
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
        [ActionName("GetUser")]
        public MS_Users GetUser(string userGenerateID)
        {
            MS_Users result = new MS_Users();
            result = UserGetById(userGenerateID);
            return result;
        }

        private MS_Users UserGetById(string userGenerateID)
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
                    cmd.CommandText = "spGetUserById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "userGenerateID";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = userGenerateID;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.UserGenerateID = reader["UserGenerateID"].ToString();
                            result.UserID = reader["UserID"].ToString();
                            result.UserName = reader["UserName"].ToString();
                            result.Password = reader["Password"].ToString();
                            result.Mail = reader["Mail"].ToString();
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

        [HttpPost]
        [ActionName("SaveUser")]
        public String SaveUser([FromBody] MS_Users user)
        {

            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(user.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = SaveUserData(user);
            return result;
        }

        private String SaveUserData(MS_Users user)
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
                    cmd.CommandText = "spInsertUser";

                    SqlParameter sqlParamUserID = new SqlParameter();
                    sqlParamUserID.ParameterName = "userID";
                    sqlParamUserID.DbType = DbType.String;
                    sqlParamUserID.Value = user.UserID;
                    cmd.Parameters.Add(sqlParamUserID);

                    SqlParameter sqlParamUserName = new SqlParameter();
                    sqlParamUserName.ParameterName = "userName";
                    sqlParamUserName.DbType = DbType.String;
                    sqlParamUserName.Value = user.UserName;
                    cmd.Parameters.Add(sqlParamUserName);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    SqlParameter sqlParamMail = new SqlParameter();
                    sqlParamMail.ParameterName = "mail";
                    sqlParamMail.DbType = DbType.String;
                    sqlParamMail.Value = user.Mail;
                    cmd.Parameters.Add(sqlParamMail);

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "type";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = user.Type;
                    cmd.Parameters.Add(sqlParamType);

                    SqlParameter sqlParamUserUpdateID = new SqlParameter();
                    sqlParamUserUpdateID.ParameterName = "userUpdateID";
                    sqlParamUserUpdateID.DbType = DbType.String;
                    sqlParamUserUpdateID.Value = user.UserUpdateID;
                    cmd.Parameters.Add(sqlParamUserUpdateID);

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
                    //ResendVerifyEmail(user);

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }


        [HttpPut]
        [ActionName("ChangePassword")]
        public String ChangePassword([FromBody] MS_Users user)
        {

            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(user.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = ChangePasswordData(user);
            return result;
        }

        private String ChangePasswordData(MS_Users user)
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
                    cmd.CommandText = "spUpdateUserChangePassword";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userID";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = user.UserID;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamOldPassword = new SqlParameter();
                    sqlParamOldPassword.ParameterName = "oldPassword";
                    sqlParamOldPassword.DbType = DbType.String;
                    sqlParamOldPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamOldPassword);

                    SqlParameter sqlParamNewPassword = new SqlParameter();
                    sqlParamNewPassword.ParameterName = "newPassword";
                    sqlParamNewPassword.DbType = DbType.String;
                    sqlParamNewPassword.Value = user.NewPassword;
                    cmd.Parameters.Add(sqlParamNewPassword);

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
                    //ResendVerifyEmail(user);

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpPut]
        [ActionName("ChangePasswordMobile")]
        public MS_Users ChangePasswordMobile([FromBody] MS_Users user)
        {

            MS_Users result;
            result = new MS_Users();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(user.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = ChangePasswordMobileData(user);
            //            result = user;
            return result;
        }

        private MS_Users ChangePasswordMobileData(MS_Users user)
        {
            //String result;
            //result = "Fail";

            MS_Users result;
            result = new MS_Users();
            result.UserID = "Fail";

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
                    cmd.CommandText = "spUpdateUserChangePassword";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userID";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = user.UserID;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamOldPassword = new SqlParameter();
                    sqlParamOldPassword.ParameterName = "oldPassword";
                    sqlParamOldPassword.DbType = DbType.String;
                    sqlParamOldPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamOldPassword);

                    SqlParameter sqlParamNewPassword = new SqlParameter();
                    sqlParamNewPassword.ParameterName = "newPassword";
                    sqlParamNewPassword.DbType = DbType.String;
                    sqlParamNewPassword.Value = user.NewPassword;
                    cmd.Parameters.Add(sqlParamNewPassword);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.UserID = "Success";
                        }
                    }

                }
                catch (Exception ex)
                {
                    //                    throw ex;
                    result.UserID = ex.Message;
                }
                finally
                {
                    //ResendVerifyEmail(user);

                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

            }

            return result;

        }

        [HttpPut]
        [ActionName("UpdateData")]
        public String UpdateData([FromBody] MS_Users user)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(user.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = UpdateUser(user);
            return result;
        }

        private String UpdateUser(MS_Users user)
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
                    cmd.CommandText = "spUpdateUserById";

                    SqlParameter sqlParamUserGenerateID = new SqlParameter();
                    sqlParamUserGenerateID.ParameterName = "userGenerateID";
                    sqlParamUserGenerateID.DbType = DbType.String;
                    sqlParamUserGenerateID.Value = user.UserGenerateID;
                    cmd.Parameters.Add(sqlParamUserGenerateID);

                    SqlParameter sqlParamUserID = new SqlParameter();
                    sqlParamUserID.ParameterName = "userID";
                    sqlParamUserID.DbType = DbType.String;
                    sqlParamUserID.Value = user.UserID;
                    cmd.Parameters.Add(sqlParamUserID);

                    SqlParameter sqlParamUserName = new SqlParameter();
                    sqlParamUserName.ParameterName = "userName";
                    sqlParamUserName.DbType = DbType.String;
                    sqlParamUserName.Value = user.UserName;
                    cmd.Parameters.Add(sqlParamUserName);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    SqlParameter sqlParamMail = new SqlParameter();
                    sqlParamMail.ParameterName = "mail";
                    sqlParamMail.DbType = DbType.String;
                    sqlParamMail.Value = user.Mail;
                    cmd.Parameters.Add(sqlParamMail);

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "type";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = user.Type;
                    cmd.Parameters.Add(sqlParamType);

                    SqlParameter sqlParamUserUpdateID = new SqlParameter();
                    sqlParamUserUpdateID.ParameterName = "userUpdateID";
                    sqlParamUserUpdateID.DbType = DbType.String;
                    sqlParamUserUpdateID.Value = user.UserUpdateID;
                    cmd.Parameters.Add(sqlParamUserUpdateID);

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
        public String DeleteData(string userGenerateID, string token)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = DeleteUser(userGenerateID);
            return result;
        }

        private String DeleteUser(string userGenerateID)
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
                    cmd.CommandText = "spDeleteUserById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "userGenerateID";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = userGenerateID;
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


        [HttpGet]
        [ActionName("CheckUsername")]
        public Result CheckUsername(string userName)
        {
            Result result = new Result();
            result = CheckUsernameData(userName);
            return result;
        }

        private Result CheckUsernameData(string userName)
        {
            Result result;
            result = new Result();

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
                    cmd.CommandText = "spCheckUsername";

                    SqlParameter sqlParamEmail = new SqlParameter();
                    sqlParamEmail.ParameterName = "userName";
                    sqlParamEmail.DbType = DbType.String;
                    sqlParamEmail.Value = userName;
                    cmd.Parameters.Add(sqlParamEmail);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.ResultStr = reader["Result"].ToString();
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

        [HttpPut]
        [ActionName("UpdateUserPassword")]
        public String UpdateUserPassword([FromBody] User user)
        {
            string result;
            result = UpdateUserPasswordData(user);
            return result;
        }

        private String UpdateUserPasswordData(User user)
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
                    cmd.CommandText = "spUpdatePasswordUser";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = user.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamPassword = new SqlParameter();
                    sqlParamPassword.ParameterName = "password";
                    sqlParamPassword.DbType = DbType.String;
                    sqlParamPassword.Value = user.Password;
                    cmd.Parameters.Add(sqlParamPassword);

                    SqlParameter sqlParamNewPassword = new SqlParameter();
                    sqlParamNewPassword.ParameterName = "newPassword";
                    sqlParamNewPassword.DbType = DbType.String;
                    sqlParamNewPassword.Value = user.NewPassword;
                    cmd.Parameters.Add(sqlParamNewPassword);

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

        [HttpPost]
        [ActionName("ResendVerifyEmail")]
        public String ResendVerifyEmail([FromBody] User user)
        {
            verifyEmail = true;
            string result;
            result = "ok";
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(new MailAddress(user.Email));
                //                mm.From = new MailAddress("eko.prasetio.rahmat@gmail.com");
                mm.From = new MailAddress("eko_prasetio_rahmat@yahoo.co.id");
                mm.Subject = "Verifikasi Email";
                //                mm.Body = "User Account anda telah dibuat untuk Login menggunakan Aplikasi Smart Surveillance <br>NIK : " + user.NIK + " <br>Password: " + user.Password + " <br><a href=\"http://localhost:5842/Verification.html?nik=" + user.NIK + "&verify=AccountActive\">klik disini untuk verifikasi</a>";
                mm.Body = "User Account anda telah dibuat untuk Login menggunakan Aplikasi Smart Surveillance <br>NIK : " + user.NIK + " <br>Password: " + user.Password + " <br><a href=\"http://mastersteelwebapp.azurewebsites.net/Verification.html?nik=" + user.NIK + "&verify=AccountActive\">klik disini untuk verifikasi</a>";
                mm.IsBodyHtml = true;
                SmtpClient smcl = new SmtpClient();
                //                smcl.Host = "smtp.gmail.com";
                smcl.Host = "smtp.mail.yahoo.com";
                smcl.Port = 587;
                //                smcl.Credentials = new NetworkCredential("eko.prasetio.rahmat@gmail.com", "pass@word10300462331");
                //                smcl.Credentials = new NetworkCredential("eko.prasetio.rahmat@gmail.com", "hnnxcykdnfkxjczs");
                smcl.Credentials = new NetworkCredential("eko_prasetio_rahmat@yahoo.co.id", "Pass@word10300462331");
                smcl.EnableSsl = true;
                smcl.Send(mm);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            //            result = SaveUserData(user);
            return result;
        }

        [HttpPut]
        [ActionName("VerifyEmail")]
        public String VerifyEmail([FromBody] User user)
        {
            string result;
            result = UpdateUserEmailVerifiedByNIK(user.NIK);
            return result;
        }

        private String UpdateUserEmailVerifiedByNIK(string nik)
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
                    cmd.CommandText = "spUpdateUserEmailVerified";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "nik";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = nik;
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
