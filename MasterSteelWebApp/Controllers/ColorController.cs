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
    public class ColorController : ApiController
    {
        private DbProviderFactory dbProviderFactory;

        [HttpPost]
        [ActionName("GetColorById")]
        public Color GetColorById([FromBody] Color color)
        {
            Color result;
            result = new Color();
            result = GetColorByIdData(color);
            return result;
        }
        private Color GetColorByIdData(Color color)
        {
            Color result;
            result = new Color();

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
                    cmd.CommandText = "spGetColorById";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = color.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result.HeaderColor = reader["HeaderColor"].ToString();
                            result.BodyColor = reader["BodyColor"].ToString();
                            result.MenuColor = reader["MenuColor"].ToString();
                            result.FooterColor = reader["FooterColor"].ToString();
                            result.TextColor = reader["TextColor"].ToString();
                            result.HoverColor = reader["HoverColor"].ToString();
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
        [ActionName("SaveColor")]
        public String SaveColor([FromBody] Color color)
        {
            string result;
            result = SaveColorData(color);
            return result;
        }

        private String SaveColorData(Color color)
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
                    cmd.CommandText = "spInsertColor";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = color.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamHeaderColor = new SqlParameter();
                    sqlParamHeaderColor.ParameterName = "headerColor";
                    sqlParamHeaderColor.DbType = DbType.String;
                    sqlParamHeaderColor.Value = color.HeaderColor;
                    cmd.Parameters.Add(sqlParamHeaderColor);

                    SqlParameter sqlParamBodyColor = new SqlParameter();
                    sqlParamBodyColor.ParameterName = "bodyColor";
                    sqlParamBodyColor.DbType = DbType.String;
                    sqlParamBodyColor.Value = color.BodyColor;
                    cmd.Parameters.Add(sqlParamBodyColor);

                    SqlParameter sqlParamMenuColor = new SqlParameter();
                    sqlParamMenuColor.ParameterName = "menuColor";
                    sqlParamMenuColor.DbType = DbType.String;
                    sqlParamMenuColor.Value = color.MenuColor;
                    cmd.Parameters.Add(sqlParamMenuColor);

                    SqlParameter sqlParamFooterColor = new SqlParameter();
                    sqlParamFooterColor.ParameterName = "footerColor";
                    sqlParamFooterColor.DbType = DbType.String;
                    sqlParamFooterColor.Value = color.FooterColor;
                    cmd.Parameters.Add(sqlParamFooterColor);

                    SqlParameter sqlParamTextColor = new SqlParameter();
                    sqlParamTextColor.ParameterName = "textColor";
                    sqlParamTextColor.DbType = DbType.String;
                    sqlParamTextColor.Value = color.TextColor;
                    cmd.Parameters.Add(sqlParamTextColor);

                    SqlParameter sqlParamHoverColor = new SqlParameter();
                    sqlParamHoverColor.ParameterName = "hoverColor";
                    sqlParamHoverColor.DbType = DbType.String;
                    sqlParamHoverColor.Value = color.HoverColor;
                    cmd.Parameters.Add(sqlParamHoverColor);

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
        [ActionName("UpdateColor")]
        public String UpdateColor([FromBody] Color color)
        {
            string result;
            result = UpdateColorData(color);
            return result;
        }

        private String UpdateColorData(Color color)
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
                    cmd.CommandText = "spUpdateColorByUser";

                    SqlParameter sqlParamUserId = new SqlParameter();
                    sqlParamUserId.ParameterName = "userId";
                    sqlParamUserId.DbType = DbType.String;
                    sqlParamUserId.Value = color.UserId;
                    cmd.Parameters.Add(sqlParamUserId);

                    SqlParameter sqlParamHeaderColor = new SqlParameter();
                    sqlParamHeaderColor.ParameterName = "headerColor";
                    sqlParamHeaderColor.DbType = DbType.String;
                    sqlParamHeaderColor.Value = color.HeaderColor;
                    cmd.Parameters.Add(sqlParamHeaderColor);

                    SqlParameter sqlParamBodyColor = new SqlParameter();
                    sqlParamBodyColor.ParameterName = "bodyColor";
                    sqlParamBodyColor.DbType = DbType.String;
                    sqlParamBodyColor.Value = color.BodyColor;
                    cmd.Parameters.Add(sqlParamBodyColor);

                    SqlParameter sqlParamMenuColor = new SqlParameter();
                    sqlParamMenuColor.ParameterName = "menuColor";
                    sqlParamMenuColor.DbType = DbType.String;
                    sqlParamMenuColor.Value = color.MenuColor;
                    cmd.Parameters.Add(sqlParamMenuColor);

                    SqlParameter sqlParamFooterColor = new SqlParameter();
                    sqlParamFooterColor.ParameterName = "footerColor";
                    sqlParamFooterColor.DbType = DbType.String;
                    sqlParamFooterColor.Value = color.FooterColor;
                    cmd.Parameters.Add(sqlParamFooterColor);

                    SqlParameter sqlParamTextColor = new SqlParameter();
                    sqlParamTextColor.ParameterName = "textColor";
                    sqlParamTextColor.DbType = DbType.String;
                    sqlParamTextColor.Value = color.TextColor;
                    cmd.Parameters.Add(sqlParamTextColor);

                    SqlParameter sqlParamHoverColor = new SqlParameter();
                    sqlParamHoverColor.ParameterName = "hoverColor";
                    sqlParamHoverColor.DbType = DbType.String;
                    sqlParamHoverColor.Value = color.HoverColor;
                    cmd.Parameters.Add(sqlParamHoverColor);

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
