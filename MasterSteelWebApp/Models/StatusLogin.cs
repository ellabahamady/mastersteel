using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class Login
    {

        public string StatusLogin { get; set; }

        public string Result { get; set; }
        public string NIK { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string Email { get; set; }

    }
}