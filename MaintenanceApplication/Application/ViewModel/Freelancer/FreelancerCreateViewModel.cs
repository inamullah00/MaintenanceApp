using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class FreelancerCreateViewModel
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Confirm Password is required.")]

    [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Phone Number is required.")]
    [Phone(ErrorMessage = "Invalid Phone Number.")]
    [StringLength(15, ErrorMessage = "Phone Number cannot exceed 15 characters.")]
    public string PhoneNumber { get; set; }

    public IFormFile? ProfilePicture { get; set; }

    [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
    public string? Bio { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateTime DateOfBirth { get; set; }
    public IFormFile? CivilID { get; set; }

    [Required(ErrorMessage = "Experience Level is required.")]
    [EnumDataType(typeof(ExperienceLevel), ErrorMessage = "Invalid Experience Level.")]
    public ExperienceLevel? ExperienceLevel { get; set; }

    [StringLength(1000, ErrorMessage = "Previous Work description cannot exceed 1000 characters.")]
    public string? PreviousWork { get; set; }  // Optional

    [Required(ErrorMessage = "Account Status is required.")]
    [EnumDataType(typeof(AccountStatus), ErrorMessage = "Invalid Account Status.")]
    public AccountStatus Status { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public Guid? CountryId { get; set; }


}
