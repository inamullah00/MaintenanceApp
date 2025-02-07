using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IAdminServiceRepository
    {
        Task<bool> AddServiceAsync(Service service, CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default);
        Task<PaginatedResponse<ServiceResponseViewModel>> GetFilteredServiceAsync(ServiceFilterViewModel filter, ISpecification<Service>? specification = null);
        Task<Service?> GetServiceByIdAsync(Guid serviceId, CancellationToken cancellationToken = default);
        Task<bool> ServiceExistsAsync(string serviceName);
        Task<bool> UpdateServiceAsync(Service service, CancellationToken cancellationToken = default);
    }
}
