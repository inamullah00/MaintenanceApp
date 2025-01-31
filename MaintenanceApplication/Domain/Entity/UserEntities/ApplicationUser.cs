using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserEntities
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }
        public ICollection<Dispute> Disputes { get; set; }
        public void UnBlockUser()
        {
            AccessFailedCount = 0;
            LockoutEnd = DateTime.Now.AddDays(-1);
        }

        public void BlockUser()
        {
            AccessFailedCount = 1000;
            LockoutEnd = DateTime.Now.AddYears(4);
        }
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