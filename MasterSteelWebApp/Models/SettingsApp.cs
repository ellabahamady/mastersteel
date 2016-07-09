using MasterSteelWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MasterSteelWebApp.Models
{
    public class SettingsApp : ISettings
    {
        public ConnectionStringSettings MasterSteelConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionDatabase"]; }
        }
    }
}