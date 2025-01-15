using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Order
    {

        public Guid Id { get; set; }
        public string? ClientId { get; set; } 
        public string? FreelancerId { get; set; } 
        public Guid? ServiceId { get; set; } // Foreign key for Service (like plumbing, electrical)

        // Order details
        public string Description { get; set; } // Description of the issue or service request
        public decimal Budget { get; set; } // Client's proposed budget for the service

        // Order status and timestamps
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // Current status of the order (Pending, In Progress, Completed, etc.)
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Date and time when the order was created

        public DateTime? CompletedDate { get; set; } // Date when the order was completed
        // Payment details
        public decimal TotalAmount { get; set; } // Total amount for the order, including freelancer's earnings and platform commission
        public decimal FreelancerAmount { get; set; } // Freelancer's earnings from the order after platform commission



        // Navigation Properties
        public ICollection<Feedback> Feedbacks { get; set; }  // Freelancer's feedback on orders

        [ForeignKey("FreelancerId")] 
        public ApplicationUser? Freelancer { get; set; }

        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }

        [ForeignKey("ServiceId")]
        public OfferedService Service { get; set; }
        public ICollection<Dispute> Disputes { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }



    // Enum for Order Status
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed, 
        Cancelled, 
        Disputed
    }
}
