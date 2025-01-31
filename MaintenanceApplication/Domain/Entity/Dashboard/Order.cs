
using Domain.Common;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Order:BaseEntity
    {
        public Guid? ClientId { get; set; } 
        public Guid? FreelancerId { get; set; } 
        public Guid? ServiceId { get; set; } // Foreign key for Service (like plumbing, electrical)

        // Order details
        public string Description { get; set; } // Description of the issue or service request
        public decimal Budget { get; set; } // Client's proposed budget for the service

        // Order status and timestamps
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // Current status of the order (Pending, In Progress, Completed, etc.)
        public DateTime? CompletedDate { get; set; } // Date when the order was completed
        // Payment details
        public decimal TotalAmount { get; set; } // Total amount for the order, including freelancer's earnings and platform commission
        public decimal FreelancerAmount { get; set; } // Freelancer's earnings from the order after platform commission

        public bool IsApproveByAdmin { get; set; } = false;

        // Navigation Properties
        public ICollection<Feedback> Feedbacks { get; set; }  // Freelancer's feedback on orders

        [ForeignKey("FreelancerId")] 
        public Freelancer Freelancers { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("ServiceId")]
        public OfferedService Service { get; set; }
        public ICollection<Dispute> Disputes { get; set; }
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
