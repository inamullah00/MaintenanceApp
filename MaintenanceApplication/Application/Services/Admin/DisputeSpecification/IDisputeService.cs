using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.DisputeSpecification
{
    public interface IDisputeService
    {
        // Get all disputes with optional keyword search
        Task<Result<List<DisputeResponseDto>>> GetAllDisputesAsync(CancellationToken cancellationToken, string Keyword = "");

        // Get a specific dispute by ID
        Task<Result<DisputeResponseDto>> GetDisputeByIdAsync(Guid id, CancellationToken cancellationToken);

        // Create a new dispute
        Task<Result<DisputeResponseDto>> CreateDisputeAsync(CreateDisputeRequest createDisputeRequestDto, CancellationToken cancellationToken);

        // Resolve an existing dispute
        Task<Result<DisputeResolveResponseDto>> ResolveDisputeAsync(Guid id, CreateDisputeResolveDto requestDto, CancellationToken cancellationToken);

        // Delete a dispute by ID
        Task<Result<string>> DeleteDisputeAsync(Guid id, CancellationToken cancellationToken);
    }
}
