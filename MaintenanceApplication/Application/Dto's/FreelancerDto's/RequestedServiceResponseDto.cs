using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class RequestedServiceResponseDto
    {
        public string CoverLetter { get; set; }
        public string BidPrice { get; set; }
        public string FreelancerName { get; set; }
        public string TotalNoOfFreelancerApplied { get; set; }
        public string Title { get; set; } // OfferedService Title
        public string Description { get; set; } // OfferedService Description
        public string Address { get; set; } // OfferedService Address
        public DateTime ServiceTime { get; set; } // OfferedService Service Time
        public List<string> Images { get; set; } // OfferedService Service Media ( Images , Videos , Audios ) 
        public List<string> Videos { get; set; } // OfferedService Service Media ( Images , Videos , Audios ) 
        public List<string> Audios { get; set; } // OfferedService Service Media ( Images , Videos , Audios ) 
        public DateTime BookingDate { get; set; } // Bid Accepted Date
    }
}
