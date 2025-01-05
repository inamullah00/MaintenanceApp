using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.UserDto_s
{
    public class UserProfileEditDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Location { get; set; }

        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public Role? Role { get; set; }

        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        //public UserStatus? Status { get; set; }

        //public string? Address { get; set; }

        //public string? ExpertiseArea { get; set; }
        //public float? Rating { get; set; }
        //public string? Bio { get; set; }
        //public DateTime? ApprovedDate { get; set; }

        //public string? Skills { get; set; }
        //public decimal? HourlyRate { get; set; }
        //public bool? IsVerified { get; set; }
    }
}
