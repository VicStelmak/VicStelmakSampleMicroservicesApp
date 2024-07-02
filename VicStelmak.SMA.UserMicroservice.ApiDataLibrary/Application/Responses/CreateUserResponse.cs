namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Responses
{
    public record CreateUserResponse(bool ActionIsSuccessful, IEnumerable<string> Errors);
}
