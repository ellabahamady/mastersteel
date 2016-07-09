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
    public class StatusProjectController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllStatusProject")]
        public IEnumerable<MS_StatusProject> GetAllStatusProject(string token)
        {
            List<MS_StatusProject> result;
            result = new List<MS_StatusProject>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllDataStatusProject();
            return result;
        }

        private List<MS_StatusProject> GetAllDataStatusProject()
        {
            List<MS_StatusProject> result;
            result = new List<MS_StatusProject>();

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
                    cmd.CommandText = "spGetAllStatusProject";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_StatusProject statusProject = new MS_StatusProject();
                                statusProject.StatusProjectID = reader["StatusProjectID"].ToString();
                                statusProject.Description = reader["Description"].ToString();
                                statusProject.Type = Convert.ToInt32(reader["Type"]);

                                result.Add(statusProject);
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
        [ActionName("GetStatusProjectByType")]
        public IEnumerable<MS_StatusProject> GetStatusProjectByType(string token, int type)
        {
            List<MS_StatusProject> result;
            result = new List<MS_StatusProject>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetStatusProjectDataByType(type);
            return result;
        }

        private List<MS_StatusProject> GetStatusProjectDataByType(int type)
        {
            List<MS_StatusProject> result;
            result = new List<MS_StatusProject>();

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
                    cmd.CommandText = "spSelectStatusProjectByType";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "type";
                    sqlParamType.DbType = DbType.Int32;
                    sqlParamType.Value = type;
                    cmd.Parameters.Add(sqlParamType);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_StatusProject statusProject = new MS_StatusProject();
                                statusProject.StatusProjectID = reader["StatusProjectID"].ToString();
                                statusProject.Description = reader["Description"].ToString();

                                result.Add(statusProject);
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
