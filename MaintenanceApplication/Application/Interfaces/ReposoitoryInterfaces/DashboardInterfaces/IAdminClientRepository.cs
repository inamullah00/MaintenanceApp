using Ardalis.Specification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IAdminClientRepository
    {
        Task<bool> AddClient(Client client, CancellationToken cancellationToken = default);
        Task<Client> GetClientByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Client?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Client> GetClientByPhoneNumberAsync(string phoneNumber, Guid? countryId, CancellationToken cancellationToken);
        Task<PaginatedResponse<ClientResponseViewModel>> GetFilteredClientAsync(ClientFilterViewModel filter, ISpecification<Client>? specification = null);
        Task<bool> UpdateClient(Client client, CancellationToken cancellationToken = default);
    }
}
