using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto
{
    public class FeedbackResponseDto
    {
        public Guid Id { get; set; }                  // Feedback ID
        public Guid? OrderId { get; set; }            // Reference to the Order
        public Guid? FeedbackByClientId { get; set; } // Reference to the client
        public Guid? FeedbackByFreelancerId { get; set; } // Reference to the freelancer
        public int Rating { get; set; }               // Rating given (1-5 scale)
        public string? Comment { get; set; }          // Optional comments
        public DateTime CreatedAt { get; set; }       // When the feedback was created
        public DateTime UpdatedAt { get; set; }
    }
}
