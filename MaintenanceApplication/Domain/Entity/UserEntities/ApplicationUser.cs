﻿using Domain.Common;
using Domain.Enums;
using Maintenance.Domain.Entity.Client;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.Freelancer;
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
       
        public string? Location { get; set; } 
        public string? Address { get; set; }  

        // Freelancer Fields
        public string? ExpertiseArea { get; set; }  // Freelancer's area of expertise (e.g., plumbing, cleaning)
        public float? Rating { get; set; }  
        public string? Bio { get; set; }  
        public DateTime? ApprovedDate { get; set; } // When the freelancer was approved by the admin
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Optional Fields
        public string? Skills { get; set; }  // Freelancer's skill set (e.g., Plumbing, Electrical)
        public decimal? HourlyRate { get; set; }  // Freelancer's hourly or project rate
        public bool? IsVerified { get; set; }  // Whether the freelancer is verified by the admi
        public bool IsSuspended { get; set; }


        // Navigation Properties
        public ICollection<Order> ClientOrders { get; set; } // Orders placed by the client
        public ICollection<Order> FreelancerOrders { get; set; } // Orders completed by the freelancer
        public ICollection<Bid> Bids { get; set; } // Bids placed by the freelancer
        //public ICollection<Notification> Notifications { get; set; } // Notifications for the user
        //public ICollection<Dispute> Disputes { get; set; } // Disputes raised by the user
        //public ICollection<PerformanceReport> PerformanceReports { get; set; } // Performance reports for the freelancer
        public ICollection<OfferedService> OfferedServices { get; set; } // Services offered by the freelancer
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

