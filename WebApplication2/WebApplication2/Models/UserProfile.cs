using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        public bool IsActive { get; set; }
    }
}
