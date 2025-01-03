﻿using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ServiceInterfaces.FreelancerInterfaces
{
    public interface IFreelancerService
    {

        Task<Result<BidResponseDto>> GetBidsByFreelancerAsync(Guid freelancerId);
        Task<Result<List<BidResponseDto>>> GetBidsByFreelancerAsync();
        Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto);
        Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto,Guid freelancerId);
        Task<Result<string>> DeleteBidAsync(Guid bidId);
        Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto ApproveBidRequestDto);

        //Task<List<OfferedServiceResponseDto>> GetAvailableServiceRequestsAsync(Guid? categoryId = null);
    }
}
