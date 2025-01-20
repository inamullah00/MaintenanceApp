using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.ViewModel.User;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.AdminInterfaces
{
    public interface IAdminRepository
    {
        Task<List<DropdownDto>> GetAdminUsersForDropdownAsync(CancellationToken cancellationToken = default);
        Task<List<ApplicationUser>> GetFilteredUsersAsync(UserFilterViewModel model, CancellationToken cancellationToken = default);
        Task<int> GetFilteredUsersCountAsync(UserFilterViewModel model, CancellationToken cancellationToken = default);
    }
}
