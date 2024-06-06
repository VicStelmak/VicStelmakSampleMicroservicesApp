using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
        Task DeleteUserAsync(string id);
        List<GetUserResponse> GetAllUsers();
        Task<GetUserResponse> GetUserByIdAsync(string id);
        Task<List<string>> GetUserRolesAsync(string id);
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task UpdateUserAsync(string id, UpdateUserRequest request);
    }
}