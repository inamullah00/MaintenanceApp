using Maintenance.Domain.Entity.FreelancerEntities;

namespace Maintenance.Application.Interfaces.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        Task<IList<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(Guid id);

    }
}
