using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VicStelmak.SMA.WebUI.Identity.Responses;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class UserEditingViewModel
    {
        public UserEditingViewModel(GetUserResponse user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        [DisplayName("Email Address")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
    }
}
