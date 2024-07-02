namespace VicStelmak.Sma.WebUi.Identity.Requests
{
    public record CreateUserRequest(
     string Email,
     string FirstName,
     string LastName,
     string Password,
     string PasswordForConfirmation);
}
