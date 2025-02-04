using Domain.Common;
using Maintenance.Domain.Entity.FreelancerEntites;

namespace Maintenance.Domain.Entity.FreelancerEntities
{
    public class FreelancerService : BaseEntity
    {
        public Guid FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
