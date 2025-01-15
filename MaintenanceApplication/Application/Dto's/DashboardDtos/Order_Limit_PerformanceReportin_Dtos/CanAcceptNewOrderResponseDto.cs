using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos
{
    public class CanAcceptNewOrderResponseDto
    {
        public bool CanAcceptNewOrder { get; set; }  // Indicates whether the freelancer can accept a new order.
        public int CurrentOrderCount { get; set; }   // The number of orders the freelancer is already handling.
        public int OrderLimit { get; set; }           // The order limit for the freelancer.
    }
}
