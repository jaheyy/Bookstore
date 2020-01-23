using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Podaj swoje imię")]
        [StringLength(15, ErrorMessage = "Imię może zawierać do 15 znaków")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Podaj swoje nazwisko")]
        [StringLength(20, ErrorMessage = "Nazwisko może zawierać do 20 znaków")]
        public string LastName { get; set; }

        [Display(Name = "Numer domu")]
        [Required(ErrorMessage = "Podaj numer domu")]
        [StringLength(10, ErrorMessage = "Numer domu może zawierać do 10 znaków")]
        public string Address { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Podaj ulicę")]
        [StringLength(35, ErrorMessage = "Ulica może zawierać do 35 znaków")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Podaj miasto")]
        [StringLength(20, ErrorMessage = "Miasto może zawierać do 20 znaków")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Podaj kod pocztowy")]
        [StringLength(20, ErrorMessage = "Imię może zawierać do 20 znaków")]
        public string ZIPcode { get; set; }

        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Podaj adres email")]
        [StringLength(50, ErrorMessage = "Email może zawierać do 50 znaków")]
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [Range(100000000,999999999, ErrorMessage = "Nieprawidłowy numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Data wykonania")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Wysłane")]
        public bool Done { get; set; }

        [Display(Name = "Id zamówionych książek")]
        public string IdOfOrderedBooks  { get; set; }

        [Display(Name = "Cena końcowa")]
        public double Amount { get; set; }
    }
}
