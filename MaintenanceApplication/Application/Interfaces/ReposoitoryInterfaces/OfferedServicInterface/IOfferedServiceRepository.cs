using Ardalis.Specification;
using Maintenance.Domain.Entity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface
{
    public interface IOfferedServiceRepository
    {
        // Create a new OfferedService entity
        Task<OfferedService> CreateAsync(OfferedService entity, CancellationToken cancellationToken = default);

        // Create multiple OfferedService entities
        Task<List<Guid>> CreateRangeAsync(List<OfferedService> entities, CancellationToken cancellationToken = default);

        // Update an existing OfferedService entity
        Task<(bool, OfferedService?)> UpdateAsync(OfferedService entity, Guid id, CancellationToken cancellationToken = default);

        // Remove an OfferedService entity by its ID
        Task<bool> RemoveAsync(OfferedService offeredService, CancellationToken cancellationToken = default);

        // Fetch an OfferedService entity by its ID
        Task<OfferedService> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        // Fetch all OfferedService entities
        Task<List<OfferedService>> GetAllAsync(CancellationToken cancellationToken = default);

        // Fetch a list of OfferedService entities
        Task<IEnumerable<OfferedService>> GetListAsync(CancellationToken cancellationToken = default);

        // Find an OfferedService entity based on a predicate
        Task<OfferedService> FindAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default);

        // Check if an OfferedService entity exists based on a predicate
        Task<bool> ExistsAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
