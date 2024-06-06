using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Email Address")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
