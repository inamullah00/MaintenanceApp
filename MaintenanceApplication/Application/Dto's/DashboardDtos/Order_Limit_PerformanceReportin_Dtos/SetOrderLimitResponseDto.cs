using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos
{
    public class SetOrderLimitResponseDto
    {
        public string FreelancerId { get; set; }  // The unique identifier of the freelancer.
        public int OrderLimit { get; set; }     // The order limit that was set.
        public string Message { get; set; }     // A success or failure message.
    }
}
