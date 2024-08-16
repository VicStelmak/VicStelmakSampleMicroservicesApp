using VicStelmak.Sma.WebUiDataLibrary.Identity.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.ViewModels
{
    public class UsersManagementViewModel
    {
        public int SequenceNumber { get; set; } = new();

        public List<GetUserResponse> Users { get; set; } = new List<GetUserResponse>();
    }
}
