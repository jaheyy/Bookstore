using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksiegarnia.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage ="Tytuł jest wymagany")]
        [StringLength(30, ErrorMessage = "Tytuł może zawierać do 30 znaków")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Nazwa autora jest wymagana")]
        [StringLength(30, ErrorMessage = "Nazwa autora może zawierać do 30 znaków")]
        public string Author { get; set; }

        [Display(Name = "Wydawnictwo")]
        [Required(ErrorMessage = "Nazwa wydawnictwa jest wymagana")]
        [StringLength(30, ErrorMessage = "Nazwa wydawnictwa może zawierać do 30 znaków")]
        public string Publisher { get; set; }

        [Display(Name = "Cena (PLN)")]
        [Required(ErrorMessage = "Ustal cenę książki")]
        [Range(0.01, 200.00, ErrorMessage = "Ustal cenę od 0,01 do 200,00 PLN")]
        [Column (TypeName ="decimal(5,2)")]
        public decimal? Price { get; set; }

        [Display(Name = "Data Wydania")]
        [Required(ErrorMessage = "Wybierz datę wydania")]
        [DataType(DataType.Date,ErrorMessage = "Podaj datę wydania")]
        public DateTime? ReleaseDate { get; set; }
    }
}
