using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto
{
    public class CreateDisputeResolveDto
    {
        public string ResolutionDetails { get; set; } // Details about the resolution
        public string ResolvedBy { get; set; } // The ID or name of the user/admin who resolved the dispute
        public DisputeStatus DisputeStatus { get; set; }
    }
}
