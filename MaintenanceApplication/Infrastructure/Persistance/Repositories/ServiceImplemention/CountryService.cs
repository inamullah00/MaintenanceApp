using Application.Interfaces.IUnitOFWork;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginatedResponse<CountryResponseViewModel>> GetFilteredCountriesAsync(CountryFilterViewModel filter)
        {
            var specification = new CountrySearchList(filter);
            return await _unitOfWork.CountryRepository.GetFilteredCountriesAsync(filter, specification);
        }

        public async Task<IList<CountryResponseViewModel>> GetAllAsync()
        {
            var countries = await _unitOfWork.CountryRepository.GetAllAsync();

            return countries.Select(a => new CountryResponseViewModel
            {
                Id = a.Id,
                Name = a.Name,
                CountryCode = a.CountryCode,
                DialCode = a.DialCode,
                FlagCode = a.FlagCode
            }).ToList();
        }

        public async Task<CountryResponseViewModel> GetByIdAsync(Guid id)
        {
            var country = await _unitOfWork.CountryRepository.GetByIdAsync(id) ?? throw new CustomException("Country not found");

            return new CountryResponseViewModel
            {
                Id = country.Id,
                Name = country.Name,
                CountryCode = country.CountryCode,
                DialCode = country.DialCode,
                FlagCode = country.FlagCode
            };
        }


    }
}
