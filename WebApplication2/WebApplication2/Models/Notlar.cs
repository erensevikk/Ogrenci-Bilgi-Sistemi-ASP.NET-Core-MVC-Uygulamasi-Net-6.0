using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Notlar
    {
        [Key]
        public int Id { get; set; }
        public Ogrenciler Ogrenci { get; set; }
        [DisplayName("Öğrenci Ad")]
        [Required]
        public int? OgrenciId { get; set; }
        [DisplayName("Ders Adı")]
        [Required]
        public Ders DersAd { get; set; }
        [Required]
        public int? DersId { get; set; }

        [DisplayName("Not")]
        [Required]
        public int? NotDegeri { get; set; }

        [DisplayName("Sonuç")]
        public bool? sonuc { get; set; }
    }
}
