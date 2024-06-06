namespace VicStelmak.SMA.WebUI.Identity.Responses
{
    public record CreateUserResponse(bool ActionIsSuccessful, IEnumerable<string> Errors);
}
