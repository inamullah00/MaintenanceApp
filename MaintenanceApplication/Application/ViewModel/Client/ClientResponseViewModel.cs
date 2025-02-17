namespace Maintenance.Application.ViewModel
{
    public class ClientResponseViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string DialCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? CountryId { get; set; }
        public bool? IsActive { get; set; }

    }
}
