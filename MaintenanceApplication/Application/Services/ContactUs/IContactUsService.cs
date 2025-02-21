
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.ContactUs
{
    public interface IContactUsService
    {
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetAllListAsync(ContactUsFilterViewModel filter);
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetPagedListAsync(ContactUsFilterViewModel filter);
        Task<int> MarkAsRead(Guid id, CancellationToken cancellationToken);
        Task<int> NotificationCountAsync();
    }
}
