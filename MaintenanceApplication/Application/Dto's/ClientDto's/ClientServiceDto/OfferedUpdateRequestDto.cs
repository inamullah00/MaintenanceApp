using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s
{
    public class OfferedUpdateRequestDto
    {

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } // Optional location details

        [MaxLength(500)]
        public string VideoUrl { get; set; } // URL for the video related to the service

        public List<IFormFile> ImageFiles { get; set; }

        [MaxLength(500)]
        public string VoiceUrl { get; set; } // URL for the voice recording related to the service

        public DateTime? PreferredTime { get; set; } // Preferred time for the service to be delivered

        public string Building { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Auto-assigned creation timestamp

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp for updates
    }
}
