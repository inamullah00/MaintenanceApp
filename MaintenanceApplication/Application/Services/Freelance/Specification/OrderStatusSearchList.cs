using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Freelance.Specification
{
    public class OrderStatusSearchList : Specification<Order>
    {
        public OrderStatusSearchList(OrderStatus status)
        {
            if (Enum.IsDefined(typeof(OrderStatus), status)) // Ensure the value is a valid enum
            {
                Query.Where(order => order.Status == status);
            }
        }
    }
}
