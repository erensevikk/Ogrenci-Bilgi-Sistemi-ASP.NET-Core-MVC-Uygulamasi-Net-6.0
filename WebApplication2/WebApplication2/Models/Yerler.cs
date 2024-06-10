using System.ComponentModel;

namespace WebApplication2.Models
{
    public class Yerler
    {
        public int Id { get; set; }
        [DisplayName("Yer")]
        public string mekan { get; set; }
    }
}
