using VicStelmak.Sma.WebUiDataLibrary.Identity.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Identity.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Identity
{
    public interface IIdentityService
    {
        Task AddRoleToUserAsync(string roleName, string userId);
        Task<bool> CheckIfUserExistsByEmailAsync(string email);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task DeleteRolesFromUserAsync(string userId, IEnumerable<string> roles);
        Task DeleteUserAsync(string email);
        Task<List<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> GetUserByIdAsync(string userId);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task LogoutAsync();
        Task UpdateUserAsync(string userId, UpdateUserRequest request);
    }
}