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
    public class CompetitorController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetCompetitorByType")]
        public IEnumerable<MS_Competitor> GetCompetitorByType(string token, int type)
        {
            List<MS_Competitor> result;
            result = new List<MS_Competitor>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetCompetitorDataByType(type);
            return result;
        }

        private List<MS_Competitor> GetCompetitorDataByType(int type)
        {
            List<MS_Competitor> result;
            result = new List<MS_Competitor>();

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
                    cmd.CommandText = "spSelectCompetitorByType";

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
                                MS_Competitor competitor = new MS_Competitor();
                                competitor.CompetitorID = reader["CompetitorID"].ToString();
                                competitor.Description = reader["Description"].ToString();

                                result.Add(competitor);
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
