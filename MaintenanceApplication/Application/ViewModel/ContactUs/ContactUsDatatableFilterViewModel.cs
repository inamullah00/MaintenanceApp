using Domain.Enums;

namespace Maintenance.Application.ViewModel
{
    public class ContactUsDatatableFilterViewModel
    {
        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public ContactUsStatusEnum? Status { get; set; }
    }
}
