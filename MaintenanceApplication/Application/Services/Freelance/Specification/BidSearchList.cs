using Ardalis.Specification;
using Maintenance.Domain.Entity.Freelancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class BidSearchList : Specification<Bid>
    {
        public BidSearchList(string? Keyword = "")
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
               _ = Query.Where(bid => bid.Freelancer.FirstName.Contains(Keyword) ||
                                bid.Freelancer.LastName.Contains(Keyword) ||
                                bid.Freelancer.Email.Contains(Keyword));
            }
            _ = Query.OrderBy(x => x.CreatedAt);
        }
    }
}
