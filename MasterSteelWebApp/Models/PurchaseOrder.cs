using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class PurchaseOrder
    {
        public string POId { get; set;}
        public string NomorPO { get; set;}
        public string TanggalPO { get; set;}
        public string POExpired { get; set;}
        public string Amount { get; set;}
        public string Quantity { get; set;}
        public string Status { get; set;}
        public string PIC { get; set;}
        public string DetailNamaBarang { get; set; }
        public string DetailQuantity { get; set; }
        public string DetailHarga { get; set; }

    }
}