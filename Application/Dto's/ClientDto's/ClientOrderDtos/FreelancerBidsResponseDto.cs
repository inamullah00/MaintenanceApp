using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos
{
    public class FreelancerBidsResponseDto
    {
        public string ProfileImage { get; set; }
        public string FreelancerName { get; set; }
        public string FreelancerService { get; set; }

        public ICollection<BidPackageResponseDto> BidPackages { get; set; }

        
    }
}
