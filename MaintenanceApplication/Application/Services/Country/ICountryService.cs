using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.Country
{
    public interface ICountryService
    {
        Task<IList<CountryResponseViewModel>> GetAllAsync();
    }
}
