using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class MS_Users
    {
        public string Token {get; set;}
        public string UserGenerateID { get; set; }
        public string UserID {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public string NewPassword { get; set; }
        public string LocactionID { get; set; }
        public string DepartemenID {get; set;}
        public string Status {get; set;}
        public string Mail {get; set;}
        public Boolean VerifikasiMail {get; set;}
        public string Type {get; set;}
        public string UserUpdateID {get; set;}
        public string UpdateAt { get; set; }
    }
}