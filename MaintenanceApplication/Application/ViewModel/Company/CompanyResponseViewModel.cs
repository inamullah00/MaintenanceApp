namespace Maintenance.Application.ViewModel
{
    public class CompanyResponseViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string DialCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? CountryId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public AccountStatus Status { get; set; }
    }
}
