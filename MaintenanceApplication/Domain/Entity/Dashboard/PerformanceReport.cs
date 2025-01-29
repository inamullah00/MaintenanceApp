using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class PerformanceReport
    {
        public Guid Id { get; set; } // Unique identifier for the performance report
        public Guid? FreelancerId { get; set; } // Foreign key for the freelancer

        public int MonthlyLimit { get; set; } // Maximum number of orders the freelancer can complete in a month
        public int OrdersCompleted { get; set; } // Number of orders completed by the freelancer in the specified month
        public decimal TotalEarnings { get; set; } // Total earnings of the freelancer for the month

        public DateTime ReportMonth { get; set; } // Specifies the month being tracked (typically the first day of the month)

        // Navigation Properties
        public ApplicationUser Freelancer { get; set; } // Navigation property for the freelancer
    }
}
