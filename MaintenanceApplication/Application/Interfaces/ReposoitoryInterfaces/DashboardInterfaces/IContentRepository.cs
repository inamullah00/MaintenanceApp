using Ardalis.Specification;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IContentRepository
    {
        // Get all content with optional specification for filtering and sorting
        Task<List<ContentResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Content>? specification = null);

        // Get content by its ID
        Task<Content?> GetByIdAsync(Guid ContentId, CancellationToken cancellationToken);

        // Create a new content
        Task<Content> CreateAsync(Content content, CancellationToken cancellationToken);

        // Update existing content
        Task<(bool, Content?)> UpdateAsync(Content content, CancellationToken cancellationToken);

        // Update specific fields of content by passing an array of fields to be updated
        Task<bool> UpdateFieldsAsync(Content content, string[] fieldsToUpdate, CancellationToken cancellationToken = default);

        // Remove content by entity
        Task<Content> RemoveAsync(Content content, CancellationToken cancellationToken);
    }
}
