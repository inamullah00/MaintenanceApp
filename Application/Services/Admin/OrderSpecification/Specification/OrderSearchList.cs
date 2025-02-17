using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.OrderSpecification.Specification
{
    public class OrderSearchList : Specification<Order>
    {

        public OrderSearchList(string? Keyword = "")
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                _ = Query.Where(x => x.Client.FullName.Contains(Keyword) ||
                                 x.Service.Title.Contains(Keyword) ||
                                 x.Service.Description.Contains(Keyword));
            }

            _ = Query.OrderBy(x => x.CreatedAt);
        }

        public OrderSearchList(Guid OrderId)
        {

            if (!(OrderId == Guid.Empty))
            {
                _ = Query.Where(x => x.Id == OrderId);
            }
        }

    }
}
