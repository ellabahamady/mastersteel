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
    public class ContractPOController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetContractOrderAfter14Days")]
        public IEnumerable<ContractPO> GetContractOrderAfter14Days(string token)
        {
            List<ContractPO> result;
            result = new List<ContractPO>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetContractOrderAfter14DaysData();
            return result;
        }

        private List<ContractPO> GetContractOrderAfter14DaysData()
        {
            List<ContractPO> result;
            result = new List<ContractPO>();

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
                    cmd.CommandText = "spSelectContractOrderAfter14Days";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ContractPO reading = new ContractPO();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.ContractDate = reader["ContractDate"].ToString();
                                reading.POId = reader["POId"].ToString();
                                reading.TanggalPengiriman = reader["TanggalPengiriman"].ToString();
                                result.Add(reading);
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
