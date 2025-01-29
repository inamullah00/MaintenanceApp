using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Maintenance.Domain.Entity.ClientEntities;
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
        Task<OfferedServiceCategoryResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        // Fetch all ClientServiceCategory entities
        Task<List<OfferedServiceCategoryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

    }

}
