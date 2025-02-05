using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s.ClientAuthDtos
{
    public class ClientRegistrationDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public Guid? CountryId { get; set; }
    }
}
