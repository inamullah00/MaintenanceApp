using Maintenance.Application.Interfaces.RepositoryInterfaces;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.CountryRepositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            return await _dbContext.Countries.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Country> GetByIdAsync(Guid id)
        {
            return await _dbContext.Countries.FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
