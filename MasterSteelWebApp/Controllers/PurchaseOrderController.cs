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
    public class PurchaseOrderController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllPurchaseOrder")]
        public IEnumerable<PurchaseOrder> GetAllPurchaseOrder(string token)
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllPurchaseOrderData();
            return result;
        }

        private List<PurchaseOrder> GetAllPurchaseOrderData()
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

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
                    cmd.CommandText = "spSelectAllPurchaseOrder";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PurchaseOrder reading = new PurchaseOrder();
                                reading.POId = reader["POId"].ToString();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.TanggalPO = Convert.ToDateTime(reader["TanggalPO"]).ToString("d MMM yyyy");
                                reading.POExpired = Convert.ToDateTime(reader["POExpired"]).ToString("d MMM yyyy");      
                         
                                decimal amountDecimal = Convert.ToDecimal(reader["Amount"]);
                                string outputAmount = amountDecimal.ToString("#,###.##");
                                reading.Amount = outputAmount;

                                decimal qtyDecimal = Convert.ToDecimal(reader["Quantity"]);
                                string outputQty = qtyDecimal.ToString("#,###.##");
                                reading.Quantity = outputQty;

                                reading.PIC = reader["Penjual"].ToString();

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
        [ActionName("GetAllPurchaseOrderByUserID")]
        public IEnumerable<PurchaseOrder> GetAllPurchaseOrderByUserID(string userId, string token)
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllPurchaseOrderByUserIDData(userId);
            return result;
        }

        private List<PurchaseOrder> GetAllPurchaseOrderByUserIDData(string userId)
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

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
                    cmd.CommandText = "spSelectAllPurchaseOrderByUserId";

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
                                PurchaseOrder reading = new PurchaseOrder();
                                reading.POId = reader["POId"].ToString();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.TanggalPO = Convert.ToDateTime(reader["TanggalPO"]).ToString("d MMM yyyy");
                                reading.POExpired = Convert.ToDateTime(reader["POExpired"]).ToString("d MMM yyyy");

                                decimal amountDecimal = Convert.ToDecimal(reader["Amount"]);
                                string outputAmount = amountDecimal.ToString("#,###.##");
                                reading.Amount = outputAmount;

                                decimal qtyDecimal = Convert.ToDecimal(reader["Quantity"]);
                                string outputQty = qtyDecimal.ToString("#,###.##");
                                reading.Quantity = outputQty;

                                reading.PIC = reader["Penjual"].ToString();

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
        [ActionName("GetPurchaseOrderByPOId")]
        public IEnumerable<PurchaseOrder> GetPurchaseOrderByPOId(string token, string poId)
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetPurchaseOrderByPOIdData(poId);
            return result;
        }

        private List<PurchaseOrder> GetPurchaseOrderByPOIdData(string poId)
        {
            List<PurchaseOrder> result;
            result = new List<PurchaseOrder>();

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
                    cmd.CommandText = "spSelectPurchaseOrderByPOId";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "poId";
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
                                PurchaseOrder reading = new PurchaseOrder();
                                reading.POId = reader["POId"].ToString();
                                reading.NomorPO = reader["NomorPO"].ToString();
                                reading.TanggalPO = Convert.ToDateTime(reader["TanggalPO"]).ToString("d MMM yyyy");
                                reading.POExpired = Convert.ToDateTime(reader["POExpired"]).ToString("d MMM yyyy");

                                decimal amountDecimal = Convert.ToDecimal(reader["Amount"]);
                                string outputAmount = amountDecimal.ToString("#,###.##");
                                reading.Amount = outputAmount;

                                decimal qtyDecimal = Convert.ToDecimal(reader["Quantity"]);
                                string outputQty = qtyDecimal.ToString("#,###.##");
                                reading.Quantity = outputQty;

                                reading.PIC = reader["Penjual"].ToString();
                                reading.DetailNamaBarang = reader["DetailNamaBarang"].ToString();

                                decimal detailQtyDecimal = Convert.ToDecimal(reader["DetailQuantity"]);
                                string outputDetailQty = detailQtyDecimal.ToString("#,###.##");
                                reading.DetailQuantity = outputDetailQty;

                                decimal detailHargaDecimal = Convert.ToDecimal(reader["DetailHarga"]);
                                string outputDetailHarga = detailHargaDecimal.ToString("#,###.##");
                                reading.DetailHarga = outputDetailHarga;                             

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
