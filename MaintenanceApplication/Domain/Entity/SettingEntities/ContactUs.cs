using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Domain.Entity.SettingEntities
{
    public class ContactUs : BaseEntity
    {
        protected ContactUs()
        {

        }
        public ContactUs(string fullName, string phoneNumber, string email, string message)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Message = message;
            IsRead = false;
        }
        public string FullName { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Email { get; protected set; }
        public string Message { get; protected set; }
        public bool IsRead { get; protected set; }
        public Guid? FreelancerId { get; protected set; }
        public Guid? ClientId { get; protected set; }
        public string? MarkedAsReadByUser { get; protected set; }
        public ApplicationUser? MarkAsReadBy { get; protected set; }
        public Freelancer? Freelancer { get; protected set; }
        public Client? Client { get; protected set; }

        public void MarkAsRead(ApplicationUser user)
        {
            MarkAsReadBy = user;
            IsRead = true;
        }

    }
}
