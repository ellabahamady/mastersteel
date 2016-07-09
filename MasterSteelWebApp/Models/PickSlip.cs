using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class PickSlip
    {
        public string NomorPO { get; set; }
        public string NomorPickSlip { get; set; }
        public string TanggalPengiriman { get; set; }
        public string NomorKendaraan { get; set; }
        public string DetailNamaBarang { get; set; }
        public string DetailQuantity { get; set; }
        public string DetailSatuan { get; set; }

    }
}