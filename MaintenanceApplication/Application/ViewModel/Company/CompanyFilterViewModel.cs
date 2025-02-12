using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class CompanyFilterViewModel : PaginationBasicFilter
    {
        public string? FullName { get; set; }
        public AccountStatus? AccountStatus { get; set; }
    }
}
