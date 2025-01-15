using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.DisputeSpecification.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class DisputeService : IDisputeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DisputeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        #region Dispute Management

        #region Get All Disputes
        public async Task<Result<List<DisputeResponseDto>>> GetAllDisputesAsync(CancellationToken cancellationToken, string? Keyword = "")
        {
            try
            {
                // Create the specification for fetching disputes based on keyword
                ContentSearchList specification = new(Keyword);

                var disputes = await _unitOfWork.DisputeRepository.GetAllAsync(cancellationToken, specification);

                // Map disputes to DTOs
                var disputeDtos = _mapper.Map<List<DisputeResponseDto>>(disputes);

                // Return success with list of disputes
                return Result<List<DisputeResponseDto>>.Success(disputeDtos, SuccessMessages.OperationSuccessful, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                // Return failure if there's an error
                return Result<List<DisputeResponseDto>>.Failure($"Error fetching disputes: {ex.Message}", "An error occurred", StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Get Dispute by ID
        public async Task<Result<DisputeResponseDto>> GetDisputeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result<DisputeResponseDto>.Failure(
                        ErrorMessages.InvalidOrEmptyId,
                        HttpResponseCodes.BadRequest
                    );
                }

                var dispute = await _unitOfWork.DisputeRepository.GetByIdAsync(id, cancellationToken);
                if (dispute == null)
                {
                    return Result<DisputeResponseDto>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
                }

                var disputeDto = _mapper.Map<DisputeResponseDto>(dispute);
                return Result<DisputeResponseDto>.Success(disputeDto, SuccessMessages.OperationSuccessful, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<DisputeResponseDto>.Failure($"Error fetching dispute by ID: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Create Dispute
        public async Task<Result<DisputeResponseDto>> CreateDisputeAsync(CreateDisputeRequest createDisputeRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                if (createDisputeRequestDto == null)
                {
                    return Result<DisputeResponseDto>.Failure(
                        ErrorMessages.InvalidOrEmpty,
                        HttpResponseCodes.BadRequest
                    );
                }

                var dispute = _mapper.Map<Dispute>(createDisputeRequestDto);

                var createdDispute = await _unitOfWork.DisputeRepository.CreateAsync(dispute, cancellationToken);

                var CreatedDisputeResponseDto = _mapper.Map<DisputeResponseDto>(createdDispute);

                return Result<DisputeResponseDto>.Success(
                    CreatedDisputeResponseDto,
                    SuccessMessages.OperationSuccessful,
                    StatusCodes.Status201Created
                );
            }
            catch (Exception ex)
            {
                return Result<DisputeResponseDto>.Failure(
                    $"Error creating dispute: {ex.Message}",
                    "An error occurred while creating the dispute",
                    StatusCodes.Status500InternalServerError
                );
            }
        }
        #endregion

        #region Resolve Dispute
        public async Task<Result<DisputeResolveResponseDto>> ResolveDisputeAsync(Guid id, CreateDisputeResolveDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || requestDto == null)
                {
                    return Result<DisputeResolveResponseDto>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
                }

                var dispute = await _unitOfWork.DisputeRepository.GetByIdAsync(id, cancellationToken);
                if (dispute == null)
                {
                    return Result<DisputeResolveResponseDto>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
                }

               var DisouteEntity =  _mapper.Map<Dispute>(dispute);
            
                DisouteEntity.DisputeStatus = requestDto.DisputeStatus;
                DisouteEntity.ResolutionDetails = requestDto.ResolutionDetails;
                DisouteEntity.ResolvedAt = DateTime.UtcNow;


                var ResolvedDispute = await _unitOfWork.DisputeRepository.UpdateAsync(DisouteEntity, cancellationToken);

                return ResolvedDispute == null
                    ? Result<DisputeResolveResponseDto>.Success( _mapper.Map<DisputeResolveResponseDto>(ResolvedDispute) ,"Dispute resolved successfully.", StatusCodes.Status200OK)
                    : Result<DisputeResolveResponseDto>.Failure("Failed to resolve dispute.An error occurred while resolving the dispute", StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return Result<DisputeResolveResponseDto>.Failure($"Error resolving dispute: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Update Dispute Status
        //public async Task<Result<string>> UpdateDisputeStatusAsync(Guid id, UpdateDisputeStatus updateDisputeStatusDto, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        if (id == Guid.Empty || updateDisputeStatusDto == null)
        //        {
        //            return Result<string>.Failure(ErrorMessages.InvalidOrEmptyId, StatusCodes.Status400BadRequest);
        //        }

        //        var dispute = await _unitOfWork.DisputeRepository.GetByIdAsync(id, cancellationToken);
        //        if (dispute == null)
        //        {
        //            return Result<string>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
        //        }

        //        // Update the dispute status
        //        dispute.Status = updateDisputeStatusDto.Status;
        //        dispute.UpdatedAt = DateTime.UtcNow;

        //        var isUpdated = await _unitOfWork.DisputeRepository.UpdateAsync(dispute, cancellationToken);

        //        return isUpdated
        //            ? Result<string>.Success("Dispute status updated successfully.", StatusCodes.Status200OK)
        //            : Result<string>.Failure("Failed to update dispute status.", StatusCodes.Status500InternalServerError);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<string>.Failure($"Error updating dispute status: {ex.Message}", "An error occurred", StatusCodes.Status500InternalServerError);
        //    }
        //}
        #endregion

        #region Delete Dispute
        public async Task<Result<string>> DeleteDisputeAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result<string>.Failure(ErrorMessages.InvalidOrEmptyId, StatusCodes.Status400BadRequest);
                }

                var dispute = await _unitOfWork.DisputeRepository.GetByIdAsync(id, cancellationToken);
                if (dispute == null)
                {
                    return Result<string>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
                }
                var DisputeToDelete = _mapper.Map<Dispute>(dispute);

                var DeletedDispute = await _unitOfWork.DisputeRepository.RemoveAsync(DisputeToDelete, cancellationToken);

                if (DeletedDispute == null) 
                {
                    return Result<string>.Failure("Failed to delete the dispute.", StatusCodes.Status500InternalServerError);
                }
                return Result<string>.Success("Dispute deleted successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error deleting dispute: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #endregion


    }
}
