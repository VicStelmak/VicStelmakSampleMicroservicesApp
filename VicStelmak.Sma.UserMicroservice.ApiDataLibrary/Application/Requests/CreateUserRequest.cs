namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests
{
    public record CreateUserRequest(
        string Email, 
        string FirstName,
        string LastName,
        string Password, 
        string PasswordForConfirmation);
}
