using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel.Service
{
    public class ServiceCreateViewModel
    {
        [Required(ErrorMessage = "Service Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Service name should be between 3 and 30 characters")]
        public string FullName { get; set; }
    }
}
