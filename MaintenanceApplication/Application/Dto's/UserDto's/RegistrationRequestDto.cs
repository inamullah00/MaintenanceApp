using Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Application.Dto_s.UserDto_s
{
    public class RegistrationRequestDto
    {


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Role Role { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UserStatus? Status { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }

        public string? ExpertiseArea { get; set; }
        public string? Experience { get; set; }
        public string? Bio { get; set; }

        public string? Skills { get; set; }
        public decimal? HourlyRate { get; set; }

    }
}
