using VicStelmak.SMA.WebUI.Identity.Requests;
using VicStelmak.SMA.WebUI.Identity.Responses;

namespace VicStelmak.SMA.WebUI.Identity
{
    public interface IIdentityService
    {
        Task AddRoleToUserAsync(string roleName, string UserId);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task DeleteRolesFromUserAsync(string UserId, IEnumerable<string> roles);
        Task DeleteUserAsync(string email);
        Task<List<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> GetUserByIdAsync(string UserId);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task LogoutAsync();
        Task UpdateUserAsync(string UserId, UpdateUserRequest request);
    }
}