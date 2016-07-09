using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class TempProject
    {
        public string ProjectCategoryID {get;set;}
        public string ProjectID {get;set;}
        public string ProjectName {get;set;}
        public string ProjectAddress {get;set;}
        public string ProvinsiID {get;set;}
        public string CityID {get;set;}
        public float BuildingArea {get;set;}
        public float Quantity {get;set;}
        public float Nominal {get;set;}
        public string DeveloperID {get;set;}
        public string ProjectManager {get;set;}
        public string KontraktorID {get;set;}
        public string ConsultantID {get;set;}
        public string ManagementKonstruksiID {get;set;}
        public string ProjectPeriodStart {get;set;}
        public string ProjectPeriodEnd {get;set;}
        public string SupplierID {get;set;}
        public string ContractNumber {get;set;}
        public string ContractID {get;set;}
        public int Status {get;set;}
        public string Note {get;set;}
        public string SalesID {get;set;}
        public string PICID {get;set;}
        public float Latitude {get;set;}
        public float Longitude {get;set;}
        public string Images {get;set;}
        public string UserCreateID {get;set;}
        public string CreatedAt {get;set;}
        public string UserUpdateID {get;set;}
        public string UpdateAt { get; set; }
    }
}