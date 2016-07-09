using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class ClientProject
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string UserID { get; set; }
        public string ProjectList { get; set; }
        public string ProjectListName { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string UserName { get; set; }
    }
}