﻿using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class BidSearchList : Specification<Bid>
    {
        public BidSearchList(Guid offeredServiceId)
        {

            if (offeredServiceId != Guid.Empty)
            {
                Query.Where(x => x.OfferedServiceId == offeredServiceId);
            }

        }
    }
}
