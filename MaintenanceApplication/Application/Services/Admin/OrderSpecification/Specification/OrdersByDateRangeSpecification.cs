using Ardalis.Specification;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Application.Services.Admin.OrderSpecification.Specification
{
    public class OrdersByDateRangeSpecification : Specification<Order>
    {
        public OrdersByDateRangeSpecification(DateTime startDate, DateTime endDate)
        {
            Query.Where(order => order.CreatedAt >= startDate && order.CreatedAt <= endDate);
        }
    }

}
