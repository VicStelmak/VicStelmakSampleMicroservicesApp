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
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
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
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }

        [AllowAnonymous]
        [HttpGet("email")]
        public async Task<ActionResult<bool>> CheckIfUserExistsByEmailAsync([FromQuery] string email)
        {
            try
            {
                var userExists = await _userService.CheckIfUserExistsByEmailAsync(email);

                if (userExists != true) return false;

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            try
            {
                var userCreatingResult = await _userService.CreateUserAsync(request);

                if (userCreatingResult.ActionIsSuccessful == false)
                {
                    _logger.LogError("User creating failed because of the following error (or errors): ");

                    foreach (var error in userCreatingResult.Errors)
                    {
                        _logger.LogError("{error}", error);
                    }

                    return BadRequest(userCreatingResult);
                }

                return StatusCode(201);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

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
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            try
            {
                var userToDelete = await _userService.GetUserByIdAsync(userId);

                if (userToDelete == null) return NotFound($"User with the Id {userId} not found.");
                
                await _userService.DeleteUserAsync(userId);

                return Ok();
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException)
                {
                    _logger.LogError(exception.ToString());
                }
                else
                {
                    _logger.LogCritical(exception.ToString());
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Deleting of data in database failed.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

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
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

        [AllowAnonymous]
        [HttpPost("logins")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInRequest request)
        {
            try
            {
                var logInResult = await _userService.LogInAsync(request);

                if (logInResult.IsAuthenticationSuccessful == false)
                {
                    _logger.LogWarning("{ userName } login attempt failed { date } at { time } because of the following reason: {error}", 
                        request.Email, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString(), logInResult.ErrorMessage);

                    return Unauthorized(logInResult);
                }

                return Ok(logInResult);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(string userId, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var userToUpdate = await _userService.GetUserByIdAsync(userId);

                if (userToUpdate == null) return NotFound($"User with the Id {userId} not found.");

                await _userService.UpdateUserAsync(userId, request);

                return StatusCode(204);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, "Connection to database failed.");
            }
        }
    }
}
