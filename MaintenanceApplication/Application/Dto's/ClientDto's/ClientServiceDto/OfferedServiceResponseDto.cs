using Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s
{
    public class OfferedServiceResponseDto
    {

        public Guid Id { get; set; } // Unique identifier for the service

        public string Title { get; set; } // Service title

        public string Description { get; set; } // Detailed description of the service

        public string ServiceTime { get; set; } // Estimated time to deliver the service

        public string Location { get; set; } // Service location details

        public List<string> VideoUrls { get; set; }
        public List<string> AudioUrls { get; set; }

        public List<string> ImageUrls { get; set; } = new(); // Store file paths/URLs


        public DateTime? PreferredTime { get; set; } // Client's preferred time for service delivery

        public DateTime CreatedAt { get; set; } // Date and time when the service was created

        public DateTime? UpdatedAt { get; set; } // Date and time of the last update

        public string CategoryName { get; set; } // Name of the category the service belongs to

        public string CategoryDescription { get; set; } // Description of the service category

        public string Building { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }

        public Guid ClientId { get; set; } // ID of the freelancer offering the service

        // Navigation Properties
        [ForeignKey("CategoryID")]
        public OfferedServiceCategory Category { get; set; } // Associated category for the service

        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; } // Associated freelancer
    }
}
