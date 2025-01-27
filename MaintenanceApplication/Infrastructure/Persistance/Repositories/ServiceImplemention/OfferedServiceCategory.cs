using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Services.OffereServiceCategory;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class OfferedServiceCategory : IOfferedServiceCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferedServiceCategory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OfferedServiceCategoryResponseDto>> AddServiceCategoryAsync(OfferedServiceCategoryRequestDto requestDto)
        {

            if (requestDto == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.InvalidCategoryData, StatusCodes.Status400BadRequest);
            }

            var serviceCategory = _mapper.Map<Domain.Entity.ClientEntities.OfferedServiceCategory>(requestDto);
            var category = await _unitOfWork.OfferedServiceCategoryRepository.CreateAsync(serviceCategory);
            if (category == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.CategoryCreationFailed, StatusCodes.Status500InternalServerError);
            }

            var responseDto = _mapper.Map<OfferedServiceCategoryResponseDto>(category);
            return Result<OfferedServiceCategoryResponseDto>.Success(responseDto, SuccessMessages.CategoryCreated, StatusCodes.Status201Created); // Created
        }


        public async Task<Result<string>> DeleteServiceCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return Result<string>.Failure(ErrorMessages.InvalidCategoryId, StatusCodes.Status400BadRequest);
            }
            var isDeleted = await _unitOfWork.OfferedServiceCategoryRepository.RemoveAsync(categoryId);

            if (!isDeleted)
            {
                return Result<string>.Failure(ErrorMessages.CategoryDeletionFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<string>.Success(SuccessMessages.CategoryDeleted, StatusCodes.Status200OK);
        }


        public async Task<Result<List<OfferedServiceCategoryResponseDto>>> GetAllServiceCategoriesAsync()
        {
            var offeredServiceCategories = await _unitOfWork.OfferedServiceCategoryRepository.GetAllAsync();

            if (offeredServiceCategories == null || !offeredServiceCategories.Any())
            {
                return Result<List<OfferedServiceCategoryResponseDto>>.Failure(ErrorMessages.CategoryNotFound, StatusCodes.Status404NotFound);
            }

            var res = _mapper.Map<List<OfferedServiceCategoryResponseDto>>(offeredServiceCategories);

            return Result<List<OfferedServiceCategoryResponseDto>>.Success(res, SuccessMessages.CategoryCreated, StatusCodes.Status200OK);
        }


        public async Task<Result<OfferedServiceCategoryResponseDto>> GetServiceCategoryByIdAsync(Guid categoryId)
        {

            if (categoryId == Guid.Empty)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.InvalidCategoryId, StatusCodes.Status400BadRequest);
            }
            var offeredServiceCategory = await _unitOfWork.OfferedServiceCategoryRepository.GetByIdAsync(categoryId);

            if (offeredServiceCategory == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.CategoryNotFound, StatusCodes.Status404NotFound);
            }

            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(offeredServiceCategory);

            return Result<OfferedServiceCategoryResponseDto>.Success(res, SuccessMessages.CategoryFetched, StatusCodes.Status200OK);
        }


        public async Task<Result<OfferedServiceCategoryResponseDto>> UpdateServiceCategoryAsync(Guid id, OfferedServiceCategoryUpdateDto requestDto)
        {
            if (id == Guid.Empty || requestDto == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.InvalidCategoryId, StatusCodes.Status400BadRequest);
            }
            var entity = _mapper.Map<Domain.Entity.ClientEntities.OfferedServiceCategory>(requestDto);
            var (isUpdated, updatedEntity) = await _unitOfWork.OfferedServiceCategoryRepository.UpdateAsync(entity, id);
            if (!isUpdated || updatedEntity == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure(ErrorMessages.CategoryUpdateFailed, StatusCodes.Status500InternalServerError);
            }
            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(updatedEntity);

            return Result<OfferedServiceCategoryResponseDto>.Success(res, SuccessMessages.CategoryUpdated, StatusCodes.Status200OK);
        }

    }
}
