using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System.ComponentModel.DataAnnotations.Schema;

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
        public ApplicationUser? ActionBy { get; set; }
    }

}
