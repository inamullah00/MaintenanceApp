using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Admin.AdminPackageSpecification
{
    public interface IAdminPackageService
    {
        Task AddPackageAsync(PackageCreateViewModel model, CancellationToken cancellationToken);
        Task DeletePackageAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<PackageResponseViewModel>> GetAllPackagesAsync();
        Task<PaginatedResponse<PackageResponseViewModel>> GetFilteredPackagesAsync(PackageFilterViewModel filter);
        Task<PackageEditViewModel> GetPackageForEditAsync(Guid id, CancellationToken cancellationToken);
        Task UpdatePackageAsync(PackageEditViewModel model, CancellationToken cancellationToken);
    }
}
