namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses
{
    public record LogInResponse(string ErrorMessage, bool IsAuthenticationSuccessful, string Jwt);
}
