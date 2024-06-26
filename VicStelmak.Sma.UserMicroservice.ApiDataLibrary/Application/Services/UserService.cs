﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfigurationSection _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly UserManager<UserModel> _userManager;

        public UserService(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
         
        public async Task<AddRoleToUserResponse> AddRoleToUserAsync(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return new AddRoleToUserResponse("User not found.", false, false);
            
            if (await _roleManager.RoleExistsAsync(roleName) == true) 
            {
                if (await _userManager.IsInRoleAsync(user, roleName) != true)
                {
                    await _userManager.AddToRoleAsync(user, roleName);

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

        public async Task<DeleteRolesFromUserResponse> DeleteRolesFromUserAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) 
            {
                return new DeleteRolesFromUserResponse("User not found.", false); 
            }

            await _userManager.RemoveFromRolesAsync(user, roles);

            return new DeleteRolesFromUserResponse($"Roles were removed successfully from user {user.Email}.", true);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);

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
            var userRoles = (List<string>)await _userManager.GetRolesAsync(user);

            if (user == null) return null;

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
                return new LogInResponse("Incorrect login or password.", false, null);
            }

            var signingCredentials = UserServiceUtils.GetSigningCredentials(_jwtSettings);
            var claims = await user.GetClaimsAsync(_userManager);
            var jwtOptions = UserServiceUtils.CreateJwtOptions(claims, _jwtSettings, signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtOptions);

            return new LogInResponse(null, true, jwt);
        }

        public async Task UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

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
