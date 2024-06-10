using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Ders
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ders Adı")]
        [Required]
        public string DersAdi { get; set; }
    }
}
