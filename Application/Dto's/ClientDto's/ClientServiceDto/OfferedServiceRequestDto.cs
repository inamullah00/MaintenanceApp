using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s
{
    public class OfferedServiceRequestDto
    {

        public Guid? ClientId { get; set; }

        public Guid? CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; } // Optional location details

        public List<IFormFile> VideoFiles { get; set; } // URL for the video related to the service

        public List<IFormFile> ImageFiles { get; set; }


        public List<IFormFile> AudioFiles { get; set; } // URL for the voice recording related to the service

        public DateTime? PreferredTime { get; set; } // Preferred time for the service to be delivered

        public string Building { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Auto-assigned creation timestamp
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp for updates
    }
}
