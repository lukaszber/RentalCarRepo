using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarHire.Domain.Entities;
namespace CarHire.WebUI.Models
{
    public class RentalListViewModel
    {
        public IEnumerable<RentalViewAggregator> Rentals { get; set; }
    }

    public class RentalViewAggregator
    {
        public Rental Rental { get; set; }
        public Car Car { get; set; }
        public User User { set; get; }
    }
}