using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos
{
    public class OrderResponseDto
    {
            public Guid Id { get; set; } // Order ID
            public Guid? ClientId { get; set; } // Client's ID
            public Guid? FreelancerId { get; set; } // Freelancer ID

            public string ClientName { get; set; }
            public string ServiceTitle { get; set; } // Service title
            public string ServiceDescription { get; set; } // Service description

            // Order details
            public decimal Budget { get; set; } // Client's proposed budget

            // Order status and timestamps
            public OrderStatus Status { get; set; } // Current status of the order
            public DateTime CreatedAt { get; set; }  // Date and time the order was created


        }
}
