using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Interfaces.ServiceInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention
{
    public class FreelancerService : IFreelancerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FreelancerService(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<(bool Success, string Message)> DeleteBidAsync(Guid bidId)
        {
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(bidId);
            if (bid == null)
            {
                return (false, "Bid not found");
            }

            var result = await _unitOfWork.FreelancerRepository.RemoveAsync(bid);
            if (!result)
            {
                return (false, "Failed to delete bid");
            }

            await _unitOfWork.SaveChangesAsync();
            return (true, "Bid deleted successfully");
        }

        public async Task<BidResponseDto> GetBidsByFreelancerAsync(Guid freelancerId)
        {
            var bids = await _unitOfWork.FreelancerRepository.GetByIdAsync(freelancerId);
            return _mapper.Map<BidResponseDto>(bids);
        }

        public async Task<(bool Success, string Message)> SubmitBidAsync(BidRequestDto bidRequestDto)
        {
            var bidEntity = _mapper.Map<Bid>(bidRequestDto);

            await _unitOfWork.FreelancerRepository.CreateAsync(bidEntity);
            await _unitOfWork.SaveChangesAsync();

            return (true, "Bid submitted successfully");
        }

        public async Task<(bool Success, string Message)> UpdateBidAsync(BidUpdateDto bidUpdateDto,Guid freelancerId)
        {
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(freelancerId);
            if (bid == null)
            {
                return (false, "Bid not found");
            }

            var entity = _mapper.Map<Bid>(bidUpdateDto);
            var result = await _unitOfWork.FreelancerRepository.UpdateAsync(entity, freelancerId);
            if (!result.Item1)
            {
                return (false, "Failed to update bid");
            }

            await _unitOfWork.SaveChangesAsync();
            return (true, "Bid updated successfully");
        }

        public async Task<List<BidResponseDto>> GetBidsByFreelancerAsync()
        {
            var Bids = await _unitOfWork.FreelancerRepository.GetAllAsync();
            var BidList = _mapper.Map<List<BidResponseDto>>(Bids);
            return BidList;
        }

       public async Task<(bool Success, string Message)> ApproveBidAsync(Guid Id, ApproveBidRequestDto bidRequestDto)
        {
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Id);
            if (bid == null)
            {
                return (false, "Bid not found");
            }

            var entity = _mapper.Map<Bid>(bidRequestDto);
            var result = await _unitOfWork.FreelancerRepository.ApproveBidAsync(entity, Id);
            if (!result.Item1)
            {
                return (false, "Failed to Update bid Status");
            }

            await _unitOfWork.SaveChangesAsync();
            return (true, "Bid Accepted successfully");
        }
    }
}
