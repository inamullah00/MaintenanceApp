using Ardalis.Specification;
using Maintenance.Domain.Entity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.FreelancerSpecification
{
    public class ClientSearchTable:Specification<OfferedService>
    {
        public ClientSearchTable(string keyword, int page, int pageSize)
        {
            //Query.Where(e => e.Name.Contains(keyword) || e.Description.Contains(keyword))
            //     .Skip((page - 1) * pageSize)
            //     .Take(pageSize);
        }
    }
}
