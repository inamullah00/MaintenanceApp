using Ardalis.Specification;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Application.Services.FreelancerAuth.Specification
{

    public class FreelancerSearchSpecification : Specification<Freelancer>
    {
        public FreelancerSearchSpecification(string? keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                Query.Where(f =>
                    f.FullName.Contains(keyword) ||
                    f.Email.Contains(keyword) ||
                    f.AreaOfExpertise.ToString().Contains(keyword) ||
                    f.Status.ToString().Contains(keyword)
                );
            }

            Query.AsNoTracking();
        }
    }
}
