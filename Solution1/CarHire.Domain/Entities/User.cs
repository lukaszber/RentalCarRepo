using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarHire.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue=false)]
        public int UserID { get; set; }
        [Required(ErrorMessage="Prosze podać imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Prosze podać nazwisko")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Prosze podać imię")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Podaj Hasło", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Prosze podać mail")]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage ="Prosze podać Prawidłowy Mail")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Prosze podać adres zamieszkania")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Prosze podać numer PESEL")]
        [RegularExpression("[0-9]{11,11}", ErrorMessage = "Prosze podać Prawidłowy Pesel")]
        public string Pesel { get; set; }
        [HiddenInput]
        //[Required(ErrorMessage = "Prosze podać Categorie")]
        public string Category { get; set; }
            
        public bool Driver { get; set; }
    }
}
