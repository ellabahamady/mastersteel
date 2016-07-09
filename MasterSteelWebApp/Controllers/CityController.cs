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
    public class CityController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetCityByProvinsiID")]
        public IEnumerable<MS_City> GetCityByProvinsiID(string token, string provinsiID)
        {
            List<MS_City> result;
            result = new List<MS_City>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetCityDataByProvinsiID(provinsiID);
            return result;
        }

        private List<MS_City> GetCityDataByProvinsiID(string provinsiID)
        {
            List<MS_City> result;
            result = new List<MS_City>();

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
                    cmd.CommandText = "spSelectCityByProvinsiID";

                    SqlParameter sqlParamProvinsiID = new SqlParameter();
                    sqlParamProvinsiID.ParameterName = "provinsiID";
                    sqlParamProvinsiID.DbType = DbType.String;
                    sqlParamProvinsiID.Value = provinsiID;
                    cmd.Parameters.Add(sqlParamProvinsiID);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_City city = new MS_City();
                                city.CityID = reader["CityID"].ToString();
                                city.Description = reader["Description"].ToString();

                                result.Add(city);
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
        [ActionName("GetAllCity")]
        public IEnumerable<MS_City> GetAllCity(string token)
        {
            List<MS_City> result;
            result = new List<MS_City>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllDataCity();
            return result;
        }

        private List<MS_City> GetAllDataCity()
        {
            List<MS_City> result;
            result = new List<MS_City>();

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
                    cmd.CommandText = "spGetAllCity";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MS_City city = new MS_City();
                                city.CityID = reader["CityID"].ToString();
                                city.Description = reader["Description"].ToString();
                                city.KabupatenID = reader["KabupatenID"].ToString();
                                city.ProvinsiID = reader["ProvinsiID"].ToString();

                                result.Add(city);
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
        [ActionName("GetAllCities")]
        public IEnumerable<City> GetAllCities()
        {
            List<City> result;
            result = new List<City>();

            result = CitiesGetAllData();
            return result;
        }

        private List<City> CitiesGetAllData()
        {
            List<City> result;
            result = new List<City>();

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
                    cmd.CommandText = "spGetCitiesAll";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                City city = new City();
                                city.Id = reader["Id"].ToString();
                                city.Name = reader["Name"].ToString();
                                city.ProvinceId = reader["ProvinceId"].ToString();
                                city.ProvinceName = reader["ProvinceName"].ToString();
                                result.Add(city);
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
        [ActionName("GetCity")]
        public City GetCity(string id)
        {
            City result = new City();
            result = CityGetById(id);
            return result;
        }

        private City CityGetById(string id)
        {
            City result;
            result = new City();

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
                    cmd.CommandText = "spGetCityById";

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
                            result.ProvinceId = reader["ProvinceId"].ToString();
                            result.ProvinceName = reader["ProvinceName"].ToString();
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
        [ActionName("GetCityByProvince")]
        public IEnumerable<City> GetCityByProvince(string provinceId)
        {
            List<City> result;
            result = new List<City>();

            result = CityGetByProvinceId(provinceId);
            return result;
        }

        private List<City> CityGetByProvinceId(string provinceId)
        {
            List<City> result;
            result = new List<City>();

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
                    cmd.CommandText = "spGetCityByProvince";

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = provinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                City city = new City();
                                city.Id = reader["Id"].ToString();
                                city.Name = reader["Name"].ToString();
                                result.Add(city);
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
        [HttpPost]
        [ActionName("SaveCity")]
        public String SaveCity([FromBody] City city)
        {
            string result;
            result = SaveCityData(city);
            return result;
        }

        private String SaveCityData(City city)
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
                    cmd.CommandText = "spInsertCity";

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = city.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = city.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

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
        [ActionName("UpdateCity")]
        public String UpdateCity([FromBody] City city)
        {
            string result;
            result = UpdateCityData(city);
            return result;
        }

        private String UpdateCityData(City city)
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
                    cmd.CommandText = "spUpdateCityById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = city.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = city.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = city.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

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
        [ActionName("DeleteCity")]
        public String DeleteCity(string id)
        {
            string result;
            result = DeleteCityData(id);
            return result;
        }

        private String DeleteCityData(string id)
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
                    cmd.CommandText = "spDeleteCityById";

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