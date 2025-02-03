using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class OrderStatusResponseDto
    {
        //Client Service Details
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientLocation { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime ServiceTime { get; set; }
        public string ServiceAddress { get; set; }
    }
}
