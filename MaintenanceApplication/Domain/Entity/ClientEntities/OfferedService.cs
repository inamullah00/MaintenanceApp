using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.Dashboard;

using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.ClientEntities
{
    public class OfferedService:BaseEntity
    {
        public Guid? ClientId { get; set; }
        public Guid? CategoryID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<string>? VideoUrls { get; set; }
        public List<string>? ImageUrls { get; set; } = new();
        public List<string>? AudioUrls { get; set; }

        public DateTime? PreferredTime { get; set; }

        public bool SetAsCurrentHomeAddress { get; set; } // it's Remaining i will refactor it 


        // Foreign Key for Location
        public Guid? ClientAddressId { get; set; }

        [ForeignKey(nameof(ClientAddressId))]
        public ClientAddress ClientAddress { get; set; } // Chosen location

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Order> Orders { get; set; }  // List of orders related to this service
        public ICollection<Bid> Bids { get; set; }  // List of Bids related to this service

        // Navigation Properties
        [ForeignKey("CategoryID")]
        public OfferedServiceCategory Category { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }

    public class OfferedServiceCategory:BaseEntity
    {
        public string CategoryName { get; set; }
       public bool IsActive { get; set; }
       public ICollection<OfferedService>? OfferedServices { get; set; }
    }
}
