using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class PrivacyAndPolicyViewModel
    {
        [Required(ErrorMessage = "Privacy & Policy is required")]
        public string? Privacy { get; set; }
        [Required(ErrorMessage = "Privacy & Policy Arabic is required")]
        public string? PrivacyArabic { get; set; }
    }

    public class PrivacyAndPolicyResponseViewModel
    {
        public string? Privacy { get; set; }
    }
}
