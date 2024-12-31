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
            public string ClientId { get; set; } // Client's ID
            public string FreelancerId { get; set; } // Freelancer ID

        public string ClientFirstName { get; set; } // Client's first name
            public string ClientLastName { get; set; } // Client's last name
            public string ClientLocation { get; set; } // Client's location

            public Guid ServiceId { get; set; } // Service ID
            public string ServiceTitle { get; set; } // Service title
            public string ServiceDescription { get; set; } // Service description
            public string ServiceLocation { get; set; } // Service location
            public DateTime? ServicePreferredTime { get; set; } // Service preferred time

            // Order details
            public string Description { get; set; } // Description of the service request
            public decimal Budget { get; set; } // Client's proposed budget

            // Order status and timestamps
            public OrderStatus Status { get; set; } // Current status of the order
            public DateTime CreatedAt { get; set; } // Date and time the order was created
            public DateTime UpdatedAt { get; set; } // Last updated time of the order

            // Payment details
            public decimal TotalAmount { get; set; } // Total amount for the order
            public decimal FreelancerAmount { get; set; } // Freelancer's earnings
        }
}
