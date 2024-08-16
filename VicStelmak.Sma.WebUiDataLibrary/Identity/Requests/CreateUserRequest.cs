namespace VicStelmak.Sma.WebUiDataLibrary.Identity.Requests
{
    public record CreateUserRequest(
        string Email,
        string FirstName,
        string LastName,
        string? Password,
        string? PasswordForConfirmation);
}
