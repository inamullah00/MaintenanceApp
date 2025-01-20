using Domain.Entity.UserEntities;
using Infrastructure.Data;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.AdminInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.AdminRepositories
{

    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region GetByIdAsync
        public async Task<ApplicationUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
        #endregion

        #region GetAllAsync
        public async Task<List<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }
        #endregion

        #region CreateAsync
        public async Task<ApplicationUser> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            return user;
        }
        #endregion

        #region UpdateAsync
        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(user);
            return user;
        }
        #endregion

        #region QueryUsers
        public IQueryable<ApplicationUser> QueryUsers()
        {
            return _dbContext.Users.AsQueryable();
        }
        #endregion
    }
}
