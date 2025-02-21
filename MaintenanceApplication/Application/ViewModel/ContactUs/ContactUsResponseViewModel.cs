namespace Maintenance.Application.ViewModel
{
    public class ContactUsResponseViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string? ProfileImageLink { get; set; }
        public string ImageBaseUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
