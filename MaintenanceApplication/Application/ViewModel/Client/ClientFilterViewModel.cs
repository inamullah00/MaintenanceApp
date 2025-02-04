using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class ClientFilterViewModel : PaginationBasicFilter
    {
        public string? FullName { get; set; }
    }
}
