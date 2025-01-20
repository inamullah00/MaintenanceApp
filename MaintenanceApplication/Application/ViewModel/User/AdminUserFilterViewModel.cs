

namespace Maintenance.Application.ViewModel
{
    public class AdminUserFilterViewModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}
