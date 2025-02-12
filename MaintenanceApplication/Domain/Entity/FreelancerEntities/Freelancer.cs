using Domain.Common;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Domain.Entity.UserEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maintenance.Domain.Entity.FreelancerEntites
{
    public class Freelancer : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }  // Optional
        public string? Bio { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public UserType IsType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string? CivilID { get; set; }
        public string? CompanyLicense { get; set; }
        public string? PreviousWork { get; set; } // Portfolio or links to previous work (Optional)
        public string? Note { get; set; } // note for freelancer by admin
        public AccountStatus Status { get; set; }  // Account status (e.g., Pending, Active, Suspended)
        public Guid? CountryId { get; set; }

        public ICollection<Bid> Bids { get; set; } // Bids placed by the freelancer
        public ICollection<Package> Packages { get; set; } // Bids placed by the freelancer

        public ICollection<Order> FreelancerOrders { get; set; } // Orders completed by the freelancer

        public ICollection<Feedback> ClientFeedbacks { get; set; }
        public ICollection<FreelancerOtp> FreelancerOtps { get; set; }

        // Many-to-many relationship with Service (Freelancer selects multiple services)
        public ICollection<FreelancerService> FreelancerServices { get; set; } = new List<FreelancerService>();

        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }


        //public void AddFreelancerServices(Service service)
        //{
        //    FreelancerServices.Add(new FreelancerService(this, service));
        //}
        public void MarkAsApproved()
        {
            Status = AccountStatus.Approved;
        }
        public void MarkAsSuspended()
        {
            Status = AccountStatus.Suspended;
        }
    }
}



public enum AccountStatus
{
    Pending = 1,       // Account is pending verification
    Suspended = 2,     // Account is suspended due to policy violations
    Approved = 3,
    Active = 4,
    Inactive = 5 // Account is approved and can be used
}

public enum UserType
{
    Freelancer = 1,
    Client = 2,
    Company = 3

}

public enum ExperienceLevel
{
    New = 1,
    Experienced = 2,
    Expert = 3,
}


