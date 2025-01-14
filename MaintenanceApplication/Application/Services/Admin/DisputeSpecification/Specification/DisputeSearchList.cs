using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Application.Services.Admin.DisputeSpecification.Specification
{
    public class ContentSearchList : Specification<Dispute>
    {
        public ContentSearchList(string? Keyword = "")
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                _ = Query.Where(x => x.ResolvedByUser.FirstName.Contains(Keyword) || x.ResolvedByUser.LastName.Contains(Keyword));
            }

            _ = Query.OrderBy(x => x.CreatedAt);
        }
    }
}
