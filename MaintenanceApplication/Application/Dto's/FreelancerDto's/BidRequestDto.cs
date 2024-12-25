using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class BidRequestDto
    {
        [Required]
        public Guid OfferedServiceId { get; set; } // ID of the service being bid on

        [Required]
        public Guid FreelancerId { get; set; } // ID of the freelancer making the bid

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero.")]
        public decimal BidAmount { get; set; } // The amount offered in the bid

    }
}
