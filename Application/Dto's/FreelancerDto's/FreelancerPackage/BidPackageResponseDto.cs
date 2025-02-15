using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage
{
    public class BidPackageResponseDto
    {
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public decimal PackagePrice { get; set; }
    }
}
