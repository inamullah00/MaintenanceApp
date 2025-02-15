using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Feedback:BaseEntity
    {
        public Guid OrderId { get; set; }          // Reference to the Order
        public Guid? FeedbackByClientId { get; set; }  // Reference to the client (if applicable)
        public Guid? FeedbackOnFreelancerId { get; set; }  // Reference to the freelancer (if applicable)
        [Range(1,5)]
        public int Rating { get; set; }            // Rating given (1-5 scale, for example)
        public string Comment { get; set; }        // Optional comments from the client or freelancer

        public Order Order { get; set; }
        [ForeignKey(nameof(FeedbackByClientId))]
        public Client Client { get; set; }
        [ForeignKey(nameof(FeedbackOnFreelancerId))]
        public Freelancer FeedbackOnFreelancer { get; set; }
    
    
    }

}
