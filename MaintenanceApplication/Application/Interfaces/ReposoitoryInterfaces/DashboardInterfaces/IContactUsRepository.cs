using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.SettingEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IContactUsRepository
    {
        Task<bool> AddContactUs(ContactUs contactUs, CancellationToken cancellationToken = default);
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetAllListAsync(ContactUsFilterViewModel filter, ISpecification<ContactUs>? specification = null);
        Task<ContactUs?> GetContactUsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PaginatedResponse<ContactUsResponseViewModel>> GetPagedListAsync(ContactUsFilterViewModel filter);
        Task<int> NotificationCount();
        Task<bool> UpdateContactUs(ContactUs contactUs, CancellationToken cancellationToken = default);
    }
}
