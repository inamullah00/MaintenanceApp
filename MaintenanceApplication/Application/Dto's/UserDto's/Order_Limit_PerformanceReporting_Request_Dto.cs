using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s
{
    public class Order_Limit_PerformanceReporting_Request_Dto
    {
        // Freelancer Fields
        public string? ExpertiseArea { get; set; }  // Freelancer's area of expertise (e.g., plumbing, cleaning)
        public float? Rating { get; set; } = 0;
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public DateTime? ApprovedDate { get; set; } = DateTime.UtcNow; // When the freelancer was approved by the admin
        public DateTime? RegistrationDate { get; set; } = DateTime.UtcNow;

        // Optional Fields
        public string? Skills { get; set; }  // Freelancer's skill set (e.g., Plumbing, Electrical)
        public decimal? HourlyRate { get; set; }  // Freelancer's hourly or project rate
        public bool? IsVerified { get; set; } = false; // Whether the freelancer is verified by the admin
        public bool IsSuspended { get; set; } = false;

        // Freelancer Related Fields

        public int? MonthlyLimit { get; set; } // Maximum number of orders the freelancer can complete in a month
        public int? OrdersCompleted { get; set; } // Number of orders completed by the freelancer in the specified month
        public decimal? TotalEarnings { get; set; } // Total earnings of the freelancer for the month
        public DateTime ReportMonth { get; set; } // Specifies the month being tracked (typically the first day of the month)
    }
}
