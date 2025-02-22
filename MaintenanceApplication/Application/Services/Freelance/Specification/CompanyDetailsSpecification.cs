using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class CompanyDetailsSpecification : Specification<Freelancer>
    {
        public CompanyDetailsSpecification(Guid CompanyId)
        {
            Query.Where(c => c.Id == CompanyId && c.IsType == UserType.Company)
                 .AsNoTracking();
        }
    }
}
