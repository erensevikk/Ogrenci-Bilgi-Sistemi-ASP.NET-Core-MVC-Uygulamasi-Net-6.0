using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Duyurular
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Öğretmen Adı")]
        [Required]
        public int OgretmenId { get; set; }

        public Ogretmenler Ogretmen { get; set; }

        [DisplayName("Duyuru")]
        [Required]
        public string Duyuru { get; set; }

        [DisplayName("Tarih")]
        [Required]
        public DateTime Tarih { get; set; }

        [DisplayName("Yer")]
        [Required]
        public int YerId { get; set; }

        public Yerler Yer { get; set; }
    }
}

