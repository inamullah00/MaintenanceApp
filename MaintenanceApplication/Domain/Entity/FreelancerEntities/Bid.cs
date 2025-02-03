using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
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
        public decimal Price { get; set; }
        public string Name  { get; set; }
        public string OfferDetails { get; set; }
        public BidStatus BidStatus { get; set; } = BidStatus.Pending;
        public Guid OfferedServiceId { get; set; }
        public Guid? FreelancerId { get; set; }
        public string CoverLetter { get; set; }


        [ForeignKey(nameof(OfferedServiceId))]
        public OfferedService OfferedService { get; set; }

        [ForeignKey(nameof(FreelancerId))]
        public  Freelancer Freelancer { get; set; }
    }

}

public enum BidStatus
{
    Pending,
    Accepted,
    Rejected
}
