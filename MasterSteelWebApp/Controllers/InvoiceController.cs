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
    public class InvoiceController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetFaktur3Hari")]
        public IEnumerable<Invoice> GetFaktur3Hari(string token)
        {
            List<Invoice> result;
            result = new List<Invoice>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetFaktur3HariData();
            return result;
        }

        private List<Invoice> GetFaktur3HariData()
        {
            List<Invoice> result;
            result = new List<Invoice>();

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
                    cmd.CommandText = "spSelectAllFaktur3Hari";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Invoice reading = new Invoice();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.NomorInvoice = reader["NomorInvoice"].ToString();
                                reading.NomorFaktur = reader["NomorFaktur"].ToString();
                                reading.TanggalInvoice = reader["TanggalInvoice"].ToString();
                                reading.TanggalTerima = reader["TanggalTerima"].ToString();
                                reading.TanggalJatuhTempo = reader["TanggalJatuhTempo"].ToString();
                                reading.Jumlah = Convert.ToSingle(reader["Jumlah"]);
                                reading.Pembayaran = Convert.ToSingle(reader["Pembayaran"]);
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
        [ActionName("GetFakturLebih1Hari")]
        public IEnumerable<Invoice> GetFakturLebih1Hari(string token)
        {
            List<Invoice> result;
            result = new List<Invoice>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetFakturLebih1HariData();
            return result;
        }

        private List<Invoice> GetFakturLebih1HariData()
        {
            List<Invoice> result;
            result = new List<Invoice>();

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
                    cmd.CommandText = "spSelectFakturLebih1Hari";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Invoice reading = new Invoice();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.NomorInvoice = reader["NomorInvoice"].ToString();
                                reading.NomorFaktur = reader["NomorFaktur"].ToString();
                                reading.TanggalInvoice = reader["TanggalInvoice"].ToString();
                                reading.TanggalTerima = reader["TanggalTerima"].ToString();
                                reading.TanggalJatuhTempo = reader["TanggalJatuhTempo"].ToString();
                                reading.Jumlah = Convert.ToSingle(reader["Jumlah"]);
                                reading.Pembayaran = Convert.ToSingle(reader["Pembayaran"]);
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
        [ActionName("GetFakturById")]
        public IEnumerable<Invoice> GetFakturById(string token, string nomorInvoice)
        {
            List<Invoice> result;
            result = new List<Invoice>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetFakturByIdData(nomorInvoice);
            return result;
        }

        private List<Invoice> GetFakturByIdData(string nomorInvoice)
        {
            List<Invoice> result;
            result = new List<Invoice>();

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
                    cmd.CommandText = "spSelectFakturById";

                    SqlParameter sqlParamType = new SqlParameter();
                    sqlParamType.ParameterName = "nomorInvoice";
                    sqlParamType.DbType = DbType.String;
                    sqlParamType.Value = nomorInvoice;
                    cmd.Parameters.Add(sqlParamType);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Invoice reading = new Invoice();
                                reading.ContractID = reader["ContractID"].ToString();
                                reading.ContractNumber = reader["ContractNumber"].ToString();
                                reading.NomorInvoice = reader["NomorInvoice"].ToString();
                                reading.NomorFaktur = reader["NomorFaktur"].ToString();
                                reading.TanggalInvoice = reader["TanggalInvoice"].ToString();
                                reading.TanggalTerima = reader["TanggalTerima"].ToString();
                                reading.TanggalJatuhTempo = reader["TanggalJatuhTempo"].ToString();
                                reading.Jumlah = Convert.ToSingle(reader["Jumlah"]);
                                reading.Pembayaran = Convert.ToSingle(reader["Pembayaran"]);
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
