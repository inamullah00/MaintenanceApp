using Ardalis.Specification;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces
{
    public interface IFeedbackRepository
    {
        // Get all feedback with optional specification for filtering and sorting
        Task<List<FeedbackResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Feedback>? specification = null);

        // Get feedback by its ID
        Task<Feedback?> GetByIdAsync(Guid feedbackId, CancellationToken cancellationToken);
        Task<FeedbackResponseDto?> GetFeedbackRatingByIdAsync(Guid feedbackId, CancellationToken cancellationToken);

        // Create new feedback
        Task<Feedback> CreateAsync(Feedback feedback, CancellationToken cancellationToken);

        // Update existing feedback
        Task<(bool, Feedback?)> UpdateAsync(Feedback feedback, CancellationToken cancellationToken);

        // Remove feedback by entity
        Task<Feedback> RemoveAsync(Feedback feedback, CancellationToken cancellationToken);
    }

}
