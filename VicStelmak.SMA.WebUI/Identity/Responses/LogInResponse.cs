namespace VicStelmak.Sma.WebUi.Identity.Responses
{
    public record LogInResponse(string ErrorMessage, string Jwt, bool IsAuthenticationSuccessful);
}
