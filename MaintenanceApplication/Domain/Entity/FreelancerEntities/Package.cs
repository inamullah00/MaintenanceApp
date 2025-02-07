using Domain.Common;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntities
{
    public class Package : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string OfferDetails { get; set; }
        public Guid FreelancerId { get; set; }

        [ForeignKey(nameof(FreelancerId))]
        public Freelancer Freelancer { get; set; }

        // Many-to-Many Relationship: A package can be used in multiple bids
        public ICollection<BidPackage> BidPackages { get; set; }
    }

}
