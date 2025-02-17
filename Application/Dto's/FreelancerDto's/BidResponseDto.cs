using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class BidResponseDto
    {

        public Guid Id { get; set; } // Bid ID

        public Guid? OfferedServiceId { get; set; } // Related OfferedService ID

        public string ServiceTitle { get; set; } // Title of the offered service

        public Guid? FreelancerId { get; set; } // Freelancer making the bid

        public string FreelancerName { get; set; } // Name of the freelancer

        public decimal BidAmount { get; set; } // Bid amount

        public string Status { get; set; } // Bid status (e.g., Pending, Accepted, Rejected)

        public DateTime CreatedAt { get; set; } // Creation timestamp of the bid

    }
}
