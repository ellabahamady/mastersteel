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
    public class TrackingOrderController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllTrackingOrder")]
        public IEnumerable<TrackingOrder> GetAllTrackingOrder(string token)
        {
            List<TrackingOrder> result;
            result = new List<TrackingOrder>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllTrackingOrderData();
            return result;
        }

        private List<TrackingOrder> GetAllTrackingOrderData()
        {
            List<TrackingOrder> result;
            result = new List<TrackingOrder>();

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
                    cmd.CommandText = "spSelectTrackingOrder";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TrackingOrder reading = new TrackingOrder();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.NomorPickSlip = reader["PickSlipID"].ToString();
                                reading.TanggalPengiriman = Convert.ToDateTime(reader["TanggalPengiriman"]).ToString("d MMM yyyy");
                                reading.NomorKendaraan = reader["NomorKendaraan"].ToString();
                                reading.StatusOrderMuat = Convert.ToInt32(reader["StatusOrderMuat"]);
                                reading.OrderMuat = Convert.ToDateTime(reader["OrderMuat"]).ToString("d MMM yyyy hh:mm:ss");
                                reading.StatusGudang = Convert.ToInt32(reader["StatusGudang"]);
                                reading.Gudang = Convert.ToDateTime(reader["Gudang"]).ToString("d MMM yyyy hh:mm:ss");
                                reading.StatusSuratJalan = Convert.ToInt32(reader["StatusSuratJalan"]);
                                reading.SuratJalan = Convert.ToDateTime(reader["SuratJalan"]).ToString("d MMM yyyy hh:mm:ss");

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
        [ActionName("GetAllTrackingOrderByUserId")]
        public IEnumerable<TrackingOrder> GetAllTrackingOrderByUserId(string userId, string token)
        {
            List<TrackingOrder> result;
            result = new List<TrackingOrder>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllTrackingOrderByUserIdData(userId);
            return result;
        }

        private List<TrackingOrder> GetAllTrackingOrderByUserIdData(string userId)
        {
            List<TrackingOrder> result;
            result = new List<TrackingOrder>();

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
                    cmd.CommandText = "spSelectTrackingOrderByUserId";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "userId";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = userId;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TrackingOrder reading = new TrackingOrder();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.NomorPickSlip = reader["PickSlipID"].ToString();
                                reading.TanggalPengiriman = Convert.ToDateTime(reader["TanggalPengiriman"]).ToString("d MMM yyyy");
                                reading.NomorKendaraan = reader["NomorKendaraan"].ToString();
                                reading.StatusOrderMuat = Convert.ToInt32(reader["StatusOrderMuat"]);
                                reading.OrderMuat = Convert.ToDateTime(reader["OrderMuat"]).ToString("d MMM yyyy hh:mm:ss");
                                reading.StatusGudang = Convert.ToInt32(reader["StatusGudang"]);
                                reading.Gudang = Convert.ToDateTime(reader["Gudang"]).ToString("d MMM yyyy hh:mm:ss");
                                reading.StatusSuratJalan = Convert.ToInt32(reader["StatusSuratJalan"]);
                                reading.SuratJalan = Convert.ToDateTime(reader["SuratJalan"]).ToString("d MMM yyyy hh:mm:ss");

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
