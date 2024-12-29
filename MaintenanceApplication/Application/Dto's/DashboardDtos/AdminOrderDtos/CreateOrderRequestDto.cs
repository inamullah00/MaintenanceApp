using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos
{
    public class CreateOrderRequestDto
    {
        public string ClientId { get; set; } // ID of the client creating the order
        public Guid ServiceId { get; set; } // ID of the service being requested
        public string Description { get; set; } // Details about the service request
        public decimal Budget { get; set; } // Proposed budget for the service
    }
}
