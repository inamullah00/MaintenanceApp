using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Ardalis.Specification;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Application.Services.Admin.ContentSpecification.Specification;
using Maintenance.Application.Services.Admin.OrderSpecification.Specification;
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
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ContentResponseDto>> CreateContentAsync(CreateContentRequestDto createContentRequestDto, CancellationToken cancellationToken)
        {
            if (createContentRequestDto == null)
            {
                return Result<ContentResponseDto>.Failure(
                    ErrorMessages.InvalidOrEmpty,
                    StatusCodes.Status400BadRequest
                );
            }

            var Content = _mapper.Map<Content>(createContentRequestDto);

            var createdContent = await _unitOfWork.ContentRepository.CreateAsync(Content, cancellationToken);

            var CreatedContentResponseDto = _mapper.Map<ContentResponseDto>(createdContent);

            return Result<ContentResponseDto>.Success(
                CreatedContentResponseDto,
                SuccessMessages.OperationSuccessful,
                StatusCodes.Status201Created
            );
        }

        public async Task<Result<string>> DeleteContentAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return Result<string>.Failure(ErrorMessages.InvalidOrEmptyId, StatusCodes.Status400BadRequest);
            }

            var contentResponseDto = await _unitOfWork.ContentRepository.GetByIdAsync(id, cancellationToken);
            if (contentResponseDto == null)
            {
                return Result<string>.Failure(ErrorMessages.ResourceNotFound, StatusCodes.Status404NotFound);
            }
            var ContentToDelete = _mapper.Map<Content>(contentResponseDto);

            var DeletedContent = await _unitOfWork.ContentRepository.RemoveAsync(ContentToDelete, cancellationToken);

            if (DeletedContent == null)
            {
                return Result<string>.Failure("Failed to delete the App Content.", StatusCodes.Status500InternalServerError);
            }
            return Result<string>.Success(DeletedContent.Id.ToString(), "App Content deleted successfully.", StatusCodes.Status200OK);
        }

        public async Task<Result<List<ContentResponseDto>>> GetAllContentAsync(CancellationToken cancellationToken, string Keyword = "")
        {
            ContentSearchList Specification = new(Keyword);
            var ContentsRes = await _unitOfWork.ContentRepository.GetAllAsync(cancellationToken, Specification);
            return Result<List<ContentResponseDto>>.Success(ContentsRes, SuccessMessages.ContentFetched, StatusCodes.Status200OK);
        }

        public async Task<Result<ContentResponseDto>> GetContentByIdAsync(Guid ContentId, CancellationToken cancellationToken)
        {
            if (ContentId == Guid.Empty)
            {
                return Result<ContentResponseDto>.Failure(
                    ErrorMessages.InvalidOrEmptyId,
                    StatusCodes.Status400BadRequest
                );
            }

            var ContentsRes = await _unitOfWork.ContentRepository.GetByIdAsync(ContentId, cancellationToken);


            var responseDto = _mapper.Map<ContentResponseDto>(ContentsRes);

            return Result<ContentResponseDto>.Success(responseDto, SuccessMessages.ContentFetched, StatusCodes.Status200OK);
        }

        public async Task<Result<ContentResponseDto>> UpdateContentAsync(Guid id, UpdateContentRequestDto updateContentRequestDto, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty || updateContentRequestDto == null)
            {
                return Result<ContentResponseDto>.Failure(
                 ErrorMessages.InvalidServiceData,
                 StatusCodes.Status400BadRequest
             );
            }


            // Retrieve the content entity
            var ExistingContent = await _unitOfWork.ContentRepository.GetByIdAsync(id, cancellationToken);

            if (ExistingContent == null)
            {
                return Result<ContentResponseDto>.Failure(
                    ErrorMessages.ServiceNotFound,
                    StatusCodes.Status404NotFound
                );
            }

            _mapper.Map(updateContentRequestDto, ExistingContent);

            // Update the entity in the repository
            var (isUpdated, updatedContent) = await _unitOfWork.ContentRepository.UpdateAsync(ExistingContent, cancellationToken);
            if (!isUpdated)
            {
                return Result<ContentResponseDto>.Failure(
                    ErrorMessages.ServiceUpdateFailed,
                    StatusCodes.Status500InternalServerError
                );
            }

            // Map the updated entity to the response DTO
            var responseDto = _mapper.Map<ContentResponseDto>(updatedContent);

            // Return success
            return Result<ContentResponseDto>.Success(
                responseDto,
                SuccessMessages.ServiceUpdated,
                StatusCodes.Status200OK
            );
        }
    }
}
