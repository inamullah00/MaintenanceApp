using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ServiceInterfaces.FreelancerInterfaces
{
    public interface IFreelancerService
    {

        Task<BidResponseDto> GetBidsByFreelancerAsync(Guid freelancerId);
        Task<List<BidResponseDto>> GetBidsByFreelancerAsync();
        Task<(bool Success, string Message)> SubmitBidAsync(BidRequestDto bidRequestDto);
        Task<(bool Success, string Message)> UpdateBidAsync(BidUpdateDto bidUpdateDto,Guid freelancerId);
        Task<(bool Success, string Message)> DeleteBidAsync(Guid bidId);
        Task<(bool Success, string Message)> ApproveBidAsync(Guid Id, ApproveBidRequestDto ApproveBidRequestDto);

        //Task<List<OfferedServiceResponseDto>> GetAvailableServiceRequestsAsync(Guid? categoryId = null);
    }
}
