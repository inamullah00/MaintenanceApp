using Maintenance.Domain.Entity.FreelancerEntities;
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
        public Guid OfferedServiceId { get; set; } // ID of the service being bid on
        public Guid FreelancerId { get; set; } // ID of the freelancer making the bid
        public List<BidPackageRequestDto> BidPackages { get; set; } = new List<BidPackageRequestDto>();

    }

    public class BidPackageRequestDto
    {
        [Required]
        public Guid PackageId { get; set; }
    }
}
