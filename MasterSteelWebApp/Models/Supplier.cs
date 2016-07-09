using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class Supplier
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string UserCreateId { get; set; }
        public string CreatedAt { get; set; }
        public string UserUpdateId { get; set; }
        public string UpdateAt { get; set; }
    }
}