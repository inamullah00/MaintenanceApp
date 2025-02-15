using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class CompleteWorkDTORequest
    {
        public OrderStatus OrderStatus { get; set; }  // Any notes or remarks from the freelancer regarding the completion of the task.
        public DateTime CompletionDate { get; set; } // The date and time when the work is marked as completed
    }
}
