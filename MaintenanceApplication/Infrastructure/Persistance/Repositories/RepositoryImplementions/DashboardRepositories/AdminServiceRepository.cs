using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories
{
    public class AdminServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<PaginatedResponse<ServiceResponseViewModel>> GetFilteredServiceAsync(ServiceFilterViewModel filter, ISpecification<Service>? specification = null)
        //{
        //    var query = SpecificationEvaluator.Default.GetQuery(query: _context.Services.AsNoTracking().AsQueryable(), specification: specification);

        //    var filteredQuery = (from Service in query
        //                         select new ServiceResponseViewModel
        //                         {
        //                             Id = Service.Id.ToString(),
        //                             Name = Service.Name,
        //                         });
        //    var totalCount = await filteredQuery.CountAsync();
        //    var services = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        //    return new PaginatedResponse<ServiceResponseViewModel>(services, totalCount, filter.PageNumber, filter.PageSize);
        //}

        public async Task<FreelancerService> GetByIdAsync(Guid freelancerId, Guid serviceId)
        {
            return await _context.FreelancerServices.Include(fs => fs.Freelancer).Include(fs => fs.Service).FirstOrDefaultAsync(fs => fs.FreelancerId == freelancerId && fs.ServiceId == serviceId);
        }

        public async Task AddAsync(FreelancerService freelancerService)
        {
            await _context.FreelancerServices.AddAsync(freelancerService);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FreelancerService freelancerService)
        {
            _context.FreelancerServices.Update(freelancerService);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid freelancerId, Guid serviceId)
        {
            var entity = await GetByIdAsync(freelancerId, serviceId);
            if (entity != null)
            {
                _context.FreelancerServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
