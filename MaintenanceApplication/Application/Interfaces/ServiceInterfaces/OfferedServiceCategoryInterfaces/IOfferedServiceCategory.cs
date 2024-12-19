using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces
{
    public interface IOfferedServiceCategory
    {
        Task<(bool Success, OfferedServiceCategoryResponseDto? Servicecategory, string Message)> AddServiceCategoryAsync(OfferedServiceCategoryRequestDto requestDto);
        Task<(bool Success, string Message)> UpdateServiceCategoryAsync(Guid id,OfferedServiceCategoryUpdateDto requestDto);
        Task<(bool Success, string Message)> DeleteServiceCategoryAsync(Guid categoryId);
        Task<(bool Success, OfferedServiceCategoryResponseDto? Category)> GetServiceCategoryByIdAsync(Guid categoryId);
        Task<(bool Success, List<OfferedServiceCategoryResponseDto>? Categories)> GetAllServiceCategoriesAsync();
    }
}
