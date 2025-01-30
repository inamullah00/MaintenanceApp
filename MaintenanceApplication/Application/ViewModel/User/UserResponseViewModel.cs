namespace Maintenance.Application.ViewModel
{
    public class UserResponseViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public bool IsBlocked { get; set; }
    }
}
