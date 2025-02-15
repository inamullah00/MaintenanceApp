using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto
{
    public class UpdateDisputeResolveRequest
    {
        public DisputeStatus DisputeStatus { get; set; } // The new status of the dispute (e.g., Pending, Resolved, Closed)
        public string? ResolutionDetails { get; set; } // The updated resolution details, if any
        public string? DisputeDescription { get; set; }
    }
}
