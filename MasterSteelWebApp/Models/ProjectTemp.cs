using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class ProjectTemp
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Nomor { get; set; }
        public string Name { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public string ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string CompanyId { get; set; }
        public string RangeDateStart { get; set; }
        public string RangeDateEnd { get; set; }
        public float RangeQualityStart { get; set; }
        public float RangeQualityEnd { get; set; }
        public float RangeNominalStart { get; set; }
        public float RangeNominalEnd { get; set; }
        public string Note { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string ImageFile { get; set; }
        public string ImageFile2 { get; set; }
        public string ImageFile3 { get; set; }
        public bool Active { get; set; }
        public string UserCreateId { get; set; }
        public string CreatedAt { get; set; }
        public string UserUpdateId { get; set; }
        public string UpdateAt { get; set; }
    }
}