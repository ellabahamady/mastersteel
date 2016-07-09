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
    public class ProjectController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetAllProjectList")]
        public IEnumerable<Project> GetAllProjectList()
        {
            List<Project> result;
            result = new List<Project>();

            result = GetAllProjectListData();
            return result;
        }

        private List<Project> GetAllProjectListData()
        {
            List<Project> result;
            result = new List<Project>();

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
                    cmd.CommandText = "spGetAllProjectList";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Project project = new Project();
                                project.ProjectCategoryID = reader["ProjectCategoryID"].ToString();
                                project.ProjectID = reader["ProjectID"].ToString();
                                project.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                project.ProjectName = reader["ProjectName"].ToString();
                                project.ProjectAddress = reader["ProjectAddress"].ToString();
                                project.ProvinsiID = reader["ProvinsiID"].ToString();
                                project.CityID = reader["CityID"].ToString();
                                project.BuildingArea = float.Parse(reader["BuildingArea"].ToString());
                                project.Quantity = float.Parse(reader["Quantity"].ToString());
                                project.Nominal = float.Parse(reader["Nominal"].ToString());
                                project.DeveloperID = reader["DeveloperID"].ToString();
                                project.ProjectManager = reader["ProjectManager"].ToString();
                                project.KontraktorID = reader["KontraktorID"].ToString();
                                project.ConsultantID = reader["ConsultantID"].ToString();
                                project.ManagementKonstruksiID = reader["ManagementKonstruksiID"].ToString();
                                project.ProjectPeriodStart = reader["ProjectPeriodStart"].ToString();
                                project.ProjectPeriodEnd = reader["ProjectPeriodEnd"].ToString();
                                project.SupplierID = reader["SupplierID"].ToString();
                                project.ContractNumber = reader["ContractNumber"].ToString();
                                project.ContractID = reader["ContractID"].ToString();
                                project.StatusProject = reader["StatusProject"].ToString();
                                project.Note = reader["Note"].ToString();
                                project.SalesID = reader["SalesID"].ToString();
                                project.PICID = reader["PICID"].ToString();
                                project.Latitude = float.Parse(reader["Latitude"].ToString());
                                project.Longitude = float.Parse(reader["Longitude"].ToString());
                                project.Images = reader["Images"].ToString();
                                project.UserCreateID = reader["UserCreateID"].ToString();
                                project.UserName = reader["UserName"].ToString();
                                project.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]).ToString("dd MMMM yyyy");
                                project.UserUpdateID = reader["UserUpdateID"].ToString();
                                project.UpdateAt = reader["UpdateAt"].ToString();
                                project.Status = reader["Status"].ToString();
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
        [ActionName("GetProjectNameList")]
        public IEnumerable<Project> GetProjectNameList()
        {
            List<Project> result;
            result = new List<Project>();

            result = GetProjectNameListData();
            return result;
        }

        private List<Project> GetProjectNameListData()
        {
            List<Project> result;
            result = new List<Project>();

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
                    cmd.CommandText = "spGetProjectNameList";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Project project = new Project();
                                project.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                project.ProjectName = reader["ProjectName"].ToString();
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


        [HttpPost]
        [ActionName("GetAllProjects")]
        public IEnumerable<Project> GetAllProjects([FromBody] Project project)
        {
            List<Project> result;
            result = new List<Project>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(project.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetAllProjectsData(project);
            return result;
        }

        private List<Project> GetAllProjectsData(Project projek)
        {
            List<Project> result;
            result = new List<Project>();

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
                    cmd.CommandText = "spSelectAllProjectsByProvinceAndCity";

                    SqlParameter sqlParamProvinsiID = new SqlParameter();
                    sqlParamProvinsiID.ParameterName = "provinsiID";
                    sqlParamProvinsiID.DbType = DbType.String;
                    sqlParamProvinsiID.Value = projek.ProvinsiID;
                    cmd.Parameters.Add(sqlParamProvinsiID);

                    SqlParameter sqlParamCityID = new SqlParameter();
                    sqlParamCityID.ParameterName = "cityID";
                    sqlParamCityID.DbType = DbType.String;
                    sqlParamCityID.Value = projek.CityID;
                    cmd.Parameters.Add(sqlParamCityID);

                    SqlParameter sqlParamStatusProject = new SqlParameter();
                    sqlParamStatusProject.ParameterName = "statusProject";
                    sqlParamStatusProject.DbType = DbType.String;
                    sqlParamStatusProject.Value = projek.StatusProject;
                    cmd.Parameters.Add(sqlParamStatusProject);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while( reader.Read())
                            {
                                Project project = new Project();

                                project.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                project.ProjectName = reader["ProjectName"].ToString();
                                project.Quantity = Convert.ToSingle( reader["Quantity"] );
                                project.Nominal = Convert.ToSingle( reader["Nominal"] );
                                project.ContractNumber = reader["ContractNumber"].ToString();
                                project.StatusProject = reader["StatusProject"].ToString();
                                project.Images = reader["Images"].ToString();
                                project.UpdateAt = reader["UpdateAt"].ToString();
                                project.Latitude = Convert.ToSingle(reader["Latitude"]);
                                project.Longitude = Convert.ToSingle(reader["Longitude"]);

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
        [ActionName("GetProjectByClient")]
        public IEnumerable<Project> GetProjectByClient()
        {
            List<Project> result;
            result = new List<Project>();

            result = GetProjectByClientData();
            return result;
        }

        private List<Project> GetProjectByClientData()
        {
            List<Project> result;
            result = new List<Project>();

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
                    cmd.CommandText = "spGetAllProjectByClient";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Project project = new Project();
                                project.ProjectCategoryID = reader["ProjectCategoryID"].ToString();
                                project.ProjectID = reader["ProjectID"].ToString();
                                project.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                project.ProjectName = reader["ProjectName"].ToString();
                                project.ProjectAddress = reader["ProjectAddress"].ToString();
                                project.ProvinsiID = reader["ProvinsiID"].ToString();
                                project.CityID = reader["CityID"].ToString();
                                project.BuildingArea = float.Parse(reader["BuildingArea"].ToString());
                                project.Quantity = float.Parse(reader["Quantity"].ToString());
                                project.Nominal = float.Parse(reader["Nominal"].ToString());
                                project.DeveloperID = reader["DeveloperID"].ToString();
                                project.ProjectManager = reader["ProjectManager"].ToString();
                                project.KontraktorID = reader["KontraktorID"].ToString();
                                project.ConsultantID = reader["ConsultantID"].ToString();
                                project.ManagementKonstruksiID = reader["ManagementKonstruksiID"].ToString();
                                project.ProjectPeriodStart = reader["ProjectPeriodStart"].ToString();
                                project.ProjectPeriodEnd = reader["ProjectPeriodEnd"].ToString();
                                project.SupplierID = reader["SupplierID"].ToString();
                                project.ContractNumber = reader["ContractNumber"].ToString();
                                project.ContractID = reader["ContractID"].ToString();
                                project.StatusProject = reader["StatusProject"].ToString();
                                project.Note = reader["Note"].ToString();
                                project.SalesID = reader["SalesID"].ToString();
                                project.PICID = reader["PICID"].ToString();
                                project.Latitude = float.Parse(reader["Latitude"].ToString());
                                project.Longitude = float.Parse(reader["Longitude"].ToString());
                                project.Images = reader["Images"].ToString();
                                project.UserCreateID = reader["UserCreateID"].ToString();
                                project.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]).ToString("dd MMMM yyyy");
                                project.UserUpdateID = reader["UserUpdateID"].ToString();
                                project.UpdateAt = reader["UpdateAt"].ToString();
                                project.Status = reader["Status"].ToString();
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

        [HttpPost]
        [ActionName("GetSearchProject")]
        public List<Project> GetSearchProject([FromBody] List<SearchProject> projects)
        {
            List<Project> result;
            result = new List<Project>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(projects[0].Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetSearchProjectData(projects[0]);
            //List<SearchProject> result;
            //result = new List<SearchProject>();

            //result = projects;
            return result;
        }

        private List<Project> GetSearchProjectData(SearchProject project)
        {
            List<Project> result;
            result = new List<Project>();
            
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
                    cmd.CommandText = "spSearchProject";

                    SqlParameter sqlParamProjectCategoryID = new SqlParameter();
                    sqlParamProjectCategoryID.ParameterName = "projectCategoryID";
                    sqlParamProjectCategoryID.DbType = DbType.String;
                    sqlParamProjectCategoryID.Value = project.ProjectCategoryID;
                    cmd.Parameters.Add(sqlParamProjectCategoryID);

                    SqlParameter sqlParamProjectName = new SqlParameter();
                    sqlParamProjectName.ParameterName = "projectName";
                    sqlParamProjectName.DbType = DbType.String;
                    sqlParamProjectName.Value = project.ProjectName;
                    cmd.Parameters.Add(sqlParamProjectName);

                    SqlParameter sqlParamProjectAddress = new SqlParameter();
                    sqlParamProjectAddress.ParameterName = "projectAddress";
                    sqlParamProjectAddress.DbType = DbType.String;
                    sqlParamProjectAddress.Value = project.ProjectAddress;
                    cmd.Parameters.Add(sqlParamProjectAddress);

                    SqlParameter sqlParamProvinsiID = new SqlParameter();
                    sqlParamProvinsiID.ParameterName = "provinsiID";
                    sqlParamProvinsiID.DbType = DbType.String;
                    sqlParamProvinsiID.Value = project.ProvinsiID;
                    cmd.Parameters.Add(sqlParamProvinsiID);

                    SqlParameter sqlParamCityID = new SqlParameter();
                    sqlParamCityID.ParameterName = "cityID";
                    sqlParamCityID.DbType = DbType.String;
                    sqlParamCityID.Value = project.CityID;
                    cmd.Parameters.Add(sqlParamCityID);

                    SqlParameter sqlParamBuildingAreaFrom = new SqlParameter();
                    sqlParamBuildingAreaFrom.ParameterName = "buildingAreaFrom";
                    sqlParamBuildingAreaFrom.DbType = DbType.Single;
                    sqlParamBuildingAreaFrom.Value = project.BuildingAreaFrom;
                    cmd.Parameters.Add(sqlParamBuildingAreaFrom);

                    SqlParameter sqlParamBuildingAreaTo = new SqlParameter();
                    sqlParamBuildingAreaTo.ParameterName = "buildingAreaTo";
                    sqlParamBuildingAreaTo.DbType = DbType.Single;
                    sqlParamBuildingAreaTo.Value = project.BuildingAreaTo;
                    cmd.Parameters.Add(sqlParamBuildingAreaTo);

                    SqlParameter sqlParamQuantityFrom = new SqlParameter();
                    sqlParamQuantityFrom.ParameterName = "quantityFrom";
                    sqlParamQuantityFrom.DbType = DbType.Single;
                    sqlParamQuantityFrom.Value = project.QuantityFrom;
                    cmd.Parameters.Add(sqlParamQuantityFrom);

                    SqlParameter sqlParamQuantityTo = new SqlParameter();
                    sqlParamQuantityTo.ParameterName = "quantityTo";
                    sqlParamQuantityTo.DbType = DbType.Single;
                    sqlParamQuantityTo.Value = project.QuantityTo;
                    cmd.Parameters.Add(sqlParamQuantityTo);

                    SqlParameter sqlParamNominalFrom = new SqlParameter();
                    sqlParamNominalFrom.ParameterName = "nominalFrom";
                    sqlParamNominalFrom.DbType = DbType.Single;
                    sqlParamNominalFrom.Value = project.NominalFrom;
                    cmd.Parameters.Add(sqlParamNominalFrom);

                    SqlParameter sqlParamNominalTo = new SqlParameter();
                    sqlParamNominalTo.ParameterName = "nominalTo";
                    sqlParamNominalTo.DbType = DbType.Single;
                    sqlParamNominalTo.Value = project.NominalTo;
                    cmd.Parameters.Add(sqlParamNominalTo);

                    SqlParameter sqlParamDeveloperID = new SqlParameter();
                    sqlParamDeveloperID.ParameterName = "developerID";
                    sqlParamDeveloperID.DbType = DbType.String;
                    sqlParamDeveloperID.Value = project.DeveloperID;
                    cmd.Parameters.Add(sqlParamDeveloperID);

                    SqlParameter sqlParamProjectManager = new SqlParameter();
                    sqlParamProjectManager.ParameterName = "projectManager";
                    sqlParamProjectManager.DbType = DbType.String;
                    sqlParamProjectManager.Value = project.ProjectManager;
                    cmd.Parameters.Add(sqlParamProjectManager);

                    SqlParameter sqlParamKontraktorID = new SqlParameter();
                    sqlParamKontraktorID.ParameterName = "kontraktorID";
                    sqlParamKontraktorID.DbType = DbType.String;
                    sqlParamKontraktorID.Value = project.KontraktorID;
                    cmd.Parameters.Add(sqlParamKontraktorID);

                    SqlParameter sqlParamConsultantID = new SqlParameter();
                    sqlParamConsultantID.ParameterName = "consultantID";
                    sqlParamConsultantID.DbType = DbType.String;
                    sqlParamConsultantID.Value = project.ConsultantID;
                    cmd.Parameters.Add(sqlParamConsultantID);

                    SqlParameter sqlParamManagementKonstruksiID = new SqlParameter();
                    sqlParamManagementKonstruksiID.ParameterName = "managementKonstruksiID";
                    sqlParamManagementKonstruksiID.DbType = DbType.String;
                    sqlParamManagementKonstruksiID.Value = project.ManagementKonstruksiID;
                    cmd.Parameters.Add(sqlParamManagementKonstruksiID);

                    SqlParameter sqlParamProjectPeriodStartFrom = new SqlParameter();
                    sqlParamProjectPeriodStartFrom.ParameterName = "projectPeriodStartFrom";
                    sqlParamProjectPeriodStartFrom.DbType = DbType.String;
                    sqlParamProjectPeriodStartFrom.Value = project.ProjectPeriodStartFrom;
                    cmd.Parameters.Add(sqlParamProjectPeriodStartFrom);

                    SqlParameter sqlParamProjectPeriodStartTo = new SqlParameter();
                    sqlParamProjectPeriodStartTo.ParameterName = "projectPeriodStartTo";
                    sqlParamProjectPeriodStartTo.DbType = DbType.String;
                    sqlParamProjectPeriodStartTo.Value = project.ProjectPeriodStartTo;
                    cmd.Parameters.Add(sqlParamProjectPeriodStartTo);

                    SqlParameter sqlParamProjectPeriodEndFrom = new SqlParameter();
                    sqlParamProjectPeriodEndFrom.ParameterName = "projectPeriodEndFrom";
                    sqlParamProjectPeriodEndFrom.DbType = DbType.String;
                    sqlParamProjectPeriodEndFrom.Value = project.ProjectPeriodEndFrom;
                    cmd.Parameters.Add(sqlParamProjectPeriodEndFrom);

                    SqlParameter sqlParamProjectPeriodEndTo = new SqlParameter();
                    sqlParamProjectPeriodEndTo.ParameterName = "projectPeriodEndTo";
                    sqlParamProjectPeriodEndTo.DbType = DbType.String;
                    sqlParamProjectPeriodEndTo.Value = project.ProjectPeriodEndTo;
                    cmd.Parameters.Add(sqlParamProjectPeriodEndTo);

                    SqlParameter sqlParamSupplierID = new SqlParameter();
                    sqlParamSupplierID.ParameterName = "supplierID";
                    sqlParamSupplierID.DbType = DbType.String;
                    sqlParamSupplierID.Value = project.SupplierID;
                    cmd.Parameters.Add(sqlParamSupplierID);

                    SqlParameter sqlParamContractNumber = new SqlParameter();
                    sqlParamContractNumber.ParameterName = "contractNo";
                    sqlParamContractNumber.DbType = DbType.String;
                    sqlParamContractNumber.Value = project.ContractNumber;
                    cmd.Parameters.Add(sqlParamContractNumber);

                    SqlParameter sqlParamStatusProject = new SqlParameter();
                    sqlParamStatusProject.ParameterName = "statusProject";
                    sqlParamStatusProject.DbType = DbType.String;
                    sqlParamStatusProject.Value = project.StatusProject;
                    cmd.Parameters.Add(sqlParamStatusProject);

                    SqlParameter sqlParamNote = new SqlParameter();
                    sqlParamNote.ParameterName = "note";
                    sqlParamNote.DbType = DbType.String;
                    sqlParamNote.Value = project.Note;
                    cmd.Parameters.Add(sqlParamNote);

                    SqlParameter sqlParamSalesID = new SqlParameter();
                    sqlParamSalesID.ParameterName = "salesID";
                    sqlParamSalesID.DbType = DbType.String;
                    sqlParamSalesID.Value = project.SalesID;
                    cmd.Parameters.Add(sqlParamSalesID);

                    SqlParameter sqlParamPICID = new SqlParameter();
                    sqlParamPICID.ParameterName = "picID";
                    sqlParamPICID.DbType = DbType.String;
                    sqlParamPICID.Value = project.PICID;
                    cmd.Parameters.Add(sqlParamPICID);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Project projek = new Project();

                                //projek.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                //projek.ProjectName = reader["ProjectName"].ToString();
                                //projek.Quantity = Convert.ToSingle(reader["Quantity"]);
                                //projek.Nominal = Convert.ToSingle(reader["Nominal"]);
                                //projek.ContractNumber = reader["ContractNumber"].ToString();
                                //projek.StatusProject = reader["StatusProject"].ToString();
                                //projek.Images = reader["Images"].ToString();
                                //projek.UpdateAt = reader["UpdateAt"].ToString();
                                //projek.Latitude = Convert.ToSingle(reader["Latitude"]);
                                //projek.Longitude = Convert.ToSingle(reader["Longitude"]);

                                projek.ProjectCategoryID = reader["ProjectCategoryID"].ToString();
                                projek.ProjectID = reader["ProjectID"].ToString();
                                projek.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                                projek.ProjectName = reader["ProjectName"].ToString();
                                projek.ProjectAddress = reader["ProjectAddress"].ToString();
                                projek.ProvinsiID = reader["ProvinsiID"].ToString();
                                projek.CityID = reader["CityID"].ToString();
                                projek.BuildingArea = float.Parse(reader["BuildingArea"].ToString());
                                projek.Quantity = float.Parse(reader["Quantity"].ToString());
                                projek.Nominal = float.Parse(reader["Nominal"].ToString());
                                projek.DeveloperID = reader["DeveloperID"].ToString();
                                projek.ProjectManager = reader["ProjectManager"].ToString();
                                projek.KontraktorID = reader["KontraktorID"].ToString();
                                projek.ConsultantID = reader["ConsultantID"].ToString();
                                projek.ManagementKonstruksiID = reader["ManagementKonstruksiID"].ToString();
                                projek.ProjectPeriodStart = reader["ProjectPeriodStart"].ToString();
                                projek.ProjectPeriodEnd = reader["ProjectPeriodEnd"].ToString();
                                projek.SupplierID = reader["SupplierID"].ToString();
                                projek.ContractNumber = reader["ContractNumber"].ToString();
                                projek.ContractID = reader["ContractID"].ToString();
                                projek.StatusProject = reader["StatusProject"].ToString();
                                projek.Note = reader["Note"].ToString();
                                projek.SalesID = reader["SalesID"].ToString();
                                projek.PICID = reader["PICID"].ToString();
                                projek.Latitude = float.Parse(reader["Latitude"].ToString());
                                projek.Longitude = float.Parse(reader["Longitude"].ToString());
                                projek.Images = reader["Images"].ToString();
                                projek.UserCreateID = reader["UserCreateID"].ToString();
                                projek.UserName = reader["UserName"].ToString();
                                projek.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]).ToString("dd MMMM yyyy");
                                projek.UserUpdateID = reader["UserUpdateID"].ToString();
                                projek.UpdateAt = Convert.ToDateTime(reader["UpdateAt"]).ToString("dd MMMM yyyy");
                                projek.Status = reader["Status"].ToString();

                                result.Add(projek);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Project pro = new Project();
                    pro.ProjectIDMobile = ex.Message;
                    result.Add(pro);
//                    throw ex;
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
        [ActionName("GetProjectByID")]
        public Project GetProjectByID(string token, string projectIDMobile)
        {
            Project result;
            result = new Project();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetProjectByIDData(projectIDMobile);
            return result;
        }

        private Project GetProjectByIDData(string projectIDMobile)
        {
            Project result;
            result = new Project();

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
                    cmd.CommandText = "spSelectProjectByProjectIDMobile";

                    SqlParameter sqlParamProjectIDMobile = new SqlParameter();
                    sqlParamProjectIDMobile.ParameterName = "projectIDMobile";
                    sqlParamProjectIDMobile.DbType = DbType.String;
                    sqlParamProjectIDMobile.Value = projectIDMobile;
                    cmd.Parameters.Add(sqlParamProjectIDMobile);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            result.ProjectCategoryID = reader["ProjectCategoryID"].ToString();
                            result.ProjectID = reader["ProjectID"].ToString();
                            result.ProjectIDMobile = reader["ProjectIDMobile"].ToString();
                            result.ProjectName = reader["ProjectName"].ToString();
                            result.ProjectAddress = reader["ProjectAddress"].ToString();
                            result.ProvinsiID = reader["ProvinsiID"].ToString();
                            result.CityID = reader["CityID"].ToString();
                            result.BuildingArea = Convert.ToSingle(reader["BuildingArea"]);
                            result.Quantity = Convert.ToSingle(reader["Quantity"]);
                            result.Nominal = Convert.ToSingle(reader["Nominal"]);
                            result.DeveloperID = reader["DeveloperID"].ToString();
                            result.ProjectManager = reader["ProjectManager"].ToString();
                            result.KontraktorID = reader["KontraktorID"].ToString();
                            result.ConsultantID = reader["ConsultantID"].ToString();
                            result.ManagementKonstruksiID = reader["ManagementKonstruksiID"].ToString();
                            result.ProjectPeriodStart = Convert.ToDateTime(reader["ProjectPeriodStart"]).ToString("dd MMMM yyyy");
                            result.ProjectPeriodEnd = Convert.ToDateTime(reader["ProjectPeriodEnd"]).ToString("dd MMMM yyyy");
                            result.SupplierID = reader["SupplierID"].ToString();
                            result.ContractNumber = reader["ContractNumber"].ToString();
                            result.ContractID = reader["ContractID"].ToString();
                            result.StatusProject = reader["StatusProject"].ToString();
                            result.Note = reader["Note"].ToString();
                            result.SalesID = reader["SalesID"].ToString();
                            result.PICID = reader["PICID"].ToString();
                            result.Latitude = Convert.ToSingle(reader["Latitude"]);
                            result.Longitude = Convert.ToSingle(reader["Longitude"]);
                            result.Images = reader["Images"].ToString();

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
        public Project SaveProject([FromBody] Project project)
        {
            Project result;
            result = new Project();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(project.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = SaveProjectData(project);
            return result;
        }

        private Project SaveProjectData(Project project)
        {
            Project result;
            result = new Project();

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

                    SqlParameter sqlParamProjectCategoryID = new SqlParameter();
                    sqlParamProjectCategoryID.ParameterName = "projectCategoryID";
                    sqlParamProjectCategoryID.DbType = DbType.String;
                    sqlParamProjectCategoryID.Value = project.ProjectCategoryID;
                    cmd.Parameters.Add(sqlParamProjectCategoryID);

                    SqlParameter sqlParamProjectIDMobile = new SqlParameter();
                    sqlParamProjectIDMobile.ParameterName = "projectIDMobile";
                    sqlParamProjectIDMobile.DbType = DbType.String;
                    sqlParamProjectIDMobile.Value = project.ProjectIDMobile;
                    cmd.Parameters.Add(sqlParamProjectIDMobile);

                    SqlParameter sqlParamProjectName = new SqlParameter();
                    sqlParamProjectName.ParameterName = "projectName";
                    sqlParamProjectName.DbType = DbType.String;
                    sqlParamProjectName.Value = project.ProjectName;
                    cmd.Parameters.Add(sqlParamProjectName);

                    SqlParameter sqlParamProjectAddress = new SqlParameter();
                    sqlParamProjectAddress.ParameterName = "projectAddress";
                    sqlParamProjectAddress.DbType = DbType.String;
                    sqlParamProjectAddress.Value = project.ProjectAddress;
                    cmd.Parameters.Add(sqlParamProjectAddress);

                    SqlParameter sqlParamProvinsiID = new SqlParameter();
                    sqlParamProvinsiID.ParameterName = "provinsiID";
                    sqlParamProvinsiID.DbType = DbType.String;
                    sqlParamProvinsiID.Value = project.ProvinsiID;
                    cmd.Parameters.Add(sqlParamProvinsiID);

                    SqlParameter sqlParamCityID = new SqlParameter();
                    sqlParamCityID.ParameterName = "cityID";
                    sqlParamCityID.DbType = DbType.String;
                    sqlParamCityID.Value = project.CityID;
                    cmd.Parameters.Add(sqlParamCityID);

                    SqlParameter sqlParamBuildingArea = new SqlParameter();
                    sqlParamBuildingArea.ParameterName = "buildingArea";
                    sqlParamBuildingArea.DbType = DbType.Single;
                    sqlParamBuildingArea.Value = project.BuildingArea;
                    cmd.Parameters.Add(sqlParamBuildingArea);

                    SqlParameter sqlParamQuantity = new SqlParameter();
                    sqlParamQuantity.ParameterName = "quantity";
                    sqlParamQuantity.DbType = DbType.Single;
                    sqlParamQuantity.Value = project.Quantity;
                    cmd.Parameters.Add(sqlParamQuantity);

                    SqlParameter sqlParamNominal = new SqlParameter();
                    sqlParamNominal.ParameterName = "nominal";
                    sqlParamNominal.DbType = DbType.Single;
                    sqlParamNominal.Value = project.Nominal;
                    cmd.Parameters.Add(sqlParamNominal);

                    SqlParameter sqlParamDeveloperID = new SqlParameter();
                    sqlParamDeveloperID.ParameterName = "developerID";
                    sqlParamDeveloperID.DbType = DbType.String;
                    sqlParamDeveloperID.Value = project.DeveloperID;
                    cmd.Parameters.Add(sqlParamDeveloperID);

                    SqlParameter sqlParamProjectManager = new SqlParameter();
                    sqlParamProjectManager.ParameterName = "projectManager";
                    sqlParamProjectManager.DbType = DbType.String;
                    sqlParamProjectManager.Value = project.ProjectManager;
                    cmd.Parameters.Add(sqlParamProjectManager);

                    SqlParameter sqlParamKontraktorID = new SqlParameter();
                    sqlParamKontraktorID.ParameterName = "kontraktorID";
                    sqlParamKontraktorID.DbType = DbType.String;
                    sqlParamKontraktorID.Value = project.KontraktorID;
                    cmd.Parameters.Add(sqlParamKontraktorID);

                    SqlParameter sqlParamConsultantID = new SqlParameter();
                    sqlParamConsultantID.ParameterName = "consultantID";
                    sqlParamConsultantID.DbType = DbType.String;
                    sqlParamConsultantID.Value = project.ConsultantID;
                    cmd.Parameters.Add(sqlParamConsultantID);

                    SqlParameter sqlParamManagementKonstruksiID = new SqlParameter();
                    sqlParamManagementKonstruksiID.ParameterName = "managementKonstruksiID";
                    sqlParamManagementKonstruksiID.DbType = DbType.String;
                    sqlParamManagementKonstruksiID.Value = project.ManagementKonstruksiID;
                    cmd.Parameters.Add(sqlParamManagementKonstruksiID);

                    SqlParameter sqlParamProjectPeriodStart = new SqlParameter();
                    sqlParamProjectPeriodStart.ParameterName = "projectPeriodStart";
                    sqlParamProjectPeriodStart.DbType = DbType.String;
                    sqlParamProjectPeriodStart.Value = project.ProjectPeriodStart;
                    cmd.Parameters.Add(sqlParamProjectPeriodStart);

                    SqlParameter sqlParamProjectPeriodEnd = new SqlParameter();
                    sqlParamProjectPeriodEnd.ParameterName = "projectPeriodEnd";
                    sqlParamProjectPeriodEnd.DbType = DbType.String;
                    sqlParamProjectPeriodEnd.Value = project.ProjectPeriodEnd;
                    cmd.Parameters.Add(sqlParamProjectPeriodEnd);

                    SqlParameter sqlParamSupplierID = new SqlParameter();
                    sqlParamSupplierID.ParameterName = "supplierID";
                    sqlParamSupplierID.DbType = DbType.String;
                    sqlParamSupplierID.Value = project.SupplierID;
                    cmd.Parameters.Add(sqlParamSupplierID);

                    SqlParameter sqlParamStatusProject = new SqlParameter();
                    sqlParamStatusProject.ParameterName = "statusProject";
                    sqlParamStatusProject.DbType = DbType.String;
                    sqlParamStatusProject.Value = project.StatusProject;
                    cmd.Parameters.Add(sqlParamStatusProject);

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

                    SqlParameter sqlParamImages = new SqlParameter();
                    sqlParamImages.ParameterName = "images";
                    sqlParamImages.DbType = DbType.String;
                    sqlParamImages.Value = project.Images;
                    cmd.Parameters.Add(sqlParamImages);

                    SqlParameter sqlParamUserCreateID = new SqlParameter();
                    sqlParamUserCreateID.ParameterName = "userCreateID";
                    sqlParamUserCreateID.DbType = DbType.String;
                    sqlParamUserCreateID.Value = project.UserCreateID;
                    cmd.Parameters.Add(sqlParamUserCreateID);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result.ProjectID = "Sukses";

                }
                catch (Exception ex)
                {
                    result.ProjectID = ex.Message;
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
        [ActionName("EditProject")]
        public Project EditProject([FromBody] Project project)
        {
            Project result = new Project();
            result.ProjectID = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(project.Token);

            if (checkToken == 0)
            {
                return result;
            }

            result = EditProjectData(project);
            return result;
        }

        private Project EditProjectData(Project project)
        {
            Project result = new Project();

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
                    cmd.CommandText = "spUpdateProjectByProjectIDMobile";

                    SqlParameter sqlParamProjectCategoryID = new SqlParameter();
                    sqlParamProjectCategoryID.ParameterName = "projectCategoryID";
                    sqlParamProjectCategoryID.DbType = DbType.String;
                    sqlParamProjectCategoryID.Value = project.ProjectCategoryID;
                    cmd.Parameters.Add(sqlParamProjectCategoryID);

                    SqlParameter sqlParamProjectIDMobile = new SqlParameter();
                    sqlParamProjectIDMobile.ParameterName = "projectIDMobile";
                    sqlParamProjectIDMobile.DbType = DbType.String;
                    sqlParamProjectIDMobile.Value = project.ProjectIDMobile;
                    cmd.Parameters.Add(sqlParamProjectIDMobile);

                    SqlParameter sqlParamProjectName = new SqlParameter();
                    sqlParamProjectName.ParameterName = "projectName";
                    sqlParamProjectName.DbType = DbType.String;
                    sqlParamProjectName.Value = project.ProjectName;
                    cmd.Parameters.Add(sqlParamProjectName);

                    SqlParameter sqlParamProjectAddress = new SqlParameter();
                    sqlParamProjectAddress.ParameterName = "projectAddress";
                    sqlParamProjectAddress.DbType = DbType.String;
                    sqlParamProjectAddress.Value = project.ProjectAddress;
                    cmd.Parameters.Add(sqlParamProjectAddress);

                    SqlParameter sqlParamProvinsiID = new SqlParameter();
                    sqlParamProvinsiID.ParameterName = "provinsiID";
                    sqlParamProvinsiID.DbType = DbType.String;
                    sqlParamProvinsiID.Value = project.ProvinsiID;
                    cmd.Parameters.Add(sqlParamProvinsiID);

                    SqlParameter sqlParamCityID = new SqlParameter();
                    sqlParamCityID.ParameterName = "cityID";
                    sqlParamCityID.DbType = DbType.String;
                    sqlParamCityID.Value = project.CityID;
                    cmd.Parameters.Add(sqlParamCityID);

                    SqlParameter sqlParamBuildingArea = new SqlParameter();
                    sqlParamBuildingArea.ParameterName = "buildingArea";
                    sqlParamBuildingArea.DbType = DbType.Single;
                    sqlParamBuildingArea.Value = project.BuildingArea;
                    cmd.Parameters.Add(sqlParamBuildingArea);

                    SqlParameter sqlParamQuantity = new SqlParameter();
                    sqlParamQuantity.ParameterName = "quantity";
                    sqlParamQuantity.DbType = DbType.Single;
                    sqlParamQuantity.Value = project.Quantity;
                    cmd.Parameters.Add(sqlParamQuantity);

                    SqlParameter sqlParamNominal = new SqlParameter();
                    sqlParamNominal.ParameterName = "nominal";
                    sqlParamNominal.DbType = DbType.Single;
                    sqlParamNominal.Value = project.Nominal;
                    cmd.Parameters.Add(sqlParamNominal);

                    SqlParameter sqlParamDeveloperID = new SqlParameter();
                    sqlParamDeveloperID.ParameterName = "developerID";
                    sqlParamDeveloperID.DbType = DbType.String;
                    sqlParamDeveloperID.Value = project.DeveloperID;
                    cmd.Parameters.Add(sqlParamDeveloperID);

                    SqlParameter sqlParamProjectManager = new SqlParameter();
                    sqlParamProjectManager.ParameterName = "projectManager";
                    sqlParamProjectManager.DbType = DbType.String;
                    sqlParamProjectManager.Value = project.ProjectManager;
                    cmd.Parameters.Add(sqlParamProjectManager);

                    SqlParameter sqlParamKontraktorID = new SqlParameter();
                    sqlParamKontraktorID.ParameterName = "kontraktorID";
                    sqlParamKontraktorID.DbType = DbType.String;
                    sqlParamKontraktorID.Value = project.KontraktorID;
                    cmd.Parameters.Add(sqlParamKontraktorID);

                    SqlParameter sqlParamConsultantID = new SqlParameter();
                    sqlParamConsultantID.ParameterName = "consultantID";
                    sqlParamConsultantID.DbType = DbType.String;
                    sqlParamConsultantID.Value = project.ConsultantID;
                    cmd.Parameters.Add(sqlParamConsultantID);

                    SqlParameter sqlParamManagementKonstruksiID = new SqlParameter();
                    sqlParamManagementKonstruksiID.ParameterName = "managementKonstruksiID";
                    sqlParamManagementKonstruksiID.DbType = DbType.String;
                    sqlParamManagementKonstruksiID.Value = project.ManagementKonstruksiID;
                    cmd.Parameters.Add(sqlParamManagementKonstruksiID);

                    SqlParameter sqlParamProjectPeriodStart = new SqlParameter();
                    sqlParamProjectPeriodStart.ParameterName = "projectPeriodStart";
                    sqlParamProjectPeriodStart.DbType = DbType.String;
                    sqlParamProjectPeriodStart.Value = project.ProjectPeriodStart;
                    cmd.Parameters.Add(sqlParamProjectPeriodStart);

                    SqlParameter sqlParamProjectPeriodEnd = new SqlParameter();
                    sqlParamProjectPeriodEnd.ParameterName = "projectPeriodEnd";
                    sqlParamProjectPeriodEnd.DbType = DbType.String;
                    sqlParamProjectPeriodEnd.Value = project.ProjectPeriodEnd;
                    cmd.Parameters.Add(sqlParamProjectPeriodEnd);

                    SqlParameter sqlParamSupplierID = new SqlParameter();
                    sqlParamSupplierID.ParameterName = "supplierID";
                    sqlParamSupplierID.DbType = DbType.String;
                    sqlParamSupplierID.Value = project.SupplierID;
                    cmd.Parameters.Add(sqlParamSupplierID);

                    SqlParameter sqlParamStatusProject = new SqlParameter();
                    sqlParamStatusProject.ParameterName = "statusProject";
                    sqlParamStatusProject.DbType = DbType.String;
                    sqlParamStatusProject.Value = project.StatusProject;
                    cmd.Parameters.Add(sqlParamStatusProject);

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

                    SqlParameter sqlParamImages = new SqlParameter();
                    sqlParamImages.ParameterName = "images";
                    sqlParamImages.DbType = DbType.String;
                    sqlParamImages.Value = project.Images;
                    cmd.Parameters.Add(sqlParamImages);

                    SqlParameter sqlParamUserUpdateID = new SqlParameter();
                    sqlParamUserUpdateID.ParameterName = "userUpdateID";
                    sqlParamUserUpdateID.DbType = DbType.String;
                    sqlParamUserUpdateID.Value = project.UserUpdateID;
                    cmd.Parameters.Add(sqlParamUserUpdateID);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result.ProjectID = "Sukses";

                }
                catch (Exception ex)
                {
                    result.ProjectID = ex.Message;
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
        public String DeleteProject(string token, string projectIDMobile)
        {
            string result;
            result = "Fail";

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = DeleteProjectData(projectIDMobile);
            return result;
        }

        private String DeleteProjectData(string projectIDMobile)
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
                    cmd.CommandText = "spDeleteProjectByProjectIDMobile";

                    SqlParameter sqlParamProjectIDMobile = new SqlParameter();
                    sqlParamProjectIDMobile.ParameterName = "projectIDMobile";
                    sqlParamProjectIDMobile.DbType = DbType.String;
                    sqlParamProjectIDMobile.Value = projectIDMobile;
                    cmd.Parameters.Add(sqlParamProjectIDMobile);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    cmd.ExecuteNonQuery();

                    result = "Success";

                }
                catch (Exception ex)
                {
                    result = "Fail";
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