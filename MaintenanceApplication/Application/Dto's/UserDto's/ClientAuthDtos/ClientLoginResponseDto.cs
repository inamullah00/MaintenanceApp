using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s.ClientAuthDtos
{
    public class ClientLoginResponseDto
    {
        public string Token { get; set; }
        public Guid ClientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
