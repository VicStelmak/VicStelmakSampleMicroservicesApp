using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.UserMicroservice.Tests
{
    internal static class UserMapper
    {
        internal static List<GetUserResponse> MapToListOfGetUserResponses(this List<UserModel> userModels)
        {
            var responses = new List<GetUserResponse>();

            foreach (var user in userModels) 
            {
                responses.Add(new GetUserResponse(user.Id, user.Email, user.FirstName, user.LastName, null));
            }
            
            return responses;
        }
    }
}
