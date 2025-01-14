using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos
{
    public class FreelancerPerformanceReportUpdateDto
    {
        public int Month { get; set; }           // The month for which the performance report is requested.
        public int Year { get; set; }
    }
}
