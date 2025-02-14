using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class ClientEditViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{8,15}$", ErrorMessage = "Enter Phone Number with 8 to 15 digits.")]
        public string PhoneNumber { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePictureFile { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? IsActive { get; set; }
    }
}
