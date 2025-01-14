using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.ContentSpecification
{
    public interface IContentService
    {
        // Get all content with optional keyword search
        Task<Result<List<ContentResponseDto>>> GetAllContentAsync(CancellationToken cancellationToken, string Keyword = "");

        // Get content by its ID
        Task<Result<ContentResponseDto>> GetContentByIdAsync(Guid ContentId, CancellationToken cancellationToken);

        // Create new content
        Task<Result<ContentResponseDto>> CreateContentAsync(CreateContentRequestDto createContentRequestDto, CancellationToken cancellationToken);

        // Update existing content by its ID
        Task<Result<ContentResponseDto>> UpdateContentAsync(Guid id, UpdateContentRequestDto updateContentRequestDto, CancellationToken cancellationToken);

        // Delete content by its ID
        Task<Result<string>> DeleteContentAsync(Guid id, CancellationToken cancellationToken);
    }

}
