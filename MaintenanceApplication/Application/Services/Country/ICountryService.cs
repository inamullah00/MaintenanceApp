using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services
{
    public interface ICountryService
    {
        Task<IList<CountryResponseViewModel>> GetAllAsync();
        Task<PaginatedResponse<CountryResponseViewModel>> GetFilteredCountriesAsync(CountryFilterViewModel filter);
    }
}
