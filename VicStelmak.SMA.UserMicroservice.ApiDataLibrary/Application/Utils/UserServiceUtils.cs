using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Utils
{
    internal static class UserServiceUtils
    {
        internal static SigningCredentials GetSigningCredentials(IConfigurationSection jwtSettings)
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);

            return new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);
        }

        internal static JwtSecurityToken CreateJwtOptions(List<Claim> claims, IConfigurationSection jwtSettings, SigningCredentials signingCredentials)
        {
            var jwtOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return jwtOptions;
        }
    }
}
