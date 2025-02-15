using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos
{
    public class ClientOrderStatusResponseDto
    {
        public string FreelancerName { get; set; }
        public string FreelancerEmail { get; set; }
        public string Rating { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceCategory { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceAddress { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ServiceTime { get; set; }
        public string BidPrice { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Audio { get; set; }

    }
}
