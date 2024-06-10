using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class Ogretmenler
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Adı")]
        [Required]
        public string? Adi { get; set; }
        [DisplayName("Soyadı")]
        [Required]
        public string? Soyadi { get; set; }
        [DisplayName("TelNo")]
        public string? TelNo { get; set; }
        [DisplayName("Fotograf")]
        public string? Fotograf { get; set; }
        [NotMapped]
        [DisplayName("Upload Image File")]
        public IFormFile? ImageFile { get; set; }
        [DisplayName("Ders")]
        public Ders DersAd { get; set; }
        [DisplayName("Ders Adı")]
        [Required]
        public int DersId { get; set; }
    }
}