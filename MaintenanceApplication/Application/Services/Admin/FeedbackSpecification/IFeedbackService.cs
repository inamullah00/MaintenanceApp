using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.FeedbackSpecification
{
    public interface IFeedbackService
    {
        // Get all feedback with optional keyword search
        Task<Result<List<FeedbackResponseDto>>> GetAllFeedbackAsync(CancellationToken cancellationToken, string keyword = "");

        // Get feedback by its ID
        Task<Result<FeedbackResponseDto>> GetFeedbackByIdAsync(Guid feedbackId, CancellationToken cancellationToken);

        // Create new feedback
        Task<Result<FeedbackResponseDto>> CreateFeedbackAsync(CreateFeedbackRequestDto createFeedbackRequestDto, CancellationToken cancellationToken);

        // Update existing feedback by its ID
        Task<Result<FeedbackResponseDto>> UpdateFeedbackAsync(Guid id, UpdateFeedbackRequestDto updateFeedbackRequestDto, CancellationToken cancellationToken);

        // Delete feedback by its ID
        Task<Result<string>> DeleteFeedbackAsync(Guid id, CancellationToken cancellationToken);
    }

}
