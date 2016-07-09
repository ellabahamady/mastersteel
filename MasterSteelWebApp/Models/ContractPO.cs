using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class ContractPO
    {
        public string ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string ContractDate { get; set; }
        public string POId { get; set; }
        public string TanggalPengiriman { get; set; }
    }
}