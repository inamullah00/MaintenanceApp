using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class AdminPackageRepository : IAdminPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminPackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedResponse<PackageResponseViewModel>> GetFilteredPackagesAsync(PackageFilterViewModel filter, ISpecification<Package>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Packages.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = query.Select(s => new PackageResponseViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Offering = s.OfferDetails,
                FreelancerName = s.Freelancer.FullName,
            });

            var totalCount = await filteredQuery.CountAsync();
            var packages = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<PackageResponseViewModel>(packages, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<IList<PackageResponseViewModel>> GetAllAsync()
        {
            var packages = await _context.Packages.Where(a => !a.DeletedAt.HasValue).Select(a => new PackageResponseViewModel
            {
                Id = a.Id,
                Name = a.Name,
            }).ToListAsync().ConfigureAwait(false);
            return packages;
        }

        public async Task<bool> PackageExistsAsync(string packageName)
        {
            return await _context.Packages.AnyAsync(s => !s.DeletedAt.HasValue && s.Name.ToLower() == packageName.ToLower());
        }

        public async Task<Package?> GetPackageByIdAsync(Guid packageId, CancellationToken cancellationToken)
        {
            return await _context.Packages.FirstOrDefaultAsync(s => s.Id == packageId, cancellationToken);
        }

        public async Task<bool> AddPackageAsync(Package package, CancellationToken cancellationToken)
        {
            await _context.Packages.AddAsync(package, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdatePackageAsync(Package package, CancellationToken cancellationToken)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
