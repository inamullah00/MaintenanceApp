using Maintenance.Application.Wrapper;

namespace Maintenance.Application.ViewModel
{
    public class ContactUsIndexAndFilterViewModel
    {
        public ContactUsFilterViewModel Filter { get; set; }
        public PaginatedResponse<ContactUsResponseViewModel> Result { get; set; }

    }
}
