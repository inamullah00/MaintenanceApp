using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{

    #region Enums
    public enum DisputeStatus
    {
        Open = 1,
        InProgress = 2,
        Resolved = 3,
        Rejected = 4
    }

    public enum DisputeType
    {
        Service = 1,
        Quality = 2,
        Payment = 3
    }

    #endregion

    public class Dispute
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public DisputeType DisputeType { get; set; } // Service, Quality, Payment
        public string DisputeDescription { get; set; }
        public DisputeStatus DisputeStatus { get; set; } // Pending, Resolved, Closed
        public string ResolutionDetails { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ResolvedAt { get; set; }
        public string? ResolvedBy { get; set; } // AdminId
        public string CreatedBy { get; set; } // The ID or Username of the user who created the dispute
        public virtual Order Order { get; set; }
        public virtual ApplicationUser ResolvedByUser { get; set; }

    }

   
}
