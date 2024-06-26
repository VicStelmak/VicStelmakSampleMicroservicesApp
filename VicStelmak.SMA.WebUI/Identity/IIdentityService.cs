using VicStelmak.SMA.WebUI.Identity.Requests;
using VicStelmak.SMA.WebUI.Identity.Responses;

namespace VicStelmak.SMA.WebUI.Identity
{
    public interface IIdentityService
    {
        Task AddRoleToUserAsync(string roleName, string userId);
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