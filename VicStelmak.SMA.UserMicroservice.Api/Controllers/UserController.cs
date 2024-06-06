using Microsoft.AspNetCore.Mvc;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Requests;

namespace VicStelmak.SMA.UserMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                var userToDelete = await _userService.GetUserByIdAsync(id);

                if (userToDelete == null) return NotFound($"User with the Id {id} not found.");
                
                await _userService.DeleteUserAsync(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deleting of data in database failed.");
            }
        }

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
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null) return NotFound();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetUserRolesAsync([FromQuery] string id)
        {
            try
            {
                var roles = await _userService.GetUserRolesAsync(id);

                if (roles == null) return NotFound();

                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Retrieving of data from the database failed.");
            }
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var userToUpdate = await _userService.GetUserByIdAsync(id);

                if (userToUpdate == null) return NotFound($"User with the Id {id} not found.");

                await _userService.UpdateUserAsync(id, request);

                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Updating of data in database failed.");
            }
        }
    }
}
