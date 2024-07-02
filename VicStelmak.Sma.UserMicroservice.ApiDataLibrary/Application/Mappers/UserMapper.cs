using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Mappers
{
    internal static class UserMapper
    {
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
