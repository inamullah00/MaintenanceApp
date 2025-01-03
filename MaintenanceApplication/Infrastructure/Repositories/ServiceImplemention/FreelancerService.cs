using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Ardalis.Specification;
using AutoMapper;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.Freelance.Specification;
using Maintenance.Application.Wrapper;
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

        #region DeleteBidAsync
        public async Task<Result<string>> DeleteBidAsync(Guid bidId)
        {
            SearchBidByMatchingId Specification = new SearchBidByMatchingId(bidId);
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure( "No bid found with the provided ID.", 404);
            }
            var ExistingBid = _mapper.Map<Bid>(bid);

            var result = await _unitOfWork.FreelancerRepository.RemoveAsync(ExistingBid);
            if (!result)
            {
                return Result<string>.Failure("An error occurred while attempting to delete the bid.",500);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Bid deleted successfully", 200);
        }
        #endregion

        #region GetBidsByFreelancerAsync
        public async Task<Result<BidResponseDto>> GetBidsByFreelancerAsync(Guid freelancerId)
        {

            FreelancerBidSearchList Specification = new FreelancerBidSearchList(freelancerId.ToString());
            var bids = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bids == null)
            {
                return Result<BidResponseDto>.Failure("No bid found for the provided freelancer ID.", 404);
            }

            var bidResponseDto = _mapper.Map<BidResponseDto>(bids);

            return Result<BidResponseDto>.Success(bidResponseDto, "Bid found successfully", 200); 
        }
        #endregion

        #region SubmitBidAsync
        public async Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto)
        {
            var bidEntity = _mapper.Map<Bid>(bidRequestDto);

            var result = await _unitOfWork.FreelancerRepository.CreateAsync(bidEntity);

            if (result == null)
            {
                return Result<string>.Failure("An error occurred while submitting the bid.", 500);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Bid submitted successfully", 200);
        }
        #endregion

        #region UpdateBidAsync
        public async Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto, Guid freelancerId)
        {
            SearchBidByMatchingId Specification = new SearchBidByMatchingId(freelancerId.ToString());
             var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);
            if (bid == null)
            {
                return Result<string>.Failure("The specified bid could not be found.", 404);
            }
            var entity = _mapper.Map<Bid>(bidUpdateDto);

            var result = await _unitOfWork.FreelancerRepository.UpdateAsync(entity, freelancerId);
            if (!result.Item1)
            {
                return Result<string>.Failure("An error occurred while updating the bid.", 500);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Bid updated successfully", 200); 
        }

        #endregion

        #region GetBidsByFreelancerAsync
        public async Task<Result<List<BidResponseDto>>> GetBidsByFreelancerAsync(CancellationToken cancellationToken,string? Keyword ="")
        {
           BidSearchList Specification = new BidSearchList(Keyword);
            var bids = await _unitOfWork.FreelancerRepository.GetAllAsync(cancellationToken,Specification);

            var bidList = _mapper.Map<List<BidResponseDto>>(bids);
            return Result<List<BidResponseDto>>.Success(bidList, $"{bids.Count} Bids retrieved successfully.", 200); 
        }
        #endregion

        #region ApproveBidAsync
        public async Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto bidRequestDto)
        {
            SearchBidByMatchingId Specification = new SearchBidByMatchingId(Id);
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure("The specified bid could not be found.", 404 );
            }

            var entity = _mapper.Map<Bid>(bidRequestDto);

            var result = await _unitOfWork.FreelancerRepository.ApproveBidAsync(entity, Id);
            if (!result.Item1)
            {
                return Result<string>.Failure("An error occurred while updating the bid status.", 400);
            }
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Bid Accepted successfully", 200);
        }
        #endregion

    }
}
