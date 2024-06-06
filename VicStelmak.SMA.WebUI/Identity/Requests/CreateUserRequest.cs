namespace VicStelmak.SMA.WebUI.Identity.Requests
{
    public record CreateUserRequest(
     string Email,
     string FirstName,
     string LastName,
     string Password,
     string PasswordForConfirmation);
}
