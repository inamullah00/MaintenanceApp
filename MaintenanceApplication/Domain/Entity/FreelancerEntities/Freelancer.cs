using Domain.Common;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntites
{
    public class Freelancer : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }  // Optional
        public string? AreaOfExpertise { get; set; }  // Freelancer's area of expertise (e.g., plumbing, cleaning)
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Country { get; set; }
        public string? CivilID { get; set; }
        public string? ExperienceLevel { get; set; } // Level of experience (e.g., "Brand New", "Some Experience", "Expert")
        public string? PreviousWork { get; set; } // Portfolio or links to previous work (Optional)
        public string Status { get; set; } // Account status (e.g., Pending, Active, Suspended)
        public string Role { get; set; }

        public ICollection<Bid> Bids { get; set; } // Bids placed by the freelancer

        public ICollection<Order> FreelancerOrders { get; set; } // Orders completed by the freelancer

        public ICollection<Feedback> ClientFeedbacks { get; set; } 

    }
}







































//public float? Rating { get; set; } = 0;
//public string? Experience { get; set; }
//public string? Skills { get; set; }  // Freelancer's skill set (e.g., Plumbing, Electrical)
//public decimal? HourlyRate { get; set; }  // Freelancer's hourly or project rate
//public bool? IsApprove { get; set; } = false; // Whether the freelancer is Approve by the admin
//public bool IsSuspended { get; set; } = false;
//public int? MonthlyLimit { get; set; } // Maximum number of orders the freelancer can complete in a month
//public int? OrdersCompleted { get; set; } // Number of orders completed by the freelancer in the specified month
//public decimal? TotalEarnings { get; set; } // Total earnings of the freelancer for the month
//public DateTime? ReportMonth { get; set; } // Specifies the month being tracked (typically the first day of the month)
//public double? CurrentRating { get; set; } // Average rating (e.g., 4.5 out of 5)
