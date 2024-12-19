using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces
{
    public interface IOfferedServiceCategoryRepository
    {

        // Create a new ClientServiceCategory entity
        Task<OfferedServiceCategory> CreateAsync(OfferedServiceCategory entity, CancellationToken cancellationToken = default);

        // Create multiple ClientServiceCategory entities
        Task<List<Guid>> CreateRangeAsync(List<OfferedServiceCategory> entities, CancellationToken cancellationToken = default);

        // Update an existing ClientServiceCategory entity
        Task<(bool, OfferedServiceCategory? UpdatedCategory)> UpdateAsync(OfferedServiceCategory entity, Guid id, CancellationToken cancellationToken = default);

        // Remove an OfferedService entity by its ID
        Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);

        // Fetch an ClientServiceCategory entity by its ID
        Task<OfferedServiceCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        // Fetch all ClientServiceCategory entities
        Task<List<OfferedServiceCategory>> GetAllAsync(CancellationToken cancellationToken = default);

        // Fetch a list of ClientServiceCategory entities
        Task<List<OfferedServiceCategory>> GetListAsync(CancellationToken cancellationToken = default);

        // Find an ClientServiceCategory entity based on a predicate
        Task<OfferedServiceCategory> FindAsync(Expression<Func<OfferedServiceCategory, bool>> predicate, CancellationToken cancellationToken = default);

        // Check if an ClientServiceCategory entity exists based on a predicate
        Task<bool> ExistsAsync(Expression<Func<OfferedServiceCategory, bool>> predicate, CancellationToken cancellationToken = default);

    }

}
