using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Task<AddRoleToUserResponse> AddRoleToUserAsync(string roleName, string userId);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task<DeleteRolesFromUserResponse> DeleteRolesFromUserAsync(string id, IEnumerable<string> roles);
        Task DeleteUserAsync(string id);
        Task<List<GetUserResponse>> GetAllUsersAsync();
        Task<GetUserResponse> GetUserByIdAsync(string id);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task UpdateUserAsync(string id, UpdateUserRequest request);
    }
}