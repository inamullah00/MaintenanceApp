using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos
{
    public class FreelancerPerformanceReportResponseDto
    {
        public Guid FreelancerId { get; set; }
        public string FreelancerName { get; set; }
        public int TotalOrders { get; set; }
        public int TotalOrdersCompleted { get; set; }
        public decimal TotalEarnings { get; set; }
        public double AverageRating { get; set; }
        public int CompletedOrders { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PendingOrders { get; set; }
    }
}
