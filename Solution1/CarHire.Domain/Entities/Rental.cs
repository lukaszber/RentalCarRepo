using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarHire.Domain.Entities
{
    public class Rental
    {
        [HiddenInput(DisplayValue = false)]
        public int RentalId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int CarId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Podaj date wypożyczenia")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime RentalDate { get; set; }
        [Required(ErrorMessage = "Podaj date zwrotu")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime ReturnDate { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime Extension { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }

    }
}
