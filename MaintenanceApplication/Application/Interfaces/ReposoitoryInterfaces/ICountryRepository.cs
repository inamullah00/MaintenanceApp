using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Interfaces.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        Task<bool> ExistsAsync(Guid? countryId);
        Task<IList<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(Guid? id);

    }
}
