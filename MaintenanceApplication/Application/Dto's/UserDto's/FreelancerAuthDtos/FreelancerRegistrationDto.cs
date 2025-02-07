using Microsoft.AspNetCore.Http;

namespace Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos
{
    public class FreelancerRegistrationDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }  // Optional
        public string City { get; set; }
        public string Address { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? CivilID { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public string? PreviousWork { get; set; } // Portfolio or links to previous work (Optional)
        public UserType IsType { get; set; }
        public Guid? CountryId { get; set; }

    }
}
