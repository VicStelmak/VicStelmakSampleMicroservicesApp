using VicStelmak.SMA.WebUI.Identity.Requests;
using VicStelmak.SMA.WebUI.Identity.Responses;

namespace VicStelmak.SMA.WebUI.Identity
{
    public interface IIdentityService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task DeleteUserAsync(string email);
        Task<List<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> GetUserByIdAsync(string id);
        Task<List<string>> GetUserRolesAsync(string id);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task LogoutAsync();
        Task UpdateUserAsync(string id, UpdateUserRequest request);
    }
}