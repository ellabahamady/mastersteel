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
    public class PickSlipController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllPickSlipByPOId")]
        public IEnumerable<PickSlip> GetAllPickSlipByPOId(string token, string poId)
        {
            List<PickSlip> result;
            result = new List<PickSlip>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllPickSlipByPOIdData(poId);
            return result;
        }

        private List<PickSlip> GetAllPickSlipByPOIdData(string poId)
        {
            List<PickSlip> result;
            result = new List<PickSlip>();

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
                    cmd.CommandText = "spSelectAllPickSlipByPOId";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "POId";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = poId;
                    cmd.Parameters.Add(sqlParamType);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PickSlip reading = new PickSlip();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.NomorPickSlip = reader["PickSlipID"].ToString();
                                reading.TanggalPengiriman = Convert.ToDateTime(reader["TanggalPengiriman"]).ToString("d MMM yyyy");
                                reading.NomorKendaraan = reader["NomorKendaraan"].ToString();

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
        [ActionName("GetPickSlipByPickSlipId")]
        public IEnumerable<PickSlip> GetPickSlipByPickSlipId(string token, string pickSlipId)
        {
            List<PickSlip> result;
            result = new List<PickSlip>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetPickSlipByPickSlipIdData(pickSlipId);
            return result;
        }

        private List<PickSlip> GetPickSlipByPickSlipIdData(string pickSlipId)
        {
            List<PickSlip> result;
            result = new List<PickSlip>();

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
                    cmd.CommandText = "spSelectPickSlipByPickSlipId";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "pickSlipId";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = pickSlipId;
                    cmd.Parameters.Add(sqlParamType);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PickSlip reading = new PickSlip();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.NomorPickSlip = reader["PickSlipID"].ToString();
                                reading.TanggalPengiriman = reader["TanggalPengiriman"].ToString();
                                reading.NomorKendaraan = reader["NomorKendaraan"].ToString();
                                reading.DetailNamaBarang = reader["DetailNamaBarang"].ToString();

                                decimal detailQtyDecimal = Convert.ToDecimal(reader["DetailQuantity"]);
                                string outputDetailQty = detailQtyDecimal.ToString("#,###.##");
                                reading.DetailQuantity = outputDetailQty;

                                reading.DetailSatuan = reader["DetailSatuan"].ToString();

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
