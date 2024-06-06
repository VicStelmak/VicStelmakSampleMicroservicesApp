namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses
{
    public record CreateUserResponse(bool ActionIsSuccessful, IEnumerable<string> Errors);
}
