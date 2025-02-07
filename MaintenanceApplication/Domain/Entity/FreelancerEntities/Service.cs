using Domain.Common;
using Domain.Entity.UserEntities;

namespace Maintenance.Domain.Entity.FreelancerEntities
{

    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserCreated { get; set; } // Indicates if a user added this service
        public bool IsApproved { get; set; } // Admin approval for dynamic services
        public string? ActionById { get; set; }


        public ApplicationUser? ActionBy { get; set; } // Tracks who created/approved the service
        public ICollection<FreelancerService> FreelancerServices { get; set; }
    }

}
