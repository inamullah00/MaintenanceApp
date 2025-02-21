using Domain.Enums;
using Maintenance.Application.Common.Filters;

namespace Maintenance.Application.ViewModel
{
    public class ContactUsFilterViewModel : PaginationBasicFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public ContactUsStatusEnum? Status { get; set; }
    }
}
