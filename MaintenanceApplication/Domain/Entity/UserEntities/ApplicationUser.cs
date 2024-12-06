using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public DateTime RegistrationDate { get; set; }

        // Optional Fields
        public string? Skills { get; set; }  // Freelancer's skill set (e.g., Plumbing, Electrical)
        public decimal? HourlyRate { get; set; }  // Freelancer's hourly or project rate
        public bool? IsVerified { get; set; }  // Whether the freelancer is verified by the admin

   
    }
}

