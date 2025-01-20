using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Freelancer
{
    public class Bid
    {
        public Guid Id { get; set; }
        public decimal CustomPrice { get; set; }
        public BidStatus BidStatus { get; set; } = BidStatus.Pending;
        public Guid OfferedServiceId { get; set; }
        public string FreelancerId { get; set; }
        public double CurrentRating { get; set; } // Average rating (e.g., 4.5 out of 5)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ArrivalTime { get; set; } // Optional expected arrival time
        public string? Message { get; set; } 
        public DateTime? BidDate { get; set; } // Date of bid submission



        [ForeignKey(nameof(OfferedServiceId))]
        public OfferedService OfferedService { get; set; }

        [ForeignKey(nameof(FreelancerId))]
        public  ApplicationUser Freelancer { get; set; }
    }

}

public enum BidStatus
{
    Pending,
    Accepted,
    Rejected
}
