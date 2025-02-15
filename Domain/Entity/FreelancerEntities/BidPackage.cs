using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntities
{

    public class BidPackage
    {
        public Guid BidId { get; set; }
        public Guid PackageId { get; set; }

        [ForeignKey(nameof(BidId))]
        public Bid Bid { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; }
    }

}
