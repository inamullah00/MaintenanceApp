using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Application.Services.Admin.OrderSpecification.Specification
{
    public class PendingClientServiceSpecification : Specification<Bid>
    {
        public PendingClientServiceSpecification(BidStatus status)
        {
            if (status == BidStatus.Pending)
            {
                Query.Where(x => x.BidStatus == BidStatus.Pending);
            }
        }
    }
}
