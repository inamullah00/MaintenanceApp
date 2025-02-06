using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{

    public class FreelancerFilterSpecification : Specification<Bid>
    {
        public FreelancerFilterSpecification(decimal? minPrice, decimal? maxPrice, double? minRating, double? maxRating)
        {
            if (minPrice.HasValue)
            {
                _ = Query.Where(f => f.PackagePrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                _ = Query.Where(f => f.PackagePrice <= maxPrice.Value);
           
            }

        }
    }

}
