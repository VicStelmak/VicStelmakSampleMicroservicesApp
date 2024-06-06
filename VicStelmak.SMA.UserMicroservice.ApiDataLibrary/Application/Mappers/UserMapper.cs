using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Mappers
{
    internal static class UserMapper
    {
        internal static GetUserResponse MapToGetUserResponse(this UserModel user)
        {
            return new GetUserResponse(user.Id, user.Email, user.FirstName, user.LastName);
        }

        internal static UserModel MapToUserModel(this CreateUserRequest request)
        {
            return new UserModel
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email
            };
        }
    }
}
