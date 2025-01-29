using Domain.Enums;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserEntities
{
    public class ApplicationUser : IdentityUser
    {

        // Common fields
        public string? FullName { get; set; }
     
        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public UserStatus Status { get; set; } // Enum: Pending, Approved, Suspended, Rejected

        public ICollection<Dispute> Disputes { get; set; }
            
    }




    public class UserOtp
    {

        public Guid Id { get; set; }
        public UserOtp()
        {
            // Automatically calculate the expiration time (5 minutes from creation)
            ExpiresAt = CreatedAt.AddMinutes(5);
        }

        [Required]
        [MaxLength(6)]
        public string Otp { get; set; } // The OTP code

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the OTP was created

        [Required]
        public DateTime ExpiresAt { get; set; } // When the OTP will expire

        [Required]
        public bool IsUsed { get; set; } // Whether the OTP has been used
    }
}
































































//// Freelancer Related Fields

//public int? MonthlyLimit { get; set; } // Maximum number of orders the freelancer can complete in a month
//public int? OrdersCompleted { get; set; } // Number of orders completed by the freelancer in the specified month
//public decimal? TotalEarnings { get; set; } // Total earnings of the freelancer for the month
//public DateTime? ReportMonth { get; set; } // Specifies the month being tracked (typically the first day of the month)

//public double? CurrentRating { get; set; } // Average rating (e.g., 4.5 out of 5)


//// Navigation Properties
//public ICollection<Order> ClientOrders { get; set; } // Orders placed by the client
//public ICollection<Order> FreelancerOrders { get; set; } // Orders completed by the freelancer
//public ICollection<Bid> Bids { get; set; } // Bids placed by the freelancer
////public ICollection<Notification> Notifications { get; set; } // Notifications for the user
//public ICollection<Dispute> DisputesResolved { get; set; } // Disputes Resolved by the Admin
//public ICollection<OfferedService> OfferedServices { get; set; } // Services offered by the freelancer

//// Navigation properties for the feedback given by this user (Client or Freelancer)
//public ICollection<Feedback> FeedbackGivenByClient { get; set; } // Feedback given by this user as a Client
//public ICollection<Feedback> FeedbackGivenByFreelancer { get; set; } // Feedback given by this user as a Freelancer