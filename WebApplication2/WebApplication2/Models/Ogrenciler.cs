using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Ogrenciler
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad kısmı gereklidir.")]
        [DisplayName("Adı")]
        public string? Adi { get; set; }

        [Required(ErrorMessage = "Soyad kısmı gereklidir.")]
        [DisplayName("Soyadı")]
        public string? Soyadi { get; set; }

        [Required(ErrorMessage = "Veli TelNo kısmı gereklidir.")]
        [DisplayName("Veli TelNo")]
        public string? VeliTelNo { get; set; }

        [DisplayName("Fotograf")]
        public string? Fotograf { get; set; }
        [NotMapped]
        [DisplayName("Upload Image File")]
        public IFormFile? ImageFile { get; set; }
        [DisplayName("Öğrenci Sınıfı")]
        [Required]
        public int SınıfId { get; set; }
        public SINIFLAR? Sınıf { get; set; }
    }
}