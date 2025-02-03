using Application.Dto_s.ClientDto_s;
using Ardalis.Specification;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance
{
    public interface IFreelancerService
    {

        Task<Result<BidResponseDto>> GetBidsByFreelancerAsync(Guid freelancerId);
        Task<Result<List<BidResponseDto>>> GetBidsByFreelancerAsync(CancellationToken cancellationToken , string ? Keyword ="");
        Task<Result<List<FilteredFreelancerResponseDto>>> FilterFreelancersAsync(FilterFreelancerRequestDto requestDto, CancellationToken cancellationToken);
        Task<Result<List<OrderStatusResponseDto>>> GetOrdersByStatusAsync(string status, CancellationToken cancellationToken);
        Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto);
        Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto, Guid freelancerId);
        Task<Result<string>> DeleteBidAsync(Guid bidId);
        Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto ApproveBidRequestDto);
        Task<Result<List<RequestedServiceResponseDto>>> GetRequestedServicesAsync(CancellationToken cancellationToken, string? keyword);




    }
}
