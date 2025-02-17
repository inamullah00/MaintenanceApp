using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class PackageCreateViewModel
    {
        [Required(ErrorMessage = "Package Name is required.")]
        [StringLength(255, ErrorMessage = "Package name contains max 255 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid price.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Package name contains max 500 characters")]
        public string? Offering { get; set; }

        public Guid? FreelancerId { get; set; }
    }
}
