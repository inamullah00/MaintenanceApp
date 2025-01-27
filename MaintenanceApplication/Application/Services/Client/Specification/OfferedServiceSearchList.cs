using Ardalis.Specification;
using Maintenance.Domain.Entity.ClientEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Client.Specification
{
    public class OfferedServiceSearchList :Specification<OfferedService>
    {

        public OfferedServiceSearchList(string? Keyword = "")
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                _ = Query.Where(x => x.Title.Contains(Keyword));
            }
            
            _ = Query.OrderBy(x => x.Title);
        }
    }
}
