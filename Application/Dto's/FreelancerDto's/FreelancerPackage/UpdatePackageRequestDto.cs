using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerPackage
{
    public class UpdatePackageRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string OfferDetails { get; set; }
        public Guid FreelancerId { get; set; }
        public ICollection<BidPackage> BidPackages { get; set; } = new List<BidPackage>();
    }
}
