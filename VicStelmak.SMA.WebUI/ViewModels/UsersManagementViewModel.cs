using Microsoft.AspNetCore.Components.Authorization;
using VicStelmak.SMA.WebUI.Identity.Responses;

namespace VicStelmak.SMA.WebUI.ViewModels
{
    public class UsersManagementViewModel
    {
        public int SequenceNumber { get; set; } = new();

        public List<GetUserResponse> Users { get; set; } = new List<GetUserResponse>();
    }
}
