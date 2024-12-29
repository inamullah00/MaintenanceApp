using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Dispute
    {
        public Guid Id { get; set; } // Unique identifier for the dispute
        public Guid OrderId { get; set; } // Foreign key for the associated order
        public string RaisedByUserId { get; set; } // Foreign key for the user who raised the dispute

        public string Details { get; set; } // Detailed description of the dispute

        public DisputeStatus Status { get; set; } // Current status of the dispute

        public DateTime CreatedAt { get; set; } // Timestamp when the dispute was created
        public DateTime? ResolvedAt { get; set; } // Timestamp when the dispute was resolved (nullable)

        // Navigation Properties
        public Order Order { get; set; } // Navigation property for the associated order
        public ApplicationUser RaisedByUser { get; set; } // Navigation property for the user who raised the dispute
    }

    // Enum for Dispute Status
    public enum DisputeStatus
    {
        Open,       // Dispute has been raised but not yet acted upon
        InProgress, // Dispute is being resolved
        Resolved,   // Dispute has been resolved
        Rejected    // Dispute was reviewed and rejected
    }
}
