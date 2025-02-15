using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.UserDto_s
{
    public class ResetPasswordRequestDto
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string OtpCode { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

    }
}
