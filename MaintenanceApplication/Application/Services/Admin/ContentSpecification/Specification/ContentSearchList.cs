using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.ContentSpecification.Specification
{
    public class ContentSearchList : Specification<Content>
    {
        public ContentSearchList(string Keyword ="")
        {

            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                _ = Query.Where(x => x.Title == Keyword);
            }
           _ = Query.OrderByDescending(x => x.Title);
        }

        
    }
}
