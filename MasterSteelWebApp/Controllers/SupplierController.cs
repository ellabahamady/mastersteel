using MasterSteelWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace MasterSteelWebApp.Controllers
{
    public class SupplierController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetSupplierByTypeSupplier")]
        public IEnumerable<MS_Supplier> GetSupplierByTypeSupplier(string token, string typeSupplier)
        {
            List<MS_Supplier> result;
            result = new List<MS_Supplier>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetSupplierDataByTypeSupplier(typeSupplier);
            return result;
        }

        private List<MS_Supplier> GetSupplierDataByTypeSupplier(string typeSupplier)
        {
            List<MS_Supplier> result;
            result = new List<MS_Supplier>();

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
                    cmd.CommandText = "spSelectSupplierByTypeSupplier";

                    SqlParameter sqlParamTypeSupplier = new SqlParameter();
                    sqlParamTypeSupplier.ParameterName = "typeSupplier";
                    sqlParamTypeSupplier.DbType = DbType.String;
                    sqlParamTypeSupplier.Value = typeSupplier;
                    cmd.Parameters.Add(sqlParamTypeSupplier);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_Supplier supplier = new MS_Supplier();
                                supplier.Supl_ID = reader["Supl_ID"].ToString();
                                supplier.Supl_Name = reader["Supl_Name"].ToString();

                                result.Add(supplier);
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
        [ActionName("GetAllSuppliers")]
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            List<Supplier> result;
            result = new List<Supplier>();

            result = SuppliersGetAllData();
            return result;
        }

        private List<Supplier> SuppliersGetAllData()
        {
            List<Supplier> result;
            result = new List<Supplier>();

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
                    cmd.CommandText = "spGetSuppliersAll";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Supplier supplier = new Supplier();
                                supplier.Id = reader["Id"].ToString();
                                supplier.Name = reader["Name"].ToString();

                                result.Add(supplier);
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
        [ActionName("GetSupplier")]
        public Supplier GetSupplier(string id)
        {
            Supplier result = new Supplier();
            result = SupplierGetById(id);
            return result;
        }

        private Supplier SupplierGetById(string id)
        {
            Supplier result;
            result = new Supplier();

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
                    cmd.CommandText = "spGetSupplierById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = id;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.Id = reader["Id"].ToString();
                            result.Name = reader["Name"].ToString();
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

        [HttpPost]
        [ActionName("SaveSupplier")]
        public String SaveSupplier([FromBody] Supplier supplier)
        {
            string result;
            result = SaveSupplierData(supplier);
            return result;
        }

        private String SaveSupplierData(Supplier supplier)
        {
            String result;
            result = "Fail";

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
                    cmd.CommandText = "spInsertSupplier";

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = supplier.Name;
                    cmd.Parameters.Add(sqlParamName);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";


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

        [HttpPut]
        [ActionName("UpdateSupplier")]
        public String UpdateSupplier([FromBody] Supplier supplier)
        {
            string result;
            result = UpdateSupplierData(supplier);
            return result;
        }

        private String UpdateSupplierData(Supplier supplier)
        {
            String result;
            result = "Fail";

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
                    cmd.CommandText = "spUpdateSupplierById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = supplier.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = supplier.Name;
                    cmd.Parameters.Add(sqlParamName);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

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

        [HttpDelete]
        [ActionName("DeleteSupplier")]
        public String DeleteSupplier(string id)
        {
            string result;
            result = DeleteSupplierData(id);
            return result;
        }

        private String DeleteSupplierData(string id)
        {
            String result;
            result = "Fail";

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
                    cmd.CommandText = "spDeleteSupplierById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = id;
                    cmd.Parameters.Add(sqlParamId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

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