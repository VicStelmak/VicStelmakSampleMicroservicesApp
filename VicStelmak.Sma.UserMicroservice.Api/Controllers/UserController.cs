using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Enums;

namespace VicStelmak.Sma.UserMicroservice.Api.Controllers
{
    [Authorize(Roles = nameof(Role.Administrator))]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("roles/{userId}")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] string roleName, string userId)
        {
            try
            {
                var roleAddingResult = await _userService.AddRoleToUserAsync(roleName, userId);

                if (roleAddingResult.RoleAddedSuccessfully != true && roleAddingResult.UserIsAlreadyInRole != true) return NotFound(roleAddingResult);
                else if (roleAddingResult.UserIsAlreadyInRole == true) return Conflict(roleAddingResult);
                    
                return Ok(roleAddingResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }

        [HttpDelete("roles/{userId}")]
        public async Task<IActionResult> DeleteRolesFromUserAsync(string userId, [FromBody] IEnumerable<string> roles)
        {
            try
            {
                var rolesDeletingResult = await _userService.DeleteRolesFromUserAsync(userId, roles);

                if (rolesDeletingResult.RolesDeletedSuccessfully != true)
                {
                    return NotFound(rolesDeletingResult);
                }

                return Ok(rolesDeletingResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            try
            {
                var userToDelete = await _userService.GetUserByIdAsync(userId);

                if (userToDelete == null) return NotFound($"User with the Id {userId} not found.");
                
                await _userService.DeleteUserAsync(userId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deleting of data in database failed.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            var userCreatingResult = await _userService.CreateUserAsync(request);

            if (userCreatingResult.ActionIsSuccessful == false) 
            {
                return BadRequest(userCreatingResult);
            }

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }
        
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);

                if (user == null) return NotFound();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

        [AllowAnonymous]
        [HttpPost("logins")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInRequest request)
        {
            var logInResult = await _userService.LogInAsync(request);

            if (logInResult.IsAuthenticationSuccessful == false)
            {
                return Unauthorized(logInResult);
            }

            return Ok(logInResult);
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUserAsync(string userId, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var userToUpdate = await _userService.GetUserByIdAsync(userId);

                if (userToUpdate == null) return NotFound($"User with the Id {userId} not found.");

                await _userService.UpdateUserAsync(userId, request);

                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Updating of data in database failed.");
            }
        }
    }
}
