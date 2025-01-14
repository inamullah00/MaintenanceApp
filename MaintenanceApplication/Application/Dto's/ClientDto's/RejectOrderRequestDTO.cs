using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s
{
    public class RejectOrderRequestDTO
    {
        public OrderStatus OrderStatus { get; set; }

        //public string Description { get; set; }
    }
}
