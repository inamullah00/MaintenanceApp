using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Admin.AdminServiceSpecification
{
    public interface IAdminSevService
    {
        Task ActivateServiceAsync(Guid serviceId);
        Task AddServiceAsync(ServiceCreateViewModel model);
        Task ApproveServiceAsync(Guid serviceId);
        Task DeactivateServiceAsync(Guid serviceId);
        Task<IList<ServiceResponseViewModel>> GetAllServicesAsync();
        Task<PaginatedResponse<ServiceResponseViewModel>> GetFilteredServicesAsync(ServiceFilterViewModel filter);
        Task<ServiceEditViewModel> GetServiceForEditAsync(Guid id);
        Task UpdateServiceAsync(ServiceEditViewModel model);
    }
}
