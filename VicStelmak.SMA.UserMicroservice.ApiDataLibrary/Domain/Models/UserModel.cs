using Microsoft.AspNetCore.Identity;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
