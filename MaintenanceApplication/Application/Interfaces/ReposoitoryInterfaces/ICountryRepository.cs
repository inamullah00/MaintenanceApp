using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Interfaces.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        Task<bool> ExistsAsync(Guid? countryId);
        Task<IList<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(Guid? id);
        Task<PaginatedResponse<CountryResponseViewModel>> GetFilteredCountriesAsync(CountryFilterViewModel filter, ISpecification<Country>? specification = null);
    }
}
