using Microsoft.AspNetCore.Components.Authorization;
using VicStelmak.Sma.WebUi.Identity.Responses;

namespace VicStelmak.Sma.WebUi.ViewModels
{
    public class UsersManagementViewModel
    {
        public int SequenceNumber { get; set; } = new();

        public List<GetUserResponse> Users { get; set; } = new List<GetUserResponse>();
    }
}
