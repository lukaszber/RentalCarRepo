using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarHire.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalCars { get; set; }
        public int CarsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalCars / CarsPerPage); } }
    }
}