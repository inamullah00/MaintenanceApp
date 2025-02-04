using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount
{
    public class FreelancerEditProfileDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        //public string City { get; set; }
        //public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ExperienceLevel { get; set; }
        public string PreviousWork { get; set; }

    }
}
