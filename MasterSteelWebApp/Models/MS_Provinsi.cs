using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class MS_Provinsi
    {
        public string ProvinsiID { get; set; }
        public string Description { get; set; }

        //tambah field baru
        public string CountryID { get; set; }
        public string Status { get; set; }
        public string UpdateAt { get; set; }
        //tambah field baru

    }
}