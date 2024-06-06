using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Extensions
{
    internal static class UserExtensions
    {
        internal static async Task<List<Claim>> GetClaimsAsync(this UserModel user, UserManager<UserModel> userManager)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);
            
            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
