using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Extensions;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Mappers;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Utils;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IConfigurationSection _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly UserManager<UserModel> _userManager;

        public UserService(IConfiguration configuration, ILogger<UserService> logger, RoleManager<IdentityRole> roleManager, 
            UserManager<UserModel> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
         
        public async Task<AddRoleToUserResponse> AddRoleToUserAsync(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) 
            {
                _logger.LogWarning(
                    "Role { roleName } can't be added to user with Id { userId } because he or she was not found { date } at { time } Utc.", 
                    roleName, userId, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

                return new AddRoleToUserResponse("User not found.", false, false);
            }

            if (await _roleManager.RoleExistsAsync(roleName) == true)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) != true)
                {
                    await _userManager.AddToRoleAsync(user, roleName);

                    _logger.LogInformation("Role { roleName } was added { date } to user { userName } at { time } Utc.",
                        roleName, DateTime.UtcNow.ToShortDateString(), user.UserName, DateTime.UtcNow.ToLongTimeString());

                    return new AddRoleToUserResponse($"Added {roleName} role to user {user.UserName}.",
                        true, false);
                }
                else 
                {
                    return new AddRoleToUserResponse($"User {user.UserName} already have {roleName} role.",
                        false, true);
                }
            } 
            else
            {
                return new AddRoleToUserResponse($"Role {roleName} does not exist.",
                    false, false);
            }
        }

        public async Task<bool> CheckIfUserExistsByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null) return true;
            else return false;
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

            _logger.LogInformation("User { userName } was created { date } at { time } Utc.", 
                user.Email, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

            return new CreateUserResponse(true, null);
        }

        public async Task<DeleteRolesFromUserResponse> DeleteRolesFromUserAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Roles couldn't be deleted from user with Id { userId } because he or she was not found { date } at { time } Utc.",
                    userId, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

                return new DeleteRolesFromUserResponse("User not found.", false);
            }

            await _userManager.RemoveFromRolesAsync(user, roles);

            foreach (var role in roles) 
            {
                _logger.LogInformation("Role { roleName } was removed { date } from user { userName } at { time } Utc.",
                    role, DateTime.UtcNow.ToShortDateString(), user.Email, DateTime.UtcNow.ToLongTimeString());
            }

            return new DeleteRolesFromUserResponse($"Roles were removed successfully from user {user.Email}.", true);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);

            if (userToDelete != null)
            {
                await _userManager.DeleteAsync(userToDelete);

                _logger.LogInformation("User { userName } was deleted { date } at { time } Utc.",
                   userToDelete.Email, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
            }
        }

        public async Task<List<GetUserResponse>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();

            List<GetUserResponse> responses = new();

            foreach (var user in users)
            {
                var userRoles = (List<string>)await _userManager.GetRolesAsync(user);

                var userData = new GetUserResponse(user.Id, user.Email, user.FirstName, user.LastName, userRoles);

                responses.Add(userData);
            }

            return responses;
        }

        public async Task<GetUserResponse> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) return null;

            var userRoles = (List<string>)await _userManager.GetRolesAsync(user);

            return new GetUserResponse(user.Id, user.Email, user.FirstName, user.LastName, userRoles);
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
                return new LogInResponse("Incorrect password.", false, null);
            }

            var signingCredentials = UserServiceUtils.GetSigningCredentials(_jwtSettings);
            var claims = await user.GetClaimsAsync(_userManager);
            var jwtOptions = UserServiceUtils.CreateJwtOptions(claims, _jwtSettings, signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtOptions);

            _logger.LogInformation("User { userName } successfully logged in { date } at { time } Utc.", 
                request.Email, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

            return new LogInResponse(null, true, jwt);
        }

        public async Task UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            ArgumentNullException.ThrowIfNull(userId);
            ArgumentNullException.ThrowIfNull(request);

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var originalUserName = user.UserName;

                user.Email = request.Email;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.UserName = request.Email;

                var userUpdatingResult = await _userManager.UpdateAsync(user);

                _logger.LogInformation("User { userName } was updated { date } at { time } Utc.",
                    originalUserName, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
            }
        }
    }
}
