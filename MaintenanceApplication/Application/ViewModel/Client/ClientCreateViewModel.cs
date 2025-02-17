using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class ClientCreateViewModel
    {
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

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]

        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
