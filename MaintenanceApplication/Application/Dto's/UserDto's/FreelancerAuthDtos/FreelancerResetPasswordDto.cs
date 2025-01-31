using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos
{
    public class FreelancerResetPasswordDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
