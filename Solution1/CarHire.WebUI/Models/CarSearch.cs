using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarHire.WebUI.Models
{
    public class CarSearch
    {
        public string NameSearch { get; set; }
        public string BrandSearch { get; set; }
        public decimal MinMileage { get; set; }
        public decimal MaxMileage { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Hired { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public decimal MinCapacity { get; set; }
        public decimal MaxCapacity { get; set; }
        public string Category { get; set; }
    }
}