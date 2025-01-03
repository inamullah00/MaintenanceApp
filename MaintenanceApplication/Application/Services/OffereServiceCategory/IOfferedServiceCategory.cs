using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Domain.Common;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.OffereServiceCategory
{
    public interface IOfferedServiceCategory
    {
        Task<Result<OfferedServiceCategoryResponseDto>> AddServiceCategoryAsync(OfferedServiceCategoryRequestDto requestDto);
        Task<Result<OfferedServiceCategoryResponseDto>> UpdateServiceCategoryAsync(Guid id, OfferedServiceCategoryUpdateDto requestDto);
        Task<Result<string>> DeleteServiceCategoryAsync(Guid categoryId);
        Task<Result<OfferedServiceCategoryResponseDto>> GetServiceCategoryByIdAsync(Guid categoryId);
        Task<Result<List<OfferedServiceCategoryResponseDto>>> GetAllServiceCategoriesAsync();
    }
}
