using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Interfaces.RepositoryInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
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
        public async Task<PaginatedResponse<CountryResponseViewModel>> GetFilteredCountriesAsync(CountryFilterViewModel filter, ISpecification<Country>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Countries.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = (from Country in query
                                 select new CountryResponseViewModel
                                 {
                                     Id = Country.Id,
                                     DialCode = Country.DialCode,
                                     FlagCode = Country.FlagCode,
                                     CountryCode = Country.CountryCode,
                                     Name = Country.Name,

                                 });
            var totalCount = await filteredQuery.CountAsync();

            var countries = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<CountryResponseViewModel>(countries, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<IList<Country>> GetAllAsync()
        {
            return await _context.Countries.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(Guid? id)
        {
            if (id == null) return null; // Explicitly handle null case
            return await _context.Countries.FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<bool> ExistsAsync(Guid? countryId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countryId);
        }


    }
}
