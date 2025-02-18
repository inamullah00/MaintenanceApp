using Application.Dto_s.ClientDto_s;
using Ardalis.Specification;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance
{
    public interface IFreelancerService
    {

        Task<Result<BidResponseDto>> GetBidByFreelancerAsync(Guid freelancerId,CancellationToken cancellationToken);
        Task<Result<List<BidResponseDto>>> GetBidsByFreelancerAsync(Guid offeredServiceId, CancellationToken cancellationToken);
        Task<Result<List<FilteredFreelancerResponseDto>>> FilterFreelancersAsync(FilterFreelancerRequestDto requestDto, CancellationToken cancellationToken);
        Task<Result<List<OrderStatusResponseDto>>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken);
        Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto);
        Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto, Guid freelancerId);
        Task<Result<string>> DeleteBidAsync(Guid bidId);
        Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto ApproveBidRequestDto);
        Task<Result<List<RequestedServiceResponseDto>>> GetRequestedServicesAsync(CancellationToken cancellationToken, string? keyword);



        // Package Methods
        Task<Result<Package>> GetPackageByIdAsync(Guid packageId , CancellationToken cancellationToken);
        Task<Result<List<Package>>> GetPackagesAsync(CancellationToken cancellationToken);
        Task<Result<Package>> CreatePackageAsync(CreatePackageRequestDto packageRequestDto, CancellationToken cancellationToken);
        Task<Result<Package>> UpdatePackageAsync(Guid packageId, Package package, CancellationToken cancellationToken);
        Task<Result<bool>> DeletePackageAsync(Guid packageId, CancellationToken cancellationToken);


    }
}
