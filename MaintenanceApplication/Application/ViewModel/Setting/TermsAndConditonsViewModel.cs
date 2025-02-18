using System.ComponentModel.DataAnnotations;

namespace Maintenance.Application.ViewModel
{
    public class TermsAndConditonsViewModel
    {
        [Required(ErrorMessage = "Terms And Condition Is Required")]
        public string? TermsAndCondition { get; set; }
        [Required(ErrorMessage = "Terms And Condition (Arabic Is Required")]
        public string? TermsAndConditionArabic { get; set; }
    }
    public class TermsAndConditonsResponseViewModel
    {
        public string? TermsAndCondition { get; set; }
    }
}
