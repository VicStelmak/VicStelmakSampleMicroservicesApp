namespace VicStelmak.SMA.WebUI.Identity.Responses
{
    public record LogInResponse(string ErrorMessage, string Jwt, bool IsAuthenticationSuccessful);
}
