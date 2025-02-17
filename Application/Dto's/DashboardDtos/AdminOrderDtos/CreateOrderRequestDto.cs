using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos
{
    public class CreateOrderRequestDto
    {
        public Guid ClientId { get; set; } // ID of the client creating the order
        public Guid FreelancerId { get; set; }    // The Freelancer being hired (optional, depending on your business logic)
        public Guid ServiceId { get; set; } // ID of the service being requested
        public decimal Budget { get; set; } // Proposed budget for the service
    }
}
