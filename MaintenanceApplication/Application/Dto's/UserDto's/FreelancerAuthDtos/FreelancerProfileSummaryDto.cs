using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos
{
    public class FreelancerProfileSummaryDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string AreaOfExpertise { get; set; }
        public string ExperienceLevel { get; set; }
        //public decimal Rating { get; set; }
    }
}
