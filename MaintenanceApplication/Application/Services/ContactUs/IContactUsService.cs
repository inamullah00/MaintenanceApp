
using Maintenance.Application.Dto_s.SettingDtos;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.ContactUs
{
    public interface IContactUsService
    {
        Task<Result<ContactUsResponseModel>> CreateContactUsAsync(ContactUsRequestModel model, CancellationToken cancellationToken);
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetAllListAsync(ContactUsFilterViewModel filter);
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetPagedListAsync(ContactUsFilterViewModel filter);
        Task<int> MarkAsRead(Guid id, CancellationToken cancellationToken);
        Task<int> NotificationCountAsync();
    }
}
