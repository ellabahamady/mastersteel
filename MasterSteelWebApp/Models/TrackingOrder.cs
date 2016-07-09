using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class TrackingOrder
    {
        public string NomorPO { get; set; }
        public string NomorPickSlip { get; set; }
        public string TanggalPengiriman { get; set; }
        public string NomorKendaraan { get; set; }
        public int StatusOrderMuat { get; set; }
        public string OrderMuat { get; set; }
        public int StatusGudang { get; set; }
        public string Gudang { get; set; }
        public int StatusSuratJalan { get; set; }
        public string SuratJalan { get; set; }
    }
}