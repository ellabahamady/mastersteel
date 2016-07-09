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
    public class NotifikasiController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpGet]
        [ActionName("GetNotifikasi3HariFakturTagihan")]
        public IEnumerable<Notifikasi> GetNotifikasi3HariFakturTagihan(string token)
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetNotifikasi3HariFakturTagihanData();
            return result;
        }

        private List<Notifikasi> GetNotifikasi3HariFakturTagihanData()
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

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
                    cmd.CommandText = "spNotifikasi3HariFakturTagihan";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Notifikasi reading = new Notifikasi();
                                reading.Title = reader["Title"].ToString();
                                reading.Body = reader["Body"].ToString();
                                reading.Id = reader["Id"].ToString();

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
        [ActionName("GetNotifikasiFakturLebih1Hari")]
        public IEnumerable<Notifikasi> GetNotifikasiFakturLebih1Hari(string token)
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetNotifikasiFakturLebih1HariData();
            return result;
        }

        private List<Notifikasi> GetNotifikasiFakturLebih1HariData()
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

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
                    cmd.CommandText = "spNotifikasiFakturLebih1Hari";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Notifikasi reading = new Notifikasi();
                                reading.Title = reader["Title"].ToString();
                                reading.Body = reader["Body"].ToString();
                                reading.Id = reader["Id"].ToString();

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
        [ActionName("GetNotifikasi30HariSebelumKontrakBerakhir")]
        public IEnumerable<Notifikasi> GetNotifikasi30HariSebelumKontrakBerakhir(string token)
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetNotifikasi30HariSebelumKontrakBerakhirData();
            return result;
        }

        private List<Notifikasi> GetNotifikasi30HariSebelumKontrakBerakhirData()
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

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
                    cmd.CommandText = "spNotifikasi30HariSebelumKontrakBerakhir";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Notifikasi reading = new Notifikasi();
                                reading.Title = reader["Title"].ToString();
                                reading.Body = reader["Body"].ToString();
                                reading.Id = reader["Id"].ToString();

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
        [ActionName("GetNotifikasi14HariSetelahOrderKontrak")]
        public IEnumerable<Notifikasi> GetNotifikasi14HariSetelahOrderKontrak(string token)
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

            UsersController usersController = new UsersController();
            int checkToken = 0;
            checkToken = usersController.CheckToken(token);

            if (checkToken == 0)
            {
                return result;
            }

            result = GetNotifikasi14HariSetelahOrderKontrakData();
            return result;
        }

        private List<Notifikasi> GetNotifikasi14HariSetelahOrderKontrakData()
        {
            List<Notifikasi> result;
            result = new List<Notifikasi>();

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
                    cmd.CommandText = "spNotifikasi14HariSetelahOrderKontrak";

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Notifikasi reading = new Notifikasi();
                                reading.Title = reader["Title"].ToString();
                                reading.Body = reader["Body"].ToString();
                                reading.Id = reader["Id"].ToString();

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
