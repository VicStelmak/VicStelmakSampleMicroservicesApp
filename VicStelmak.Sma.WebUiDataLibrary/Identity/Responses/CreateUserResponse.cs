namespace VicStelmak.Sma.WebUiDataLibrary.Identity.Responses
{
    public record CreateUserResponse(bool ActionIsSuccessful, IEnumerable<string> Errors);
}
