using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FeedbackRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Feedback> CreateAsync(Feedback feedback, CancellationToken cancellationToken)
        {
            await _dbContext.Feedbacks.AddAsync(feedback, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<List<FeedbackResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Feedback>? specification = null)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                query: _dbContext.Feedbacks.AsQueryable(),
                specification: specification);

            return await (from feedback in queryResult.AsSplitQuery()
                          select new FeedbackResponseDto
                          {
                              Id = feedback.Id,
                              FeedbackByClientId = feedback.FeedbackByClientId,
                              Comment = feedback.Comment,
                              Rating = feedback.Rating,
                              CreatedAt = feedback.CreatedAt,
                              UpdatedAt = feedback.UpdatedAt.Value
                          })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Feedback?> GetByIdAsync(Guid feedbackId, CancellationToken cancellationToken)
        {
            return await (from feedback in _dbContext.Feedbacks.AsNoTracking()
                          where feedback.Id == feedbackId
                          select feedback)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Feedback> RemoveAsync(Feedback feedback, CancellationToken cancellationToken)
        {
            _dbContext.Feedbacks.Remove(feedback);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<(bool, Feedback?)> UpdateAsync(Feedback feedback, CancellationToken cancellationToken)
        {
            _dbContext.Feedbacks.Update(feedback);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return (true, feedback);
        }


    }
}
