﻿using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "Invalid email format")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Enter Phone Number with 10 to 15 digits.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string RoleId { get; set; }
    }
}
