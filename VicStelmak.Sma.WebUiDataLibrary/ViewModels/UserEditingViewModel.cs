using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VicStelmak.Sma.WebUiDataLibrary.Identity.Responses;
using VicStelmak.Sma.WebUiDataLibrary.Identity.Enums;

namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
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

        public List<string> Roles { get; set; } = new() { Role.Administrator.ToString(), Role.Customer.ToString(), Role.User.ToString() };

        public List<string> RolesSelected { get; set; } = new();

        public List<string> RolesToDelete { get; set; } = new();

        public List<string> UserRoles { get; set; } = new();
    }
}
