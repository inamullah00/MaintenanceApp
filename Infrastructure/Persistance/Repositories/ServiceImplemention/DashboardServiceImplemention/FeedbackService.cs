using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Services.Admin.FeedbackSpecification;
using Maintenance.Application.Services.Admin.FeedbackSpecification.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<FeedbackResponseDto>> CreateFeedbackAsync(CreateFeedbackRequestDto createFeedbackRequestDto, CancellationToken cancellationToken)
        {
            if (createFeedbackRequestDto == null)
            {
                return Result<FeedbackResponseDto>.Failure(
                    ErrorMessages.InvalidOrEmpty,
                    StatusCodes.Status400BadRequest
                );
            }

            var feedback = _mapper.Map<Feedback>(createFeedbackRequestDto);

            var createdFeedback = await _unitOfWork.FeedbackRepository.CreateAsync(feedback, cancellationToken);

            var createdFeedbackResponseDto = _mapper.Map<FeedbackResponseDto>(createdFeedback);

            return Result<FeedbackResponseDto>.Success(
                createdFeedbackResponseDto,
                SuccessMessages.OperationSuccessful,
                StatusCodes.Status201Created
            );
        }

        public async Task<Result<string>> DeleteFeedbackAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Result<string>.Failure(ErrorMessages.InvalidOrEmptyId, StatusCodes.Status400BadRequest);
            }

            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id, cancellationToken);
            if (feedback == null)
            {
                return Result<string>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
            }


            var deletedFeedback = await _unitOfWork.FeedbackRepository.RemoveAsync(feedback, cancellationToken);

            if (deletedFeedback == null)
            {
                return Result<string>.Failure("Failed to delete feedback.", StatusCodes.Status500InternalServerError);
            }

            return Result<string>.Success(deletedFeedback.Id.ToString(), "Feedback deleted successfully.", StatusCodes.Status200OK);
        }

        public async Task<Result<List<FeedbackResponseDto>>> GetAllFeedbackAsync(CancellationToken cancellationToken, string keyword = "")
        {
            FeedbackSearchList specification = new(keyword);
            var feedbackList = await _unitOfWork.FeedbackRepository.GetAllAsync(cancellationToken, specification);
            return Result<List<FeedbackResponseDto>>.Success(feedbackList, SuccessMessages.FeedbackFetched, StatusCodes.Status200OK);
        }

        public async Task<Result<FeedbackResponseDto>> GetFeedbackRatingForFreelancerAsync(Guid feedbackId, CancellationToken cancellationToken)
        {
            if (feedbackId == Guid.Empty)
            {
                return Result<FeedbackResponseDto>.Failure(
                    ErrorMessages.InvalidOrEmptyId,
                StatusCodes.Status400BadRequest
                );
            }

            var feedback = await _unitOfWork.FeedbackRepository.GetFeedbackRatingByIdAsync(feedbackId, cancellationToken);

            if (feedback == null)
            {
                return Result<FeedbackResponseDto>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
            }

            var responseDto = _mapper.Map<FeedbackResponseDto>(feedback);

            return Result<FeedbackResponseDto>.Success(responseDto, SuccessMessages.FeedbackFetched, StatusCodes.Status200OK);
        }

        public async Task<Result<FeedbackResponseDto>> UpdateFeedbackAsync(Guid id, UpdateFeedbackRequestDto updateFeedbackRequestDto, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty || updateFeedbackRequestDto == null)
            {
                return Result<FeedbackResponseDto>.Failure(
                    ErrorMessages.InvalidServiceData,
                    StatusCodes.Status400BadRequest
                );
            }

            var existingFeedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id, cancellationToken);

            if (existingFeedback == null)
            {
                return Result<FeedbackResponseDto>.Failure(
                    ErrorMessages.ServiceNotFound,
                    StatusCodes.Status404NotFound
                );
            }

            _mapper.Map(updateFeedbackRequestDto, existingFeedback);

            var (isUpdated, updatedFeedback) = await _unitOfWork.FeedbackRepository.UpdateAsync(existingFeedback, cancellationToken);

            if (!isUpdated)
            {
                return Result<FeedbackResponseDto>.Failure(
                    ErrorMessages.ServiceUpdateFailed,
                    StatusCodes.Status500InternalServerError
                );
            }

            var responseDto = _mapper.Map<FeedbackResponseDto>(updatedFeedback);

            return Result<FeedbackResponseDto>.Success(
                responseDto,
                SuccessMessages.ServiceUpdated,
                StatusCodes.Status200OK
            );
        }

       public async Task<Result<List<FeedbackResponseDto>>> FilterRatingsAsync(FilterRatingsDto filterRatingsDto, CancellationToken cancellationToken)
        {
            FilterRatingSpecification specification = new(filterRatingsDto);
            var feedbackList = await _unitOfWork.FeedbackRepository.GetAllAsync(cancellationToken, specification);
            return Result<List<FeedbackResponseDto>>.Success(feedbackList, SuccessMessages.FeedbackFetched, StatusCodes.Status200OK);
        }
    }
}
