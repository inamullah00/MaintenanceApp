using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContentRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Content> CreateAsync(Content content, CancellationToken cancellationToken)
        {

            await _dbContext.Contents.AddAsync(content, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return content;

        }

        public async Task<List<ContentResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Content>? specification = null)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Contents.AsQueryable(),
            specification: specification);


            return await (from content in queryResult.AsSplitQuery()
                          select new ContentResponseDto
                          {
                              Id = content.Id,
                              Title = content.Title,
                              Body = content.Body,
                              ContentType = content.ContentType.ToString(),
                              IsActive = content.IsActive,
                              CreatedAt = content.CreatedAt,
                              UpdatedAt = content.UpdatedAt
                          })
                  .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Content?> GetByIdAsync(Guid ContentId, CancellationToken cancellationToken)
        {
            return await (from content in _dbContext.Contents.AsNoTracking()
                          where content.Id == ContentId
                          select content)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Content> RemoveAsync(Content content, CancellationToken cancellationToken)
        {
            _dbContext.Contents.Remove(content);
            await _dbContext.SaveChangesAsync();
            return content;
        }

        public async Task<(bool,Content?)> UpdateAsync(Content content, CancellationToken cancellationToken)
        {
            _dbContext.Contents.Update(content);
            await _dbContext.SaveChangesAsync(cancellationToken);
          return (true, content);
        }

        public Task<bool> UpdateFieldsAsync(Content content, string[] fieldsToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
