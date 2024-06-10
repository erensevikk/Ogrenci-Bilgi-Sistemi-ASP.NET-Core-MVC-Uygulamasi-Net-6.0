using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Belgeler
    {
        public int Id { get; set; }
        public Ogrenciler Ogrenci { get; set; }
        [DisplayName("Öğrenci Adı")]
        [Required]
        public int OgrenciId { get; set; }
        [DisplayName("Teşekkür Belgesi")]
        public bool tskbelge { get; set; }
        [DisplayName("Takdir Belgesi")]
        public bool takdirbelge { get; set; }
        [DisplayName("Onur Belgesi")]
        public bool onurbelge { get; set; }
        [DisplayName("Başarı Belgesi")]
        public bool basaribelge { get; set; }
        
    }
}
