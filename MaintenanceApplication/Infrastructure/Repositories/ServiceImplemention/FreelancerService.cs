using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Ardalis.Specification;
using AutoMapper;
using Infrastructure.Data;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.Freelance.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Freelancer;
using Microsoft.AspNetCore.Http;
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

            if (bidId == Guid.Empty)
            {
                return Result<string>.Failure( ErrorMessages.InvalidFreelancerBidId, StatusCodes.Status400BadRequest);
            }

            SearchBidByMatchingId Specification = new SearchBidByMatchingId(bidId);

            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure( ErrorMessages.BidNotFound, StatusCodes.Status404NotFound);
            }
            var ExistingBid = _mapper.Map<Bid>(bid);

            var result = await _unitOfWork.FreelancerRepository.RemoveAsync(ExistingBid);
            if (!result)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidDeletionFailed, StatusCodes.Status500InternalServerError);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success(SuccessMessages.FreelancerBidDeleted, StatusCodes.Status200OK);
        }
        #endregion

        #region GetBidsByFreelancerAsync
        public async Task<Result<BidResponseDto>> GetBidsByFreelancerAsync(Guid freelancerId)
        {
            if (freelancerId == Guid.Empty)
            {
                return Result<BidResponseDto>.Failure( ErrorMessages.InvalidFreelancerId,StatusCodes.Status400BadRequest);
            }
            
            FreelancerBidSearchList Specification = new FreelancerBidSearchList(freelancerId.ToString());
          
            var bids = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bids == null)
            {
                return Result<BidResponseDto>.Failure(ErrorMessages.FreelancerBidNotFound, StatusCodes.Status404NotFound);
            }

            var bidResponseDto = _mapper.Map<BidResponseDto>(bids);

            return Result<BidResponseDto>.Success(bidResponseDto, SuccessMessages.FreelancerBidFetched, StatusCodes.Status200OK); 
        }
        #endregion

        #region SubmitBidAsync
        public async Task<Result<string>> SubmitBidAsync(BidRequestDto bidRequestDto)
        {

            if (bidRequestDto == null)
            {
                return Result<string>.Failure( ErrorMessages.InvalidFreelancerBidData, StatusCodes.Status400BadRequest);
            }

            var bidEntity = _mapper.Map<Bid>(bidRequestDto);

            var result = await _unitOfWork.FreelancerRepository.CreateAsync(bidEntity);

            if (result == null)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidCreationFailed, StatusCodes.Status500InternalServerError);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success(SuccessMessages.FreelancerBidCreated, StatusCodes.Status200OK);
        }
        #endregion

        #region UpdateBidAsync
        public async Task<Result<string>> UpdateBidAsync(BidUpdateDto bidUpdateDto, Guid freelancerId)
        {

            if (freelancerId == Guid.Empty || bidUpdateDto == null)
            {
                return Result<string>.Failure(ErrorMessages.InvalidFreelancerBidData,StatusCodes.Status400BadRequest);
            }


            SearchBidByMatchingId Specification = new SearchBidByMatchingId(freelancerId.ToString());

             var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);
            if (bid == null)
            {
                return Result<string>.Failure(ErrorMessages.BidNotFound, StatusCodes.Status404NotFound);
            }
            var entity = _mapper.Map<Bid>(bidUpdateDto);

            var result = await _unitOfWork.FreelancerRepository.UpdateAsync(entity, freelancerId);
            if (!result.Item1)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidUpdateFailed, StatusCodes.Status500InternalServerError);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(SuccessMessages.FreelancerBidUpdated, StatusCodes.Status200OK); 
        }

        #endregion

        #region GetBidsByFreelancerAsync
        public async Task<Result<List<BidResponseDto>>> GetBidsByFreelancerAsync(CancellationToken cancellationToken,string? Keyword ="")
        {
           BidSearchList Specification = new BidSearchList(Keyword);
            var bids = await _unitOfWork.FreelancerRepository.GetAllAsync(cancellationToken,Specification);

            var bidList = _mapper.Map<List<BidResponseDto>>(bids);
            return Result<List<BidResponseDto>>.Success(bidList, $"{bids.Count} {SuccessMessages.FreelancerBidFetched}", StatusCodes.Status200OK); 
        }
        #endregion

        #region ApproveBidAsync
        public async Task<Result<string>> ApproveBidAsync(Guid Id, ApproveBidRequestDto bidRequestDto)
        {
            if (Id ==Guid.Empty || bidRequestDto == null)
            {
                return Result<string>.Failure(
                    ErrorMessages.InvalidFreelancerBidData,
                    HttpResponseCodes.BadRequest
                );
            }

            SearchBidByMatchingId Specification = new SearchBidByMatchingId(Id);
            var bid = await _unitOfWork.FreelancerRepository.GetByIdAsync(Specification);

            if (bid == null)
            {
                return Result<string>.Failure(ErrorMessages.BidNotFound, StatusCodes.Status404NotFound );
            }

            var entity = _mapper.Map<Bid>(bidRequestDto);

            var result = await _unitOfWork.FreelancerRepository.ApproveBidAsync(entity, Id);
            if (!result.Item1)
            {
                return Result<string>.Failure(ErrorMessages.FreelancerBidApprovalFailed, StatusCodes.Status500InternalServerError);
            }
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(SuccessMessages.FreelancerBidAccepted, StatusCodes.Status200OK);
        }
        #endregion

    }
}
