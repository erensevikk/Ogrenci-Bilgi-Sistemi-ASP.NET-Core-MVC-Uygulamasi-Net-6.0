using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Dersler
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Ders Adı")]
        [Required]
        public int DersId { get; set; }

        public Ders? Ders { get; set; }

        [DisplayName("Ders Saati")]
        [Required]
        public TimeSpan Saat { get; set; }

        [DisplayName("Ögretmen")]
        [Required]
        public int OgretmenId { get; set; }
        [DisplayName("Ögretmen")]
        [Required]
        public Ogretmenler? Ogretmen { get; set; }
    }
}
