using Domain.Entity.UserEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.AdminInterfaces
{
    public interface IAdminRepository
    {
        Task<ApplicationUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApplicationUser> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        Task<ApplicationUser> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        IQueryable<ApplicationUser> QueryUsers();
    }
}
