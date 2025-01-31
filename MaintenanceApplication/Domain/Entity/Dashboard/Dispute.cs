using Domain.Common;
using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public class Dispute:BaseEntity
    {
        public Guid? OrderId { get; set; }
        public DisputeType DisputeType { get; set; } // Service, Quality, Payment
        public string DisputeDescription { get; set; }
        public DisputeStatus DisputeStatus { get; set; } // Pending, Resolved, Closed
        public string ResolutionDetails { get; set; }
        public string? ResolvedByAdminId { get; set; } // AdminId
        //public string CreatedBy { get; set; } // The ID or Username of the user who created the dispute
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ResolvedByAdminId))]
        public virtual ApplicationUser ResolvedByUser { get; set; }

    }

   
}
