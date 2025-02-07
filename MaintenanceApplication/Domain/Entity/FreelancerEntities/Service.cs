using Domain.Common;
using Domain.Entity.UserEntities;

namespace Maintenance.Domain.Entity.FreelancerEntities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsUserCreated { get; set; } // Indicates if a freelancer added this service
        public bool IsApproved { get; set; }
        public string? ActionById { get; set; }

        public ApplicationUser? ActionBy { get; set; }
        public ICollection<FreelancerService> FreelancerServices { get; set; }


        public void Activate()
        {
            IsActive = true;
        }
        public void MarkAsApproved()
        {
            IsApproved = true;
        }
        public void MarkAsCreatedByUser()
        {
            IsUserCreated = true;
        }
        public void MarkAsSystemCreated(ApplicationUser user)
        {
            IsUserCreated = false;
            ActionBy = user;

        }
        public void Deactivate()
        {
            IsActive = false;
        }
    }

}
