﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class CompanyEditViewModel
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
        [Required(ErrorMessage = "FreelancerService is required.")]
        public List<Guid> FreelancerServiceIds { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public IFormFile? ProfilePictureFile { get; set; }
        public string? ProfilePicture { get; set; }

        [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
        public string? Bio { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }
        public IFormFile? CivilId { get; set; }
        public string? CivilIdString { get; set; }

        [Required(ErrorMessage = "Experience Level is required.")]
        public ExperienceLevel ExperienceLevel { get; set; }

        [StringLength(1000, ErrorMessage = "Previous Work description cannot exceed 1000 characters.")]
        public string? Note { get; set; }  // Optional

        public string Status { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public Guid? CountryId { get; set; }
    }
}
