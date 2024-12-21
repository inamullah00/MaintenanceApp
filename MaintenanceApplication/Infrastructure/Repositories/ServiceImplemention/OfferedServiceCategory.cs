using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entity.UserEntities;
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

        public async Task<(bool Success,OfferedServiceCategoryResponseDto? Servicecategory, string Message)> AddServiceCategoryAsync(OfferedServiceCategoryRequestDto requestDto)
        {
            var ServiceCategory = _mapper.Map<Domain.Entity.UserEntities.OfferedServiceCategory>(requestDto);
           var Category = await _unitOfWork.OfferedServiceCategoryRepository.CreateAsync(ServiceCategory);

            if (Category ==null)
            {
                return (false,null, "An Error Occured While Adding Service Category!");
            }
            var OfferedServiceCategoryResponseDto = _mapper.Map<OfferedServiceCategoryResponseDto>(Category);
            return (true, OfferedServiceCategoryResponseDto, "Service Category Added Successfully");
        }

        public async Task<(bool Success, string Message)> DeleteServiceCategoryAsync(Guid categoryId)
        {
            
            var res = await _unitOfWork.OfferedServiceCategoryRepository.RemoveAsync(categoryId);

            if (!res)
            {
                return (false, "An Error Occured While Deleting Category!");
            }
            return (true, "Category Deleted Successfully");
        }

        public async Task<(bool Success, List<OfferedServiceCategoryResponseDto>? Categories)> GetAllServiceCategoriesAsync()
        {
           var offeredServiceCategory= await _unitOfWork.OfferedServiceCategoryRepository.GetAllAsync();

            var res = _mapper.Map<List<OfferedServiceCategoryResponseDto>>(offeredServiceCategory);
            return (true, res);
        
        }

        public async Task<(bool Success, OfferedServiceCategoryResponseDto? Category)> GetServiceCategoryByIdAsync(Guid categoryId)
        {
            var offeredServiceCategory = await _unitOfWork.OfferedServiceCategoryRepository.GetByIdAsync(categoryId);

            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(offeredServiceCategory);
            return (true, res);
        }

        public async Task<(bool Success, string Message)> UpdateServiceCategoryAsync(Guid id, OfferedServiceCategoryUpdateDto requestDto)
        {

            // Map the update DTO to the domain entity
            var entity = _mapper.Map<Domain.Entity.UserEntities.OfferedServiceCategory>(requestDto);

            // Perform the update operation
            var (isUpdated, updatedEntity) = await _unitOfWork.OfferedServiceCategoryRepository.UpdateAsync(entity, id);

            // Check if the update operation was successful
            if (!isUpdated || updatedEntity == null)
            {
                return (false, "Failed to update the service category. Entity not found or an error occurred.");
            }

            // Map the updated entity to the response DTO
            var res = _mapper.Map<OfferedServiceCategoryResponseDto>(updatedEntity);
            return (true, "Service category updated successfully");
        }
    }
}
