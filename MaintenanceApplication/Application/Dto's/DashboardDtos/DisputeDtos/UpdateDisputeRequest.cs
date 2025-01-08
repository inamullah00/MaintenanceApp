using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos
{
    public class UpdateDisputeRequest
    {
        public Guid OrderId { get; set; }
        public DisputeType DisputeType { get; set; } // Service, Quality, Payment
        public string DisputeDescription { get; set; }

    }
}
