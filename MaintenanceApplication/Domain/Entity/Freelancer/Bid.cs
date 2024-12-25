using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Freelancer
{
    public class Bid
    {
        public Guid Id { get; set; }
        public decimal BidAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
        public Guid OfferedServiceId { get; set; }
        public string FreelancerId { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(OfferedServiceId))]
        OfferedService OfferedService { get; set; }

        [ForeignKey(nameof(FreelancerId))]
        ApplicationUser Freelancer { get; set; }
    }

}

//public string ApprovedBy { get; set; }