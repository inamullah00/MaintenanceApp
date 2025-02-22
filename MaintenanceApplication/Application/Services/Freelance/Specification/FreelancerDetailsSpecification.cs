using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Application.Services.Freelance.Specification
{

    public class FreelancerDetailsSpecification : Specification<Freelancer>
    {
        public FreelancerDetailsSpecification(Guid id)
        {
            Query.Where(f => f.Id == id)
                 .Include(f => f.ClientFeedbacks)
                 .AsNoTracking();
        }
    }

}
