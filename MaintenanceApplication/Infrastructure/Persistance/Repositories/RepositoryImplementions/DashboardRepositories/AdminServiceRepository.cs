using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entity.UserEntities;
using Maintenance.Application.Helper;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories
{
    public class AdminServiceRepository : IAdminServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ServiceExistsAsync(string serviceName)
        {
            string normalizedName = NormalizeNamesHelper.NormalizeNames(serviceName);
            return await _context.Services.AnyAsync(s => s.Name.ToLower() == normalizedName.ToLower());
        }

        public async Task<PaginatedResponse<ServiceResponseViewModel>> GetFilteredServiceAsync(ServiceFilterViewModel filter, ISpecification<Service>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Services.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = query.Select(s => new ServiceResponseViewModel
            {
                Id = s.Id.ToString(),
                Name = s.Name,
                IsActive = s.IsActive,
                IsUserCreated = s.IsUserCreated,
                IsApproved = s.IsApproved
            });

            var totalCount = await filteredQuery.CountAsync();
            var services = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<ServiceResponseViewModel>(services, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<ApplicationUser?> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(s => s.Id == adminId, cancellationToken);
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
        {
            return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == serviceId, cancellationToken);
        }

        public async Task<bool> AddServiceAsync(Service service, CancellationToken cancellationToken = default)
        {
            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateServiceAsync(Service service, CancellationToken cancellationToken = default)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }




    }
}
