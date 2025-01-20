using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
