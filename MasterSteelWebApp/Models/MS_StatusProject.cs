using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class MS_StatusProject
    {
        public string StatusProjectID { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }
    }
}