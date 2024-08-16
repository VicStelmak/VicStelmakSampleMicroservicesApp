namespace VicStelmak.Sma.WebUiDataLibrary.Identity.Responses
{
    public record LogInResponse(string ErrorMessage, string Jwt, bool IsAuthenticationSuccessful);
}
