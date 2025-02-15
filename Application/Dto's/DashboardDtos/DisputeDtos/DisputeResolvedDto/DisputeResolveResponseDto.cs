using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto
{
    public class DisputeResolveResponseDto
    {
        public Guid Id { get; set; } // The ID of the dispute
        public Guid OrderId { get; set; } // The ID of the related order
        public DisputeType DisputeType { get; set; } // Type of the dispute (Service, Quality, Payment)
        public string DisputeDescription { get; set; } // Description of the dispute
        public DisputeStatus DisputeStatus { get; set; } // Status of the dispute (Pending, Resolved, Closed)
        public string ResolutionDetails { get; set; } // Details about how the dispute was resolved
        public DateTime CreatedAt { get; set; } // Date when the dispute was created
        public DateTime? ResolvedAt { get; set; } // Date when the dispute was resolved
        public string ResolvedBy { get; set; } // The user who resolved the dispute
    }
}
