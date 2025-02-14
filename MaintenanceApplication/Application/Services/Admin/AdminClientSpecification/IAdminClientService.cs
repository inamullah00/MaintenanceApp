using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Admin.AdminClientSpecification
{
    public interface IAdminClientService
    {
        Task CreateClientAsync(ClientCreateViewModel model, CancellationToken cancellationToken);
        Task EditClientAsync(ClientEditViewModel model, CancellationToken cancellationToken);
        Task<ClientEditViewModel> GetClientForEditAsync(Guid id, CancellationToken cancellationToken);
        Task<PaginatedResponse<ClientResponseViewModel>> GetFilteredClientsAsync(ClientFilterViewModel filter);
    }
}
