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
    public class SalesController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetSales")]
        public IEnumerable<MS_Sales> GetSales(string token)
        {
            List<MS_Sales> result;
            result = new List<MS_Sales>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetSalesData();
            return result;
        }

        private List<MS_Sales> GetSalesData()
        {
            List<MS_Sales> result;
            result = new List<MS_Sales>();

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
                    cmd.CommandText = "spSelectSales";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_Sales sales = new MS_Sales();
                                sales.SalesID = reader["SalesID"].ToString();
                                sales.Penjual = reader["Penjual"].ToString();

                                result.Add(sales);
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
