using Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.UserEntities
{
    public class OfferedService
    {
        public Guid Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public Guid CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } 

        public List<string> VideoUrls { get; set; }
        public List<string> ImageUrls { get; set; } = new(); // Store file paths/URLs
        public List<string> AudioUrls { get; set; } 

        public DateTime? PreferredTime { get; set; } 



        // Additional Information


        public string Building { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }

        public bool SetAsCurrentHomeAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } 

        // Navigation Properties
        [ForeignKey("CategoryID")]
        public OfferedServiceCategory Category { get; set; }

        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; } 
    }

    public class OfferedServiceCategory
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
    }
}
