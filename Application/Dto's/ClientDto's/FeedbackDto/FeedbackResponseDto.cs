using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto
{
    public class FeedbackResponseDto
    {
        public Guid Id { get; set; }                  // Feedback ID

        public string ClientName { get; set; }    // Customer mean Client
        public string ServiceName { get; set; }    //  mean Client Posted Service ( OfferedService)

        public Guid OrderId { get; set; }          // Reference to the Order
        //public Guid? FeedbackByClientId { get; set; }  // Reference to the client (if applicable)
        //public Guid? FeedbackOnFreelancerId { get; set; }  // Reference to the freelancer (if applicable)
        [Range(1, 5)]
        public int Rating { get; set; }            // Rating given (1-5 scale, for example)
        public string Comment { get; set; }        // Optional comments from the client or freelancer

        public DateTime FeedbackDate { get; set; }       // When the feedback was created
    }
}
