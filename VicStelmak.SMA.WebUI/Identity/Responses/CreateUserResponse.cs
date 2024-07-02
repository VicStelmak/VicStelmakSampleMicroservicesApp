namespace VicStelmak.Sma.WebUi.Identity.Responses
{
    public record CreateUserResponse(bool ActionIsSuccessful, IEnumerable<string> Errors);
}
