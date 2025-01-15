using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto
{
    public class UpdateFeedbackRequestDto
    {
        public Guid? OrderId { get; set; }            
        public string? FeedbackByClientId { get; set; } // Reference to the client
        public string? FeedbackOnFreelancerServiceId { get; set; } // Reference to the freelancer
        public int Rating { get; set; }               // Rating given (1-5 scale)
        public string? Comment { get; set; }
    }
}
