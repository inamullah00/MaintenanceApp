using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }          // Reference to the Order
        public string? FeedbackByClientId { get; set; }  // Reference to the client (if applicable)
        public string? FeedbackOnFreelancerId { get; set; }  // Reference to the freelancer (if applicable)
        [Range(1,5)]
        public int Rating { get; set; }            // Rating given (1-5 scale, for example)
        public string Comment { get; set; }        // Optional comments from the client or freelancer
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;   // When the feedback was given
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;     // When the feedback was last updated

        public Order Order { get; set; }
        public ApplicationUser Client { get; set; }
        public ApplicationUser Freelancer { get; set; }
    }

}
