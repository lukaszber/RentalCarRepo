using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarHire.Domain.Entities
{
    public class Car
    {
        [HiddenInput(DisplayValue = false)]
        public int CarID { get; set; }
        [Required(ErrorMessage = "Prosze podać marke auta")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Prosze podać model auta")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Prosze podać przebieg auta")]
        public decimal Mileage { get; set; }
        [Required(ErrorMessage = "Prosze podać numer rejestracyjny auta")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Prosze podać cene wypożyczenia auta")]
        public decimal PricePerDay { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool Hired { get; set; }
        [Required(ErrorMessage = "Prosze podać rok produkcji auta")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Prosze podać pojemność auta")]
        public decimal Capacity { get; set; }
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }
}
