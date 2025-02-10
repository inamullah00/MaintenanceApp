using Maintenance.Application.Interfaces.RepositoryInterfaces;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.CountryRepositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            return await _context.Countries.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Country> GetByIdAsync(Guid? id)
        {
            return await _context.Countries.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsAsync(Guid? countryId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countryId);
        }


    }
}
