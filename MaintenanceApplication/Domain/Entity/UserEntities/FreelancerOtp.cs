using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.UserEntities
{
    public class FreelancerOtp
    {
        public FreelancerOtp()
        {
            ExpiresAt = CreatedAt.AddMinutes(5); // OTP expires 5 minutes after creation
        }
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; } // Email linked to OTP

        [Required]
        [MaxLength(6)]
        public string OtpCode { get; set; } // The generated OTP code

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When OTP was generated

        [Required]
        public DateTime ExpiresAt { get; set; } // When OTP expires

        [Required]
        public bool IsUsed { get; set; } = false; // Mark true after successful verification

        [Required]
        public Guid FreelancerId { get; set; } // The User ID linked to this OTP
        public Freelancer Freelancer { get; set; }
    }

}
