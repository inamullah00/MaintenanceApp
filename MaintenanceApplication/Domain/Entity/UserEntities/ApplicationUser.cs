using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.UserEntities
{
    public class ApplicationUser : IdentityUser
    {

        // Common fields
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
     
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UserStatus? Status { get; set; } // Enum: Pending, Approved, Suspended, Rejected
       


        // Client Fields (some fields are optional depending on requirements)
        public string? Location { get; set; }  // Client's service location
        public string? Address { get; set; }  // Client's address (optional)

        // Freelancer Fields
        public string? ExpertiseArea { get; set; }  // Freelancer's area of expertise (e.g., plumbing, cleaning)
        public float? Rating { get; set; }  // Freelancer's average rating
        public string? Bio { get; set; }  // Short bio for freelancer
        public DateTime? ApprovedDate { get; set; } // When the freelancer was approved by the admin
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Optional Fields
        public string? Skills { get; set; }  // Freelancer's skill set (e.g., Plumbing, Electrical)
        public decimal? HourlyRate { get; set; }  // Freelancer's hourly or project rate
        public bool? IsVerified { get; set; }  // Whether the freelancer is verified by the admin

   
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

