using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class AboutUsViewModel
    {
        [Required(ErrorMessage = "AboutUs is required")]
        public string? AboutUs { get; set; }
        [Required(ErrorMessage = "AboutUs Arabic is required")]
        public string? AboutUsArabic { get; set; }
    }

    public class AboutUsResponseViewModel
    {
        public string? AboutUs { get; set; }
    }
}
