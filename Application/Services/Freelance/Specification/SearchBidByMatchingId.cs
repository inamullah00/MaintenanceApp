using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class SearchBidByMatchingId:Specification<Bid>
    {
        public SearchBidByMatchingId(Guid BidId)
        {
            if(BidId != Guid.Empty)
            {
                _ = Query.Where(x => x.Id == BidId);
            }
        }

        public SearchBidByMatchingId(string FreelancerId)
        {
            if (!string.IsNullOrWhiteSpace(FreelancerId))
            {
                _ = Query.Where(x => x.FreelancerId.ToString() == FreelancerId);
            }
        }
    }
}
