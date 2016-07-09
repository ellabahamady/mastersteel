using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class Invoice
    {
        public string ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string NomorInvoice {get; set;}
        public string NomorFaktur {get; set;}
        public string TanggalInvoice {get; set;}
        public string TanggalTerima {get; set;}
        public string TanggalJatuhTempo {get; set;}
        public float Jumlah {get; set;}
        public float Pembayaran {get; set;}
    }
}