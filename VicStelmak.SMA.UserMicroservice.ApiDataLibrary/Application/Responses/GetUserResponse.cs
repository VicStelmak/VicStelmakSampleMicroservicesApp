namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses
{
    public record GetUserResponse(string Id, string Email, string FirstName, string LastName, List<string> Roles);
}
