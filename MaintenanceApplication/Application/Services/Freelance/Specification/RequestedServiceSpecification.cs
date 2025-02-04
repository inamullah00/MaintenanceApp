using Ardalis.Specification;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class RequestedServiceSpecification : Specification<OfferedService>
    {
        public RequestedServiceSpecification(string? keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                _ = Query.Where(s => s.Title.Contains(keyword) || s.Description.Contains(keyword));
            }
        }
    }
}
