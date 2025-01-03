using Ardalis.Specification;
using Maintenance.Domain.Entity.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class FreelancerBidSearchList:Specification<Bid>
    {
        public FreelancerBidSearchList(string FreelancerId)
        {
            if (!string.IsNullOrWhiteSpace(FreelancerId))
            {
                _ = Query.Where(x => x.FreelancerId == FreelancerId);
            }
        }
    }
}
