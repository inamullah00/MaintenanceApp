using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos
{
    public class TopFreelancerDto
    {
        public string FreelancerId { get; set; }  // The unique identifier of the freelancer.
        public string FreelancerName { get; set; }  // Freelancer's full name.
        public int CompletedOrders { get; set; }  // Total number of orders completed by the freelancer.
        public decimal TotalEarnings { get; set; }  // Total earnings of the freelancer for the month.
    }
}
