using MasterSteelWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MasterSteelWebApp.Controllers
{
    public class ProjectCategoriesController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetProjectCategories")]
        public IEnumerable<MS_ProjectCategories> GetProjectCategories(string token)
        {
            List<MS_ProjectCategories> result;
            result = new List<MS_ProjectCategories>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if(checkToken == 0)
            {
                return result;
            }

            result = GetProjectCategoriesData();
            return result;
        }

        private List<MS_ProjectCategories> GetProjectCategoriesData()
        {
            List<MS_ProjectCategories> result;
            result = new List<MS_ProjectCategories>();

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
                    cmd.CommandText = "spSelectProjectCategories";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_ProjectCategories projectCategories = new MS_ProjectCategories();
                                projectCategories.ProjectCategoriesID = reader["ProjectCategoriesID"].ToString();
                                projectCategories.Description = reader["Description"].ToString();

                                result.Add(projectCategories);
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
