using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces;
using AutoMapper;
using Domain.Common;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class OfferedServiceCategory : IOfferedServiceCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferedServiceCategory(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<OfferedServiceCategoryResponseDto>> AddServiceCategoryAsync(OfferedServiceCategoryRequestDto requestDto)
        {
            var serviceCategory = _mapper.Map<Maintenance.Domain.Entity.Client.OfferedServiceCategory>(requestDto);
            var category = await _unitOfWork.OfferedServiceCategoryRepository.CreateAsync(serviceCategory);
            if (category == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure("An error occurred while adding the service category.", 400 );
            }

            var responseDto = _mapper.Map<OfferedServiceCategoryResponseDto>(category);
            return Result<OfferedServiceCategoryResponseDto>.Success(responseDto, "Service category added successfully.", 201); // Created
        }


        public async Task<Result<string>> DeleteServiceCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return Result<string>.Failure( "The provided category ID is not valid or Empty", 400 );
            }
            var isDeleted = await _unitOfWork.OfferedServiceCategoryRepository.RemoveAsync(categoryId);

            if (!isDeleted)
            {
                return Result<string>.Failure("Failed to delete the category. It may not exist or any other Server error.", 404);
            }

            return Result<string>.Success("Category deleted successfully.", 200);
        }


        public async Task<Result<List<OfferedServiceCategoryResponseDto>>> GetAllServiceCategoriesAsync()
        {
            var offeredServiceCategories = await _unitOfWork.OfferedServiceCategoryRepository.GetAllAsync();

            if (offeredServiceCategories == null || !offeredServiceCategories.Any())
            {
                return Result<List<OfferedServiceCategoryResponseDto>>.Failure( "No service categories found.",404);
            }

            var res = _mapper.Map<List<OfferedServiceCategoryResponseDto>>(offeredServiceCategories);

            return Result<List<OfferedServiceCategoryResponseDto>>.Success( res,"Service categories retrieved successfully.",200);
        }


        public async Task<Result<OfferedServiceCategoryResponseDto>> GetServiceCategoryByIdAsync(Guid categoryId)
        {
       
            if (categoryId == Guid.Empty)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure( "The provided category ID is not valid or Empty",400 );
            }
            var offeredServiceCategory = await _unitOfWork.OfferedServiceCategoryRepository.GetByIdAsync(categoryId);

            if (offeredServiceCategory == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure("No category found with the provided ID.",404);
            }

            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(offeredServiceCategory);

            return Result<OfferedServiceCategoryResponseDto>.Success(res,"Service category retrieved successfully.",200);
        }


        public async Task<Result<OfferedServiceCategoryResponseDto>> UpdateServiceCategoryAsync(Guid id, OfferedServiceCategoryUpdateDto requestDto)
        {
            if (id == Guid.Empty)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure("The provided category ID is not valid or Empty.", 400);
            }
            var entity = _mapper.Map<Maintenance.Domain.Entity.Client.OfferedServiceCategory>(requestDto);
            var (isUpdated, updatedEntity) = await _unitOfWork.OfferedServiceCategoryRepository.UpdateAsync(entity, id);
            if (!isUpdated || updatedEntity == null)
            {
                return Result<OfferedServiceCategoryResponseDto>.Failure("Failed to update the service category.", 400);
            }
            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(updatedEntity);

            return Result<OfferedServiceCategoryResponseDto>.Success(res,"Service category updated successfully.",200);
        }

    }
}
