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
    public class ProjectTempController : ApiController
    {
        private DbProviderFactory dbProviderFactory;


        [HttpGet]
        [ActionName("GetAllProjects")]
        public IEnumerable<ProjectTemp> GetAllProjects(string userId)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

            result = ProjectsGetAllData(userId);
            return result;
        }

        private List<ProjectTemp> ProjectsGetAllData(string userId)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

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
                    cmd.CommandText = "spGetMyProjectsAllWeb";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = userId;
                    cmd.Parameters.Add(sqlParamUserId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ProjectTemp project = new ProjectTemp();
                                project.Id = reader["Id"].ToString();
                                project.Nomor = reader["Nomor"].ToString();
                                project.Name = reader["Name"].ToString();
                                project.RangeDateStart = reader["RangeDateStart"].ToString();
                                project.RangeDateEnd = reader["RangeDateEnd"].ToString();
                                project.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                                project.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                                project.Latitude = float.Parse(reader["Latitude"].ToString());
                                project.Longitude = float.Parse(reader["Longitude"].ToString());

                                result.Add(project);
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
        [ActionName("GetOtherProjects")]
        public IEnumerable<ProjectTemp> GetOtherProjects(string userId)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

            result = ProjectsGetOtherData(userId);
            return result;
        }

        private List<ProjectTemp> ProjectsGetOtherData(string userId)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

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
                    cmd.CommandText = "spGetOtherProjectsAllWeb";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = userId;
                    cmd.Parameters.Add(sqlParamUserId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ProjectTemp project = new ProjectTemp();
                                project.Id = reader["Id"].ToString();
                                project.Nomor = reader["Nomor"].ToString();
                                project.Name = reader["Name"].ToString();
                                project.RangeDateStart = reader["RangeDateStart"].ToString();
                                project.RangeDateEnd = reader["RangeDateEnd"].ToString();
                                project.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                                project.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                                project.Latitude = float.Parse(reader["Latitude"].ToString());
                                project.Longitude = float.Parse(reader["Longitude"].ToString());

                                result.Add(project);
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
        [ActionName("GetListProjectsAndroid")]
        public IEnumerable<Data> GetListProjectsAndroid(string userId)
        {
            List<Data> result;
            result = new List<Data>();

            result = ProjectsGetListProjectsAndroid(userId);
            return result;
        }

        private List<Data> ProjectsGetListProjectsAndroid(String userId)
        {
            List<Data> result;
            result = new List<Data>();

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
                    cmd.CommandText = "spGetProjectsListAndroid";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = userId;
                    cmd.Parameters.Add(sqlParamUserId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Data data = new Data();
                                data.Data0 = reader["Id"].ToString();
                                data.Data3 = reader["Name"].ToString();
                                data.Data9 = reader["CreatedAt"].ToString();
                                data.Data12 = reader["ImageFile"].ToString();

                                result.Add(data);

                                //Data project = new Project();
                                //project.Id = reader["Id"].ToString();
                                //project.Name = reader["Name"].ToString();
                                //project.CreatedAt = reader["CreatedAt"].ToString();
                                //project.ImageFile = reader["ImageFile"].ToString();

                                //result.Add(project);
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
        [ActionName("GetListProjectsByUserIdWithPaging")]
        public IEnumerable<ProjectTemp> GetListProjectsByUserIdWithPaging(string userId, int pageNumber, int pageSize)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

            result = ProjectsGetListProjectsByUserIdWithPaging(userId, pageNumber, pageSize);
            return result;
        }

        private List<ProjectTemp> ProjectsGetListProjectsByUserIdWithPaging(String userId, int pageNumber, int pageSize)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

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
                    cmd.CommandText = "spGetProjectsListByUserIdWithPaging";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = userId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamPageNumber = new SqlParameter();
                    sqlParamPageNumber.ParameterName = "pageNumber";
                    sqlParamPageNumber.DbType = DbType.Int32;
                    sqlParamPageNumber.Value = pageNumber;
                    cmd.Parameters.Add(sqlParamPageNumber);

                    SqlParameter sqlParamPageSize = new SqlParameter();
                    sqlParamPageSize.ParameterName = "pageSize";
                    sqlParamPageSize.DbType = DbType.Int32;
                    sqlParamPageSize.Value = pageSize;
                    cmd.Parameters.Add(sqlParamPageSize);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ProjectTemp data = new ProjectTemp();
                                data.Id = reader["Id"].ToString();
                                data.Name = reader["Name"].ToString();
                                data.CreatedAt = reader["CreatedAt"].ToString();
                                data.ImageFile = reader["ImageFile"].ToString();

                                result.Add(data);

                                //Data project = new Project();
                                //project.Id = reader["Id"].ToString();
                                //project.Name = reader["Name"].ToString();
                                //project.CreatedAt = reader["CreatedAt"].ToString();
                                //project.ImageFile = reader["ImageFile"].ToString();

                                //result.Add(project);
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
        [ActionName("GetListOtherProjectsByUserIdWithPaging")]
        public IEnumerable<ProjectTemp> GetListOtherProjectsByUserIdWithPaging(string userId, int pageNumber, int pageSize)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

            result = ProjectsGetListOtherProjectsByUserIdWithPaging(userId, pageNumber, pageSize);
            return result;
        }

        private List<ProjectTemp> ProjectsGetListOtherProjectsByUserIdWithPaging(String userId, int pageNumber, int pageSize)
        {
            List<ProjectTemp> result;
            result = new List<ProjectTemp>();

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
                    cmd.CommandText = "spGetOtherProjectsListByUserIdWithPaging";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = userId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamPageNumber = new SqlParameter();
                    sqlParamPageNumber.ParameterName = "pageNumber";
                    sqlParamPageNumber.DbType = DbType.Int32;
                    sqlParamPageNumber.Value = pageNumber;
                    cmd.Parameters.Add(sqlParamPageNumber);

                    SqlParameter sqlParamPageSize = new SqlParameter();
                    sqlParamPageSize.ParameterName = "pageSize";
                    sqlParamPageSize.DbType = DbType.Int32;
                    sqlParamPageSize.Value = pageSize;
                    cmd.Parameters.Add(sqlParamPageSize);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ProjectTemp data = new ProjectTemp();
                                data.Id = reader["Id"].ToString();
                                data.Name = reader["Name"].ToString();
                                data.CreatedAt = reader["CreatedAt"].ToString();
                                data.ImageFile = reader["ImageFile"].ToString();

                                result.Add(data);

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
        [ActionName("GetProject")]
        public ProjectTemp GetProject(string id)
        {
            ProjectTemp result = new ProjectTemp();
            result = ProjectGetById(id);
            return result;
        }

        private ProjectTemp ProjectGetById(string id)
        {
            ProjectTemp result;
            result = new ProjectTemp();

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
                    cmd.CommandText = "spGetProjectById";

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
                            result.UserId = reader["UserId"].ToString();
                            result.Nomor = reader["Nomor"].ToString();
                            result.Name = reader["Name"].ToString();
                            result.ProvinceId = reader["ProvinceId"].ToString();
                            result.CityId = reader["CityId"].ToString();
                            result.DeveloperId = reader["DeveloperId"].ToString();
                            result.ContractorId = reader["ContractorId"].ToString();
                            result.ConsultantId = reader["ConsultantId"].ToString();
                            result.SupplierId = reader["SupplierId"].ToString();
                            result.RangeDateStart = reader["RangeDateStart"].ToString();
                            result.RangeDateEnd = reader["RangeDateEnd"].ToString();
                            result.RangeQualityStart = float.Parse(reader["RangeQualityStart"].ToString());
                            result.RangeQualityEnd = float.Parse(reader["RangeQualityEnd"].ToString());
                            result.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                            result.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                            result.Note = reader["Note"].ToString();
                            result.Latitude = float.Parse(reader["Latitude"].ToString());
                            result.Longitude = float.Parse(reader["Longitude"].ToString());
                            result.ImageFile = reader["ImageFile"].ToString();
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
        [ActionName("GetDataProjectById")]
        public ProjectTemp GetDataProjectById(string id)
        {
            ProjectTemp result = new ProjectTemp();
            result = GetingDataProjectById(id);
            return result;
        }

        private ProjectTemp GetingDataProjectById(string id)
        {
            ProjectTemp result;
            result = new ProjectTemp();

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
                    cmd.CommandText = "spGetDataProjectById";

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
                            result.UserId = reader["UserId"].ToString();
                            result.Nomor = reader["Nomor"].ToString();
                            result.Name = reader["Name"].ToString();
                            result.ProvinceId = reader["ProvinceId"].ToString();
                            result.CityId = reader["CityId"].ToString();
                            result.DeveloperId = reader["DeveloperId"].ToString();
                            result.ContractorId = reader["ContractorId"].ToString();
                            result.ConsultantId = reader["ConsultantId"].ToString();
                            result.SupplierId = reader["SupplierId"].ToString();
                            result.RangeDateStart = reader["RangeDateStart"].ToString();
                            result.RangeDateEnd = reader["RangeDateEnd"].ToString();
                            result.RangeQualityStart = float.Parse(reader["RangeQualityStart"].ToString());
                            result.RangeQualityEnd = float.Parse(reader["RangeQualityEnd"].ToString());
                            result.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                            result.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                            result.Note = reader["Note"].ToString();
                            result.Latitude = float.Parse(reader["Latitude"].ToString());
                            result.Longitude = float.Parse(reader["Longitude"].ToString());
                            result.ImageFile = reader["ImageFile"].ToString();
                            result.ImageFile2 = reader["ImageFile2"].ToString();
                            result.ImageFile3 = reader["ImageFile3"].ToString();
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
        [ActionName("GetDataProjectByIdWithName")]
        public ProjectTemp GetDataProjectByIdWithName(string id)
        {
            ProjectTemp result = new ProjectTemp();
            result = GettingDataProjectByIdWithName(id);
            return result;
        }

        private ProjectTemp GettingDataProjectByIdWithName(string id)
        {
            ProjectTemp result;
            result = new ProjectTemp();

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
                    cmd.CommandText = "spGetDataProjectByIdWithName";

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
                            result.UserId = reader["UserId"].ToString();
                            result.UserName = reader["UserName"].ToString();
                            result.Nomor = reader["Nomor"].ToString();
                            result.Name = reader["Name"].ToString();
                            result.ProvinceId = reader["ProvinceId"].ToString();
                            result.ProvinceName = reader["ProvinceName"].ToString();
                            result.CityId = reader["CityId"].ToString();
                            result.CityName = reader["CityName"].ToString();
                            result.DeveloperId = reader["DeveloperId"].ToString();
                            result.DeveloperName = reader["DeveloperName"].ToString();
                            result.ContractorId = reader["ContractorId"].ToString();
                            result.ContractorName = reader["ContractorName"].ToString();
                            result.ConsultantId = reader["ConsultantId"].ToString();
                            result.ConsultantName = reader["ConsultantName"].ToString();
                            result.SupplierId = reader["SupplierId"].ToString();
                            result.SupplierName = reader["SupplierName"].ToString();
                            result.RangeDateStart = reader["RangeDateStart"].ToString();
                            result.RangeDateEnd = reader["RangeDateEnd"].ToString();
                            result.RangeQualityStart = float.Parse(reader["RangeQualityStart"].ToString());
                            result.RangeQualityEnd = float.Parse(reader["RangeQualityEnd"].ToString());
                            result.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                            result.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                            result.Note = reader["Note"].ToString();
                            result.Latitude = float.Parse(reader["Latitude"].ToString());
                            result.Longitude = float.Parse(reader["Longitude"].ToString());
                            result.ImageFile = reader["ImageFile"].ToString();
                            result.ImageFile2 = reader["ImageFile2"].ToString();
                            result.ImageFile3 = reader["ImageFile3"].ToString();
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
        [ActionName("GetOtherProject")]
        public ProjectTemp GetOtherProject(string id)
        {
            ProjectTemp result = new ProjectTemp();
            result = ProjectOtherGetById(id);
            return result;
        }

        private ProjectTemp ProjectOtherGetById(string id)
        {
            ProjectTemp result;
            result = new ProjectTemp();

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
                    cmd.CommandText = "spGetOtherProjectById";

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
                            result.UserId = reader["UserId"].ToString();
                            result.Nomor = reader["Nomor"].ToString();
                            result.Name = reader["Name"].ToString();
                            result.ProvinceId = reader["ProvinceId"].ToString();
                            result.CityId = reader["CityId"].ToString();
                            result.DeveloperId = reader["DeveloperId"].ToString();
                            result.ContractorId = reader["ContractorId"].ToString();
                            result.ConsultantId = reader["ConsultantId"].ToString();
                            result.SupplierId = reader["SupplierId"].ToString();
                            result.RangeDateStart = reader["RangeDateStart"].ToString();
                            result.RangeDateEnd = reader["RangeDateEnd"].ToString();
                            result.RangeQualityStart = float.Parse(reader["RangeQualityStart"].ToString());
                            result.RangeQualityEnd = float.Parse(reader["RangeQualityEnd"].ToString());
                            result.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                            result.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                            result.Note = reader["Note"].ToString();
                            result.Latitude = float.Parse(reader["Latitude"].ToString());
                            result.Longitude = float.Parse(reader["Longitude"].ToString());
                            result.ImageFile = reader["ImageFile"].ToString();
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
        [ActionName("GetProjectIdAndroid")]
        public Data GetProjectIdAndroid(string id)
        {
            Data result = new Data();
            result = ProjectGetByIdAndroid(id);
            return result;
        }

        private Data ProjectGetByIdAndroid(string id)
        {
            Data result;
            result = new Data();

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
                    cmd.CommandText = "spGetProjectById";

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

                            result.Data0 = reader["Id"].ToString();
                            result.Data1 = reader["UserId"].ToString();
                            result.Data2 = reader["Nomor"].ToString();
                            result.Data3 = reader["Name"].ToString();
                            result.DDL1 = reader["ProvinceId"].ToString();
                            result.DDL2 = reader["CityId"].ToString();
                            result.DDL3 = reader["DeveloperId"].ToString();
                            result.DDL4 = reader["ContractorId"].ToString();
                            result.DDL5 = reader["ConsultantId"].ToString();
                            result.DDL6 = reader["SupplierId"].ToString();
                            result.Date1 = reader["RangeDateStart"].ToString();
                            result.Date2 = reader["RangeDateEnd"].ToString();
                            result.Data4 = reader["RangeQualityStart"].ToString();
                            result.Data5 = reader["RangeQualityEnd"].ToString();
                            result.Data6 = reader["RangeNominalStart"].ToString();
                            result.Data7 = reader["RangeNominalEnd"].ToString();
                            result.Data8 = reader["Note"].ToString();

                            result.Data10 = reader["Latitude"].ToString();
                            result.Data11 = reader["Longitude"].ToString();
                            result.Data12 = reader["ImageFile"].ToString();

                            //result.Id = reader["Id"].ToString();
                            //result.UserId = reader["UserId"].ToString();
                            //result.Nomor = reader["Nomor"].ToString();
                            //result.Name = reader["Name"].ToString();
                            //result.ProvinceId = reader["ProvinceId"].ToString();
                            //result.CityId = reader["CityId"].ToString();
                            //result.DeveloperId = reader["DeveloperId"].ToString();
                            //result.ContractorId = reader["ContractorId"].ToString();
                            //result.ConsultantId = reader["ConsultantId"].ToString();
                            //result.SupplierId = reader["SupplierId"].ToString();
                            //result.RangeDateStart = reader["RangeDateStart"].ToString();
                            //result.RangeDateEnd = reader["RangeDateEnd"].ToString();
                            //result.RangeQualityStart = float.Parse(reader["RangeQualityStart"].ToString());
                            //result.RangeQualityEnd = float.Parse(reader["RangeQualityEnd"].ToString());
                            //result.RangeNominalStart = float.Parse(reader["RangeNominalStart"].ToString());
                            //result.RangeNominalEnd = float.Parse(reader["RangeNominalEnd"].ToString());
                            //result.Note = reader["Note"].ToString();
                            //result.Latitude = float.Parse(reader["Latitude"].ToString());
                            //result.Longitude = float.Parse(reader["Longitude"].ToString());
                            //result.ImageFile = reader["ImageFile"].ToString();
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
        [ActionName("GetProjectIdSmartPhone")]
        public Data GetProjectIdSmartPhone(string id)
        {
            Data result = new Data();
            result = ProjectGetByIdSmartPhone(id);
            return result;
        }

        private Data ProjectGetByIdSmartPhone(string id)
        {
            Data result;
            result = new Data();

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
                    cmd.CommandText = "spGetProjectByIdSmartPhone";

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

                            result.Data0 = reader["Id"].ToString();
                            result.Data1 = reader["UserId"].ToString();
                            result.Data2 = reader["Nomor"].ToString();
                            result.Data3 = reader["Name"].ToString();
                            result.DDL1 = reader["ProvinceId"].ToString();
                            result.DDL2 = reader["CityId"].ToString();
                            result.DDL3 = reader["DeveloperId"].ToString();
                            result.DDL4 = reader["ContractorId"].ToString();
                            result.DDL5 = reader["ConsultantId"].ToString();
                            result.DDL6 = reader["SupplierId"].ToString();

                            result.DDL1Name = reader["ProvinceName"].ToString();
                            result.DDL2Name = reader["CityName"].ToString();
                            result.DDL3Name = reader["DeveloperName"].ToString();
                            result.DDL4Name = reader["ContractorName"].ToString();
                            result.DDL5Name = reader["ConsultantName"].ToString();
                            result.DDL6Name = reader["SupplierName"].ToString();

                            result.Date1 = reader["RangeDateStart"].ToString();
                            result.Date2 = reader["RangeDateEnd"].ToString();
                            result.Data4 = reader["RangeQualityStart"].ToString();
                            result.Data5 = reader["RangeQualityEnd"].ToString();
                            result.Data6 = reader["RangeNominalStart"].ToString();
                            result.Data7 = reader["RangeNominalEnd"].ToString();
                            result.Data8 = reader["Note"].ToString();

                            result.Data10 = reader["Latitude"].ToString();
                            result.Data11 = reader["Longitude"].ToString();
                            result.Data12 = reader["ImageFile"].ToString();

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
        [ActionName("SaveProject")]
        public String SaveProject([FromBody] ProjectTemp project)
        {
            string result;
            result = SaveProjectData(project);
            return result;
        }

        private String SaveProjectData(ProjectTemp project)
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
                    cmd.CommandText = "spInsertProject";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = project.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamNomor = new SqlParameter();
                    sqlParamNomor.ParameterName = "nomor";
                    sqlParamNomor.DbType = DbType.String;
                    sqlParamNomor.Value = project.Nomor;
                    cmd.Parameters.Add(sqlParamNomor);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = project.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = project.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

                    SqlParameter sqlParamCityId = new SqlParameter();
                    sqlParamCityId.ParameterName = "cityId";
                    sqlParamCityId.DbType = DbType.String;
                    sqlParamCityId.Value = project.CityId;
                    cmd.Parameters.Add(sqlParamCityId);

                    SqlParameter sqlParamDeveloperId = new SqlParameter();
                    sqlParamDeveloperId.ParameterName = "developerId";
                    sqlParamDeveloperId.DbType = DbType.String;
                    sqlParamDeveloperId.Value = project.DeveloperId;
                    cmd.Parameters.Add(sqlParamDeveloperId);

                    SqlParameter sqlParamContractorId = new SqlParameter();
                    sqlParamContractorId.ParameterName = "contractorId";
                    sqlParamContractorId.DbType = DbType.String;
                    sqlParamContractorId.Value = project.ContractorId;
                    cmd.Parameters.Add(sqlParamContractorId);

                    SqlParameter sqlParamConsultantId = new SqlParameter();
                    sqlParamConsultantId.ParameterName = "consultantId";
                    sqlParamConsultantId.DbType = DbType.String;
                    sqlParamConsultantId.Value = project.ConsultantId;
                    cmd.Parameters.Add(sqlParamConsultantId);

                    SqlParameter sqlParamSupplierId = new SqlParameter();
                    sqlParamSupplierId.ParameterName = "supplierId";
                    sqlParamSupplierId.DbType = DbType.String;
                    sqlParamSupplierId.Value = project.SupplierId;
                    cmd.Parameters.Add(sqlParamSupplierId);

                    SqlParameter sqlParamRangeDateStart = new SqlParameter();
                    sqlParamRangeDateStart.ParameterName = "rangeDateStart";
                    sqlParamRangeDateStart.DbType = DbType.String;
                    sqlParamRangeDateStart.Value = project.RangeDateStart;
                    cmd.Parameters.Add(sqlParamRangeDateStart);

                    SqlParameter sqlParamRangeDateEnd = new SqlParameter();
                    sqlParamRangeDateEnd.ParameterName = "rangeDateEnd";
                    sqlParamRangeDateEnd.DbType = DbType.String;
                    sqlParamRangeDateEnd.Value = project.RangeDateEnd;
                    cmd.Parameters.Add(sqlParamRangeDateEnd);

                    SqlParameter sqlParamRangeQualityStart = new SqlParameter();
                    sqlParamRangeQualityStart.ParameterName = "rangeQualityStart";
                    sqlParamRangeQualityStart.DbType = DbType.Single;
                    sqlParamRangeQualityStart.Value = project.RangeQualityStart;
                    cmd.Parameters.Add(sqlParamRangeQualityStart);

                    SqlParameter sqlParamRangeQualityEnd = new SqlParameter();
                    sqlParamRangeQualityEnd.ParameterName = "rangeQualityEnd";
                    sqlParamRangeQualityEnd.DbType = DbType.Single;
                    sqlParamRangeQualityEnd.Value = project.RangeQualityEnd;
                    cmd.Parameters.Add(sqlParamRangeQualityEnd);

                    SqlParameter sqlParamRangeNominalStart = new SqlParameter();
                    sqlParamRangeNominalStart.ParameterName = "rangeNominalStart";
                    sqlParamRangeNominalStart.DbType = DbType.Single;
                    sqlParamRangeNominalStart.Value = project.RangeNominalStart;
                    cmd.Parameters.Add(sqlParamRangeNominalStart);

                    SqlParameter sqlParamRangeNominalEnd = new SqlParameter();
                    sqlParamRangeNominalEnd.ParameterName = "rangeNominalEnd";
                    sqlParamRangeNominalEnd.DbType = DbType.Single;
                    sqlParamRangeNominalEnd.Value = project.RangeNominalEnd;
                    cmd.Parameters.Add(sqlParamRangeNominalEnd);

                    SqlParameter sqlParamNote = new SqlParameter();
                    sqlParamNote.ParameterName = "note";
                    sqlParamNote.DbType = DbType.String;
                    sqlParamNote.Value = project.Note;
                    cmd.Parameters.Add(sqlParamNote);

                    SqlParameter sqlParamLatitude = new SqlParameter();
                    sqlParamLatitude.ParameterName = "latitude";
                    sqlParamLatitude.DbType = DbType.Single;
                    sqlParamLatitude.Value = project.Latitude;
                    cmd.Parameters.Add(sqlParamLatitude);

                    SqlParameter sqlParamLongitude = new SqlParameter();
                    sqlParamLongitude.ParameterName = "longitude";
                    sqlParamLongitude.DbType = DbType.Single;
                    sqlParamLongitude.Value = project.Longitude;
                    cmd.Parameters.Add(sqlParamLongitude);

                    SqlParameter sqlParamImageFile = new SqlParameter();
                    sqlParamImageFile.ParameterName = "imageFile";
                    sqlParamImageFile.DbType = DbType.String;
                    sqlParamImageFile.Value = project.ImageFile;
                    cmd.Parameters.Add(sqlParamImageFile);

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

        [HttpPost]
        [ActionName("SaveDataProject")]
        public String SaveDataProject([FromBody] ProjectTemp project)
        {
            string result;
            result = SavingProjectData(project);
            return result;
        }

        private String SavingProjectData(ProjectTemp project)
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
                    cmd.CommandText = "spInsertDataProject";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = project.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamNomor = new SqlParameter();
                    sqlParamNomor.ParameterName = "nomor";
                    sqlParamNomor.DbType = DbType.String;
                    sqlParamNomor.Value = project.Nomor;
                    cmd.Parameters.Add(sqlParamNomor);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = project.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = project.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

                    SqlParameter sqlParamCityId = new SqlParameter();
                    sqlParamCityId.ParameterName = "cityId";
                    sqlParamCityId.DbType = DbType.String;
                    sqlParamCityId.Value = project.CityId;
                    cmd.Parameters.Add(sqlParamCityId);

                    SqlParameter sqlParamDeveloperId = new SqlParameter();
                    sqlParamDeveloperId.ParameterName = "developerId";
                    sqlParamDeveloperId.DbType = DbType.String;
                    sqlParamDeveloperId.Value = project.DeveloperId;
                    cmd.Parameters.Add(sqlParamDeveloperId);

                    SqlParameter sqlParamContractorId = new SqlParameter();
                    sqlParamContractorId.ParameterName = "contractorId";
                    sqlParamContractorId.DbType = DbType.String;
                    sqlParamContractorId.Value = project.ContractorId;
                    cmd.Parameters.Add(sqlParamContractorId);

                    SqlParameter sqlParamConsultantId = new SqlParameter();
                    sqlParamConsultantId.ParameterName = "consultantId";
                    sqlParamConsultantId.DbType = DbType.String;
                    sqlParamConsultantId.Value = project.ConsultantId;
                    cmd.Parameters.Add(sqlParamConsultantId);

                    SqlParameter sqlParamSupplierId = new SqlParameter();
                    sqlParamSupplierId.ParameterName = "supplierId";
                    sqlParamSupplierId.DbType = DbType.String;
                    sqlParamSupplierId.Value = project.SupplierId;
                    cmd.Parameters.Add(sqlParamSupplierId);

                    SqlParameter sqlParamRangeDateStart = new SqlParameter();
                    sqlParamRangeDateStart.ParameterName = "rangeDateStart";
                    sqlParamRangeDateStart.DbType = DbType.String;
                    sqlParamRangeDateStart.Value = project.RangeDateStart;
                    cmd.Parameters.Add(sqlParamRangeDateStart);

                    SqlParameter sqlParamRangeDateEnd = new SqlParameter();
                    sqlParamRangeDateEnd.ParameterName = "rangeDateEnd";
                    sqlParamRangeDateEnd.DbType = DbType.String;
                    sqlParamRangeDateEnd.Value = project.RangeDateEnd;
                    cmd.Parameters.Add(sqlParamRangeDateEnd);

                    SqlParameter sqlParamRangeQualityStart = new SqlParameter();
                    sqlParamRangeQualityStart.ParameterName = "rangeQualityStart";
                    sqlParamRangeQualityStart.DbType = DbType.Single;
                    sqlParamRangeQualityStart.Value = project.RangeQualityStart;
                    cmd.Parameters.Add(sqlParamRangeQualityStart);

                    SqlParameter sqlParamRangeQualityEnd = new SqlParameter();
                    sqlParamRangeQualityEnd.ParameterName = "rangeQualityEnd";
                    sqlParamRangeQualityEnd.DbType = DbType.Single;
                    sqlParamRangeQualityEnd.Value = project.RangeQualityEnd;
                    cmd.Parameters.Add(sqlParamRangeQualityEnd);

                    SqlParameter sqlParamRangeNominalStart = new SqlParameter();
                    sqlParamRangeNominalStart.ParameterName = "rangeNominalStart";
                    sqlParamRangeNominalStart.DbType = DbType.Single;
                    sqlParamRangeNominalStart.Value = project.RangeNominalStart;
                    cmd.Parameters.Add(sqlParamRangeNominalStart);

                    SqlParameter sqlParamRangeNominalEnd = new SqlParameter();
                    sqlParamRangeNominalEnd.ParameterName = "rangeNominalEnd";
                    sqlParamRangeNominalEnd.DbType = DbType.Single;
                    sqlParamRangeNominalEnd.Value = project.RangeNominalEnd;
                    cmd.Parameters.Add(sqlParamRangeNominalEnd);

                    SqlParameter sqlParamNote = new SqlParameter();
                    sqlParamNote.ParameterName = "note";
                    sqlParamNote.DbType = DbType.String;
                    sqlParamNote.Value = project.Note;
                    cmd.Parameters.Add(sqlParamNote);

                    SqlParameter sqlParamLatitude = new SqlParameter();
                    sqlParamLatitude.ParameterName = "latitude";
                    sqlParamLatitude.DbType = DbType.Single;
                    sqlParamLatitude.Value = project.Latitude;
                    cmd.Parameters.Add(sqlParamLatitude);

                    SqlParameter sqlParamLongitude = new SqlParameter();
                    sqlParamLongitude.ParameterName = "longitude";
                    sqlParamLongitude.DbType = DbType.Single;
                    sqlParamLongitude.Value = project.Longitude;
                    cmd.Parameters.Add(sqlParamLongitude);

                    SqlParameter sqlParamImageFile = new SqlParameter();
                    sqlParamImageFile.ParameterName = "imageFile";
                    sqlParamImageFile.DbType = DbType.String;
                    sqlParamImageFile.Value = project.ImageFile;
                    cmd.Parameters.Add(sqlParamImageFile);

                    SqlParameter sqlParamImageFile2 = new SqlParameter();
                    sqlParamImageFile2.ParameterName = "imageFile2";
                    sqlParamImageFile2.DbType = DbType.String;
                    sqlParamImageFile2.Value = project.ImageFile2;
                    cmd.Parameters.Add(sqlParamImageFile2);

                    SqlParameter sqlParamImageFile3 = new SqlParameter();
                    sqlParamImageFile3.ParameterName = "imageFile3";
                    sqlParamImageFile3.DbType = DbType.String;
                    sqlParamImageFile3.Value = project.ImageFile3;
                    cmd.Parameters.Add(sqlParamImageFile3);

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
        [ActionName("UpdateProject")]
        public String UpdateProject([FromBody] ProjectTemp project)
        {
            string result;
            result = UpdateProjectData(project);
            return result;
        }

        private String UpdateProjectData(ProjectTemp project)
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
                    cmd.CommandText = "spUpdateProjectById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = project.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = project.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamNomor = new SqlParameter();
                    sqlParamNomor.ParameterName = "nomor";
                    sqlParamNomor.DbType = DbType.String;
                    sqlParamNomor.Value = project.Nomor;
                    cmd.Parameters.Add(sqlParamNomor);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = project.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = project.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

                    SqlParameter sqlParamCityId = new SqlParameter();
                    sqlParamCityId.ParameterName = "cityId";
                    sqlParamCityId.DbType = DbType.String;
                    sqlParamCityId.Value = project.CityId;
                    cmd.Parameters.Add(sqlParamCityId);

                    SqlParameter sqlParamDeveloperId = new SqlParameter();
                    sqlParamDeveloperId.ParameterName = "developerId";
                    sqlParamDeveloperId.DbType = DbType.String;
                    sqlParamDeveloperId.Value = project.DeveloperId;
                    cmd.Parameters.Add(sqlParamDeveloperId);

                    SqlParameter sqlParamContractorId = new SqlParameter();
                    sqlParamContractorId.ParameterName = "contractorId";
                    sqlParamContractorId.DbType = DbType.String;
                    sqlParamContractorId.Value = project.ContractorId;
                    cmd.Parameters.Add(sqlParamContractorId);

                    SqlParameter sqlParamConsultantId = new SqlParameter();
                    sqlParamConsultantId.ParameterName = "consultantId";
                    sqlParamConsultantId.DbType = DbType.String;
                    sqlParamConsultantId.Value = project.ConsultantId;
                    cmd.Parameters.Add(sqlParamConsultantId);

                    SqlParameter sqlParamSupplierId = new SqlParameter();
                    sqlParamSupplierId.ParameterName = "supplierId";
                    sqlParamSupplierId.DbType = DbType.String;
                    sqlParamSupplierId.Value = project.SupplierId;
                    cmd.Parameters.Add(sqlParamSupplierId);

                    SqlParameter sqlParamRangeDateStart = new SqlParameter();
                    sqlParamRangeDateStart.ParameterName = "rangeDateStart";
                    sqlParamRangeDateStart.DbType = DbType.String;
                    sqlParamRangeDateStart.Value = project.RangeDateStart;
                    cmd.Parameters.Add(sqlParamRangeDateStart);

                    SqlParameter sqlParamRangeDateEnd = new SqlParameter();
                    sqlParamRangeDateEnd.ParameterName = "rangeDateEnd";
                    sqlParamRangeDateEnd.DbType = DbType.String;
                    sqlParamRangeDateEnd.Value = project.RangeDateEnd;
                    cmd.Parameters.Add(sqlParamRangeDateEnd);

                    SqlParameter sqlParamRangeQualityStart = new SqlParameter();
                    sqlParamRangeQualityStart.ParameterName = "rangeQualityStart";
                    sqlParamRangeQualityStart.DbType = DbType.Single;
                    sqlParamRangeQualityStart.Value = project.RangeQualityStart;
                    cmd.Parameters.Add(sqlParamRangeQualityStart);

                    SqlParameter sqlParamRangeQualityEnd = new SqlParameter();
                    sqlParamRangeQualityEnd.ParameterName = "rangeQualityEnd";
                    sqlParamRangeQualityEnd.DbType = DbType.Single;
                    sqlParamRangeQualityEnd.Value = project.RangeQualityEnd;
                    cmd.Parameters.Add(sqlParamRangeQualityEnd);

                    SqlParameter sqlParamRangeNominalStart = new SqlParameter();
                    sqlParamRangeNominalStart.ParameterName = "rangeNominalStart";
                    sqlParamRangeNominalStart.DbType = DbType.Single;
                    sqlParamRangeNominalStart.Value = project.RangeNominalStart;
                    cmd.Parameters.Add(sqlParamRangeNominalStart);

                    SqlParameter sqlParamRangeNominalEnd = new SqlParameter();
                    sqlParamRangeNominalEnd.ParameterName = "rangeNominalEnd";
                    sqlParamRangeNominalEnd.DbType = DbType.Single;
                    sqlParamRangeNominalEnd.Value = project.RangeNominalEnd;
                    cmd.Parameters.Add(sqlParamRangeNominalEnd);

                    SqlParameter sqlParamNote = new SqlParameter();
                    sqlParamNote.ParameterName = "note";
                    sqlParamNote.DbType = DbType.String;
                    sqlParamNote.Value = project.Note;
                    cmd.Parameters.Add(sqlParamNote);

                    SqlParameter sqlParamLatitude = new SqlParameter();
                    sqlParamLatitude.ParameterName = "latitude";
                    sqlParamLatitude.DbType = DbType.Single;
                    sqlParamLatitude.Value = project.Latitude;
                    cmd.Parameters.Add(sqlParamLatitude);

                    SqlParameter sqlParamLongitude = new SqlParameter();
                    sqlParamLongitude.ParameterName = "longitude";
                    sqlParamLongitude.DbType = DbType.Single;
                    sqlParamLongitude.Value = project.Longitude;
                    cmd.Parameters.Add(sqlParamLongitude);

                    SqlParameter sqlParamImageFile = new SqlParameter();
                    sqlParamImageFile.ParameterName = "imageFile";
                    sqlParamImageFile.DbType = DbType.String;
                    sqlParamImageFile.Value = project.ImageFile;
                    cmd.Parameters.Add(sqlParamImageFile);

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
        [ActionName("UpdateDataProject")]
        public String UpdateDataProject([FromBody] ProjectTemp project)
        {
            string result;
            result = UpdatingProjectData(project);
            return result;
        }

        private String UpdatingProjectData(ProjectTemp project)
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
                    cmd.CommandText = "spUpdateDataProjectById";

                    SqlParameter sqlParamId = new SqlParameter();
                    sqlParamId.ParameterName = "id";
                    sqlParamId.DbType = DbType.String;
                    sqlParamId.Value = project.Id;
                    cmd.Parameters.Add(sqlParamId);

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = project.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamNomor = new SqlParameter();
                    sqlParamNomor.ParameterName = "nomor";
                    sqlParamNomor.DbType = DbType.String;
                    sqlParamNomor.Value = project.Nomor;
                    cmd.Parameters.Add(sqlParamNomor);

                    SqlParameter sqlParamName = new SqlParameter();
                    sqlParamName.ParameterName = "name";
                    sqlParamName.DbType = DbType.String;
                    sqlParamName.Value = project.Name;
                    cmd.Parameters.Add(sqlParamName);

                    SqlParameter sqlParamProvinceId = new SqlParameter();
                    sqlParamProvinceId.ParameterName = "provinceId";
                    sqlParamProvinceId.DbType = DbType.String;
                    sqlParamProvinceId.Value = project.ProvinceId;
                    cmd.Parameters.Add(sqlParamProvinceId);

                    SqlParameter sqlParamCityId = new SqlParameter();
                    sqlParamCityId.ParameterName = "cityId";
                    sqlParamCityId.DbType = DbType.String;
                    sqlParamCityId.Value = project.CityId;
                    cmd.Parameters.Add(sqlParamCityId);

                    SqlParameter sqlParamDeveloperId = new SqlParameter();
                    sqlParamDeveloperId.ParameterName = "developerId";
                    sqlParamDeveloperId.DbType = DbType.String;
                    sqlParamDeveloperId.Value = project.DeveloperId;
                    cmd.Parameters.Add(sqlParamDeveloperId);

                    SqlParameter sqlParamContractorId = new SqlParameter();
                    sqlParamContractorId.ParameterName = "contractorId";
                    sqlParamContractorId.DbType = DbType.String;
                    sqlParamContractorId.Value = project.ContractorId;
                    cmd.Parameters.Add(sqlParamContractorId);

                    SqlParameter sqlParamConsultantId = new SqlParameter();
                    sqlParamConsultantId.ParameterName = "consultantId";
                    sqlParamConsultantId.DbType = DbType.String;
                    sqlParamConsultantId.Value = project.ConsultantId;
                    cmd.Parameters.Add(sqlParamConsultantId);

                    SqlParameter sqlParamSupplierId = new SqlParameter();
                    sqlParamSupplierId.ParameterName = "supplierId";
                    sqlParamSupplierId.DbType = DbType.String;
                    sqlParamSupplierId.Value = project.SupplierId;
                    cmd.Parameters.Add(sqlParamSupplierId);

                    SqlParameter sqlParamRangeDateStart = new SqlParameter();
                    sqlParamRangeDateStart.ParameterName = "rangeDateStart";
                    sqlParamRangeDateStart.DbType = DbType.String;
                    sqlParamRangeDateStart.Value = project.RangeDateStart;
                    cmd.Parameters.Add(sqlParamRangeDateStart);

                    SqlParameter sqlParamRangeDateEnd = new SqlParameter();
                    sqlParamRangeDateEnd.ParameterName = "rangeDateEnd";
                    sqlParamRangeDateEnd.DbType = DbType.String;
                    sqlParamRangeDateEnd.Value = project.RangeDateEnd;
                    cmd.Parameters.Add(sqlParamRangeDateEnd);

                    SqlParameter sqlParamRangeQualityStart = new SqlParameter();
                    sqlParamRangeQualityStart.ParameterName = "rangeQualityStart";
                    sqlParamRangeQualityStart.DbType = DbType.Single;
                    sqlParamRangeQualityStart.Value = project.RangeQualityStart;
                    cmd.Parameters.Add(sqlParamRangeQualityStart);

                    SqlParameter sqlParamRangeQualityEnd = new SqlParameter();
                    sqlParamRangeQualityEnd.ParameterName = "rangeQualityEnd";
                    sqlParamRangeQualityEnd.DbType = DbType.Single;
                    sqlParamRangeQualityEnd.Value = project.RangeQualityEnd;
                    cmd.Parameters.Add(sqlParamRangeQualityEnd);

                    SqlParameter sqlParamRangeNominalStart = new SqlParameter();
                    sqlParamRangeNominalStart.ParameterName = "rangeNominalStart";
                    sqlParamRangeNominalStart.DbType = DbType.Single;
                    sqlParamRangeNominalStart.Value = project.RangeNominalStart;
                    cmd.Parameters.Add(sqlParamRangeNominalStart);

                    SqlParameter sqlParamRangeNominalEnd = new SqlParameter();
                    sqlParamRangeNominalEnd.ParameterName = "rangeNominalEnd";
                    sqlParamRangeNominalEnd.DbType = DbType.Single;
                    sqlParamRangeNominalEnd.Value = project.RangeNominalEnd;
                    cmd.Parameters.Add(sqlParamRangeNominalEnd);

                    SqlParameter sqlParamNote = new SqlParameter();
                    sqlParamNote.ParameterName = "note";
                    sqlParamNote.DbType = DbType.String;
                    sqlParamNote.Value = project.Note;
                    cmd.Parameters.Add(sqlParamNote);

                    SqlParameter sqlParamLatitude = new SqlParameter();
                    sqlParamLatitude.ParameterName = "latitude";
                    sqlParamLatitude.DbType = DbType.Single;
                    sqlParamLatitude.Value = project.Latitude;
                    cmd.Parameters.Add(sqlParamLatitude);

                    SqlParameter sqlParamLongitude = new SqlParameter();
                    sqlParamLongitude.ParameterName = "longitude";
                    sqlParamLongitude.DbType = DbType.Single;
                    sqlParamLongitude.Value = project.Longitude;
                    cmd.Parameters.Add(sqlParamLongitude);

                    SqlParameter sqlParamImageFile = new SqlParameter();
                    sqlParamImageFile.ParameterName = "imageFile";
                    sqlParamImageFile.DbType = DbType.String;
                    sqlParamImageFile.Value = project.ImageFile;
                    cmd.Parameters.Add(sqlParamImageFile);

                    SqlParameter sqlParamImageFile2 = new SqlParameter();
                    sqlParamImageFile2.ParameterName = "imageFile2";
                    sqlParamImageFile2.DbType = DbType.String;
                    sqlParamImageFile2.Value = project.ImageFile2;
                    cmd.Parameters.Add(sqlParamImageFile2);

                    SqlParameter sqlParamImageFile3 = new SqlParameter();
                    sqlParamImageFile3.ParameterName = "imageFile3";
                    sqlParamImageFile3.DbType = DbType.String;
                    sqlParamImageFile3.Value = project.ImageFile3;
                    cmd.Parameters.Add(sqlParamImageFile3);

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
        [ActionName("DeleteProject")]
        public String DeleteProject(string id)
        {
            string result;
            result = DeleteProjectData(id);
            return result;
        }

        private String DeleteProjectData(string id)
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
                    cmd.CommandText = "spDeleteProjectById";

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
