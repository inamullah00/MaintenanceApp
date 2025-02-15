using Ardalis.Specification;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.OrderSpecification.Specification
{
    public class ClientOrderStatusSpecification : Specification<Order>
    {
        public ClientOrderStatusSpecification(OrderStatus status)
        {
            if (Enum.IsDefined(typeof(OrderStatus), status))
            {
                Query.Where(order => order.Status == status);
            }
        }
    }
}
