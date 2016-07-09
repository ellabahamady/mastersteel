using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class SearchProject
    {
        public string Token { get; set; }
        public string ProjectCategoryID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public string ProvinsiID { get; set; }
        public string CityID { get; set; }
        public float BuildingAreaFrom { get; set; }
        public float BuildingAreaTo { get; set; }
        public float QuantityFrom { get; set; }
        public float QuantityTo { get; set; }
        public float NominalFrom { get; set; }
        public float NominalTo { get; set; }
        public string DeveloperID { get; set; }
        public string ProjectManager { get; set; }
        public string KontraktorID { get; set; }
        public string ConsultantID { get; set; }
        public string ManagementKonstruksiID { get; set; }
        public string ProjectPeriodStartFrom { get; set; }
        public string ProjectPeriodStartTo { get; set; }
        public string ProjectPeriodEndFrom { get; set; }
        public string ProjectPeriodEndTo { get; set; }
        public string SupplierID { get; set; }
        public string ContractNumber { get; set; }
        public string StatusProject { get; set; }
        public string Note { get; set; }
        public string SalesID { get; set; }
        public string PICID { get; set; }
    }
}