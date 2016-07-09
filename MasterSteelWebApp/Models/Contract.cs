using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class Contract
    {
        public string ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string ProjectName { get; set; }
        public string ContractDate { get; set; }
        public string ExpiredDate { get; set; }
        public float Amount { get; set; }
        public float Quantity { get; set; }
        public string Satuan { get; set; }
    }
}