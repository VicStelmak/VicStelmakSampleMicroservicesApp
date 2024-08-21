using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Task<AddRoleToUserResponse> AddRoleToUserAsync(string roleName, string userId);
        Task<bool> CheckIfUserExistsByEmailAsync(string email);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task<DeleteRolesFromUserResponse> DeleteRolesFromUserAsync(string id, IEnumerable<string> roles);
        Task DeleteUserAsync(string id);
        Task<List<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> GetUserByIdAsync(string id);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task UpdateUserAsync(string id, UpdateUserRequest request);
    }
}