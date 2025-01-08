using Ardalis.Specification;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces
{
    public interface IDisputeRepository
    {

        // Get all disputes with optional specification for filtering
        Task<List<DisputeResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Dispute>? specification = null);

        // Get dispute by its ID
        Task<DisputeResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        // Create a new dispute
        Task<Dispute> CreateAsync(Dispute dispute, CancellationToken cancellationToken);

        // Update an existing dispute
        Task<Dispute> UpdateAsync(Dispute dispute, CancellationToken cancellationToken);

        // Update specific fields of a dispute (partial update)
        //Task<bool> UpdateFieldsAsync(Dispute dispute, string[] fieldsToUpdate, CancellationToken cancellationToken = default);

        // Remove a dispute (soft delete or hard delete depending on the implementation)
        Task<Dispute> RemoveAsync(Dispute dispute, CancellationToken cancellationToken);
    }
}

