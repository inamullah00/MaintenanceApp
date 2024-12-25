using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class BidResponseDto
    {
        //public Guid Id { get; set; } 

        //public Guid OfferedServiceId { get; set; } 

        //public Guid FreelancerId { get; set; }

        //public decimal BidAmount { get; set; }

        //public string Status { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid Id { get; set; } // Bid ID

        public Guid OfferedServiceId { get; set; } // Related OfferedService ID

        public string ServiceTitle { get; set; } // Title of the offered service

        public string FreelancerId { get; set; } // Freelancer making the bid

        public string FreelancerName { get; set; } // Name of the freelancer

        public decimal BidAmount { get; set; } // Bid amount

        public string Status { get; set; } // Bid status (e.g., Pending, Accepted, Rejected)

        public DateTime CreatedAt { get; set; } // Creation timestamp of the bid

        public string ClientId { get; set; } // ID of the client requesting the service

        public string CategoryName { get; set; } // Name of the service category

        public string Description { get; set; } // Description of the service

        public string Location { get; set; } // Location details of the service

        public DateTime? PreferredTime { get; set; } // Preferred time for service delivery

        public string Building { get; set; } // Building details

        public string Apartment { get; set; } // Apartment details

        public string Floor { get; set; } // Floor details

        public string Street { get; set; } // Street details

        public List<string> VideoUrls { get; set; } = new List<string>(); // URLs of associated videos

        public List<string> ImageUrls { get; set; } = new List<string>(); // URLs of associated images

        public List<string> AudioUrls { get; set; } = new List<string>(); // URLs of associated audio files

        //public DateTime? UpdatedAt { get; set; } // Timestamp for last update
    }
}
