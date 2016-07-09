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
    public class ClientProjectController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllClientProject")]
        public IEnumerable<ClientProject> GetAllProjectList(string token)
        {
            List<ClientProject> result;
            result = new List<ClientProject>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllClientProjectData();
            return result;
        }

        private List<ClientProject> GetAllClientProjectData()
        {
            List<ClientProject> result;
            result = new List<ClientProject>();

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
                    cmd.CommandText = "spGetAllClientProject";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ClientProject clientProject = new ClientProject();
                                clientProject.Id = reader["Id"].ToString();
                                clientProject.UserID = reader["UserID"].ToString();
                                clientProject.ProjectList = reader["ProjectList"].ToString();
                                clientProject.ProjectListName = reader["ProjectListName"].ToString();
                                clientProject.UserName = reader["UserName"].ToString();
                                clientProject.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]).ToString("dd MMMM yyyy");
                                result.Add(clientProject);
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
        [ActionName("GetClientProjectById")]
        public ClientProject GetClientProjectById(string token, string id)
        {
            ClientProject result;
            result = new ClientProject();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetClientProjectByIdData(id);
            return result;
        }

        private ClientProject GetClientProjectByIdData(string id)
        {
            ClientProject result;
            result = new ClientProject();

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
                    cmd.CommandText = "spGetClientProjectById";

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
                            result.UserID = reader["UserID"].ToString();
                            result.ProjectList = reader["ProjectList"].ToString();
                            result.ProjectListName = reader["ProjectListName"].ToString();
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
        [ActionName("InsertClientProject")]
        public String InsertClientProject([FromBody] ClientProject clientProject)
        {

            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(clientProject.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = InsertClientProjectData(clientProject);
            return result;
        }

        private String InsertClientProjectData(ClientProject clientProject)
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
                    cmd.CommandText = "spInsertClientProject";

                    SqlParameter sqlParamUserID = new SqlParameter();
                    sqlParamUserID.ParameterName = "userID";
                    sqlParamUserID.DbType = DbType.String;
                    sqlParamUserID.Value = clientProject.UserID;
                    cmd.Parameters.Add(sqlParamUserID);

                    SqlParameter sqlParamProjectList = new SqlParameter();
                    sqlParamProjectList.ParameterName = "projectList";
                    sqlParamProjectList.DbType = DbType.String;
                    sqlParamProjectList.Value = clientProject.ProjectList;
                    cmd.Parameters.Add(sqlParamProjectList);

                    SqlParameter sqlParamProjectListName = new SqlParameter();
                    sqlParamProjectListName.ParameterName = "projectListName";
                    sqlParamProjectListName.DbType = DbType.String;
                    sqlParamProjectListName.Value = clientProject.ProjectListName;
                    cmd.Parameters.Add(sqlParamProjectListName);

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
        [ActionName("UpdateClientProject")]
        public String UpdateClientProject([FromBody] ClientProject clientProject)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(clientProject.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = UpdateClientProjectData(clientProject);
            return result;
        }

        private String UpdateClientProjectData(ClientProject clientProject)
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
                    cmd.CommandText = "spUpdateClientProjectById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = clientProject.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamUserID = new SqlParameter();
                    sqlParamUserID.ParameterName = "userID";
                    sqlParamUserID.DbType = DbType.String;
                    sqlParamUserID.Value = clientProject.UserID;
                    cmd.Parameters.Add(sqlParamUserID);

                    SqlParameter sqlParamProjectList = new SqlParameter();
                    sqlParamProjectList.ParameterName = "projectList";
                    sqlParamProjectList.DbType = DbType.String;
                    sqlParamProjectList.Value = clientProject.ProjectList;
                    cmd.Parameters.Add(sqlParamProjectList);

                    SqlParameter sqlParamProjectListName = new SqlParameter();
                    sqlParamProjectListName.ParameterName = "projectListName";
                    sqlParamProjectListName.DbType = DbType.String;
                    sqlParamProjectListName.Value = clientProject.ProjectListName;
                    cmd.Parameters.Add(sqlParamProjectListName);

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
        [ActionName("DeleteClientProject")]
        public String DeleteClientProject([FromBody] ClientProject clientProject)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(clientProject.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = DeleteClientProjectData(clientProject.Id);
            return result;
        }

        private String DeleteClientProjectData(String id)
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
                    cmd.CommandText = "spDeleteClientProjectById";

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
