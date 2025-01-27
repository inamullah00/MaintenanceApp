using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount
{
    public class CreateSignupRequestDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }  // Optional
        public string? AreaOfExpertise { get; set; }  // Freelancer's area of expertise (e.g., plumbing, cleaning)
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string CivilID { get; set; }
        public string ExperienceLevel { get; set; } // Level of experience (e.g., "Brand New", "Some Experience", "Expert")
        public string PreviousWork { get; set; } // Portfolio or links to previous work (Optional)
        public string Status { get; set; } // Account status (e.g., Pending, Active, Suspended)

    }
}
