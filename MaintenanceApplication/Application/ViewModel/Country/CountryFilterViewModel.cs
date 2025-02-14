using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class CountryFilterViewModel : PaginationBasicFilter
    {
        public string? Name { get; set; }
    }
}
