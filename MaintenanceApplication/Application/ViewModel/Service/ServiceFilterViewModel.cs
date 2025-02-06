using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class ServiceFilterViewModel : PaginationBasicFilter
    {
        public string? Name { get; set; }
    }
}
