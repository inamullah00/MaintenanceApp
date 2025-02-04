namespace Maintenance.Application.ViewModel.User
{
    public class UserFilterViewModel
    {
        public string? UserId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }


}
