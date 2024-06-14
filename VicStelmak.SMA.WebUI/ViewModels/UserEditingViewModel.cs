using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VicStelmak.SMA.WebUI.Identity.Responses;
using VicStelmak.SMA.WebUI.Identity.Enums;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class UserEditingViewModel
    {
        public UserEditingViewModel(GetUserResponse user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserRoles = user.Roles;
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

        internal List<string> Roles { get; set; } = new() { Role.Administrator.ToString(), Role.Customer.ToString(), Role.User.ToString() };

        internal List<string> RolesSelected { get; set; } = new();

        internal List<string> RolesToDelete { get; set; } = new();

        internal List<string> UserRoles { get; set; } = new();
    }
}
