using Application.Dto_s.ClientDto_s;
using Ardalis.Specification;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntities;
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
        Task<OfferedService> CreateAsync(OfferedService entity, CancellationToken cancellationToken = default);

        Task<List<Guid>> CreateRangeAsync(List<OfferedService> entities, CancellationToken cancellationToken = default);

        Task<(bool, OfferedService?)> UpdateAsync(OfferedService entity, Guid id, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(OfferedService offeredService, CancellationToken cancellationToken = default);

        Task<OfferedServiceResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<OfferedServiceResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<RequestedServiceResponseDto>> GetRequestedServicesAsync(ISpecification<OfferedService> specification, CancellationToken cancellationToken = default);

        // Find an OfferedService entity based on a predicate
        Task<OfferedService> FindAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default);

        // Check if an OfferedService entity exists based on a predicate
        Task<bool> ExistsAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
