using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class PackageFilterViewModel : PaginationBasicFilter
    {
        public string? Name { get; set; }
        public string? FreelancerId { get; set; }
    }
}
