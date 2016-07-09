using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class User
    {

        public string Id { get; set; }
        public string NIK { get; set; }
        public string Nama { get; set; }
        public string JabatanId { get; set; }
        public string JabatanName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string UserCreateId { get; set; }
        public string CreatedAt { get; set; }
        public string UserUpdateId { get; set; }
        public string UpdateAt { get; set; }
        public string LoginMachineId { get; set; }
        public bool Active { get; set; }
        public bool EmailVerified { get; set; }

    }
}