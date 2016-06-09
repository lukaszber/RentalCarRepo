using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarHire.Domain.Entities;
namespace CarHire.WebUI.Models
{
    public class CarsListMainModel
    {
        public CarsListViewModel CarListViewModel { get; set; }
        public CarSearch CarSearch {get; set;}
    }
}