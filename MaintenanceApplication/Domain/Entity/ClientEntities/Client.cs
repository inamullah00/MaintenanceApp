using Domain.Common;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Domain.Entity.UserEntities;

namespace Maintenance.Domain.Entity.ClientEntities
{
    public class Client : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<Order> ClientOrders { get; set; } // Orders placed by the client
        public ICollection<Feedback> TotalProvidedFeedbacksByClient { get; set; } // Feedback given by Client
        public ICollection<ClientOtp> clientOtps { get; set; }


    }
}
