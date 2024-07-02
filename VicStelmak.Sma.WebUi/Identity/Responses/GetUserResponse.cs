namespace VicStelmak.Sma.WebUi.Identity.Responses
{
    public record GetUserResponse(string Id, string Email, string FirstName, string LastName, List<string> Roles);
}
