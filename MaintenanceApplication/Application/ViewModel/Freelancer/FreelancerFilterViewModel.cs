using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class FreelancerFilterViewModel : PaginationBasicFilter
    {
        public string? FullName { get; set; }
        public AccountStatus? AccountStatus { get; set; }
    }
}
