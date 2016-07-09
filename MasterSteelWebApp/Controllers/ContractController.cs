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
    public class ContractController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetContractBy30DaysBeforeExpiredDate")]
        public IEnumerable<Contract> GetContractBy30DaysBeforeExpiredDate(string token)
        {
            List<Contract> result;
            result = new List<Contract>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetContractBy30DaysBeforeExpiredDateData();
            return result;
        }

        private List<Contract> GetContractBy30DaysBeforeExpiredDateData()
        {
            List<Contract> result;
            result = new List<Contract>();

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
                    cmd.CommandText = "spSelectContractBy30DaysBeforeExpiredDate";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Contract reading = new Contract();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.ProjectName = reader["ProjectName"].ToString();
                                reading.ContractDate = reader["ContractDate"].ToString();
                                reading.ExpiredDate = reader["ExpiredDate"].ToString();
                                reading.Amount = Convert.ToSingle(reader["Amount"]);
                                reading.Quantity = Convert.ToSingle(reader["Quantity"]);
                                reading.Satuan = reader["Satuan"].ToString();
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

        [HttpGet]
        [ActionName("GetContractOrderAfter14Days")]
        public IEnumerable<Contract> GetContractOrderAfter14Days(string token)
        {
            List<Contract> result;
            result = new List<Contract>();

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

        private List<Contract> GetContractOrderAfter14DaysData()
        {
            List<Contract> result;
            result = new List<Contract>();

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
                    cmd.CommandText = "spSelectContractBy30DaysBeforeExpiredDate";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Contract reading = new Contract();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.ProjectName = reader["ProjectName"].ToString();
                                reading.ContractDate = reader["ContractDate"].ToString();
                                reading.ExpiredDate = reader["ExpiredDate"].ToString();
                                reading.Amount = Convert.ToSingle(reader["Amount"]);
                                reading.Quantity = Convert.ToSingle(reader["Quantity"]);
                                reading.Satuan = reader["Satuan"].ToString();
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

        [HttpGet]
        [ActionName("GetContractById")]
        public IEnumerable<Contract> GetContractById(string token, string contractID)
        {
            List<Contract> result;
            result = new List<Contract>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetContractByIdData(contractID);
            return result;
        }

        private List<Contract> GetContractByIdData(string contractID)
        {
            List<Contract> result;
            result = new List<Contract>();

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
                    cmd.CommandText = "spSelectContractById";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "contractID";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = contractID;
                    cmd.Parameters.Add(sqlParamType);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Contract reading = new Contract();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.ProjectName = reader["ProjectName"].ToString();
                                reading.ContractDate = reader["ContractDate"].ToString();
                                reading.ExpiredDate = reader["ExpiredDate"].ToString();
                                reading.Amount = Convert.ToSingle(reader["Amount"]);
                                reading.Quantity = Convert.ToSingle(reader["Quantity"]);
                                reading.Satuan = reader["Satuan"].ToString();
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
