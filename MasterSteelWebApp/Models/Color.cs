using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterSteelWebApp.Models
{
    public class Color
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string HeaderColor { get; set; }
        public string BodyColor { get; set; }
        public string MenuColor { get; set; }
        public string FooterColor { get; set; }
        public string TextColor { get; set; }
        public string HoverColor { get; set; }
        public string UserCreatedAt { get; set; }
        public string UpdateAt { get; set; }
    }
}