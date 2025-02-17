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
        public Guid OfferedServiceId { get; set; }

        public Guid FreelancerId { get; set; }

        //public BidStatus BidStatus { get; set; } = BidStatus.Pending;

        public List<BidPackageRequestDto> BidPackages { get; set; }

    }



    public class BidPackageRequestDto
    {
        [Required]
        public Guid PackageId { get; set; }
    }
}
