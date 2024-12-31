using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.UserDto_s
{
    public class UserDetailsResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string ExpertiseArea { get; set; }
        public string Rating { get; set; }
        public string Bio { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Skills { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? IsVerified { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
