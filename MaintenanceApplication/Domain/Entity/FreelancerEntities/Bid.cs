using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntites
{
    public class Bid : BaseEntity
    {
        public BidStatus BidStatus { get; set; } = BidStatus.Pending;
        public Guid OfferedServiceId { get; set; }
        public Guid? FreelancerId { get; set; }
   

        [ForeignKey(nameof(OfferedServiceId))]
        public OfferedService OfferedService { get; set; }

        [ForeignKey(nameof(FreelancerId))]
        public  Freelancer Freelancer { get; set; }


        // Many-to-Many Relationship: A bid can have multiple packages
        public ICollection<BidPackage> BidPackages { get; set; } = new List<BidPackage>();
    }

}

public enum BidStatus
{
    Pending,
    Accepted,
    Rejected
}
