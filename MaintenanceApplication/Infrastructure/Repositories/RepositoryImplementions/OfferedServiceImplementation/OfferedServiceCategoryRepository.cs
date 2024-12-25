using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Infrastructure.Data;
using Maintenance.Domain.Entity.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation
{
    public class OfferedServiceCategoryRepository : IOfferedServiceCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferedServiceCategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OfferedServiceCategory> CreateAsync(OfferedServiceCategory entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.OfferedServiceCategories.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public Task<List<Guid>> CreateRangeAsync(List<OfferedServiceCategory> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<OfferedServiceCategory, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OfferedServiceCategory> FindAsync(Expression<Func<OfferedServiceCategory, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OfferedServiceCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
           return await _dbContext.OfferedServiceCategories.ToListAsync();
        }

        public Task<OfferedServiceCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferedServiceCategory>> GetListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, OfferedServiceCategory? UpdatedCategory)> UpdateAsync(OfferedServiceCategory entity, Guid id, CancellationToken cancellationToken = default)
        {
           var serviceCategory = await _dbContext.OfferedServiceCategories.FirstOrDefaultAsync(x =>x.Id == id, cancellationToken);
            if (serviceCategory == null)
            {
                return (false, null);
            }
            serviceCategory.CategoryName = entity.CategoryName;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return (true, serviceCategory);

        }
    }
}
