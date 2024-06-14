namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Application.Responses
{
    public record AddRoleToUserResponse(string Message, bool RoleAddedSuccessfully, bool UserIsAlreadyInRole);
}
