using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces
{
    public interface IFreelancerRepository
    {

        Task<Bid> CreateAsync(Bid entity, CancellationToken cancellationToken = default);

        Task<List<Guid>> CreateRangeAsync(List<Bid> entities, CancellationToken cancellationToken = default);

        Task<(bool, Bid?)> UpdateAsync(Bid entity, Guid id, CancellationToken cancellationToken = default);
        Task<(bool, Bid?)> ApproveBidAsync(Bid entity, Guid id, CancellationToken cancellationToken = default);

        Task<bool> RemoveAsync(Bid offeredService, CancellationToken cancellationToken = default);
        Task<BidResponseDto> GetByIdAsync(ISpecification<Bid> specification , CancellationToken cancellationToken = default);

        Task<List<BidResponseDto>> GetAllAsync(CancellationToken cancellationToken = default, ISpecification<Bid>? specification = null);
        Task<List<FilteredFreelancerResponseDto>> GetByFilterAsync(CancellationToken cancellationToken = default, ISpecification<Bid>? specification = null);
        Task<Bid> FindAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken = default);


        // Package Methods
        Task<Package> GetPackageByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Package>> GetAllPackagesAsync(CancellationToken cancellationToken);
        Task<Package> CreatePackageAsync(Package package, CancellationToken cancellationToken);
        Task<Package> UpdatePackageAsync(Package package, CancellationToken cancellationToken);
        Task<Package> DeletePackageAsync(Package package, CancellationToken cancellationToken);

    }
}
