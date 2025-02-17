using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IAdminPackageRepository
    {
        Task<bool> AddPackageAsync(Package package, CancellationToken cancellationToken);
        Task<IList<PackageResponseViewModel>> GetAllAsync();
        Task<PaginatedResponse<PackageResponseViewModel>> GetFilteredPackagesAsync(PackageFilterViewModel filter, ISpecification<Package>? specification = null);
        Task<Package?> GetPackageByIdAsync(Guid packageId, CancellationToken cancellationToken);
        Task<bool> PackageExistsAsync(string packageName);
        Task<bool> UpdatePackageAsync(Package package, CancellationToken cancellationToken);
    }
}
