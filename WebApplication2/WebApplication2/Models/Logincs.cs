using System.ComponentModel;

namespace WebApplication2.Models
{
    public class Logincs
    {
        [DisplayName("Username")]
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool LoggedStatus { get; set; }
    }
        
}
