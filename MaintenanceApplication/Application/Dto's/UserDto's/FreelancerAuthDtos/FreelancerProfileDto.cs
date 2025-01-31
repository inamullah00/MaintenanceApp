using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos
{
    public class FreelancerProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string AreaOfExpertise { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string CivilID { get; set; }
        public string ExperienceLevel { get; set; }
        public string PreviousWork { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
