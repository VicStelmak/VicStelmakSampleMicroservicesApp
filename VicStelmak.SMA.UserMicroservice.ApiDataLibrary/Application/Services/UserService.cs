using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Extensions;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Mappers;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Utils;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public UserService(UserManager<UserModel> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        public async Task DeleteUserAsync(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete != null)
            {
                await _userManager.DeleteAsync(userToDelete);
            }
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var user = request.MapToUserModel();

            var userCreatingResult = await _userManager.CreateAsync(user, request.Password);

            if (userCreatingResult.Succeeded == false)
            {
                var errors = userCreatingResult.Errors.Select(error => error.Description);

                return new CreateUserResponse(false, errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return new CreateUserResponse(true, null);
        }

        public List<GetUserResponse> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            return users.Select(user => user.MapToGetUserResponse()).ToList();
        }

        public async Task<GetUserResponse> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return null;

            return user.MapToGetUserResponse();
        }

        public async Task<List<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null) return null;

            return roles.ToList();
        }

        public async Task<LogInResponse> LogInAsync(LogInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
            {
                return new LogInResponse("User not found.", false, null);
            }
            if (await _userManager.CheckPasswordAsync(user, request.Password) == false)
            {
                return new LogInResponse("Incorrect login or password.", false, null);
            }

            var signingCredentials = UserServiceUtils.GetSigningCredentials(_jwtSettings);
            var claims = await user.GetClaimsAsync(_userManager);
            var jwtOptions = UserServiceUtils.CreateJwtOptions(claims, _jwtSettings, signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtOptions);

            return new LogInResponse(null, true, jwt);
        }

        public async Task UpdateUserAsync(string id, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = request.Email;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.UserName = request.Email;

                var userUpdatingResult = await _userManager.UpdateAsync(user);
            }
        }
    }
}
