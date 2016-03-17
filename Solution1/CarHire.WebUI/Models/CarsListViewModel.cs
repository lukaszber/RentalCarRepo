using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarHire.Domain.Entities;
namespace CarHire.WebUI.Models
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}