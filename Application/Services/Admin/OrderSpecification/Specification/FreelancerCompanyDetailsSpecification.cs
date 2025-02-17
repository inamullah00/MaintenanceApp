using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.OrderSpecification.Specification
{
    public class FreelancerCompanyDetailsSpecification : Specification<Freelancer>
    {
        public FreelancerCompanyDetailsSpecification(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                {
                    _ = Query.Where(x => x.Id == Id);
                }

            }
        }
    }
}
