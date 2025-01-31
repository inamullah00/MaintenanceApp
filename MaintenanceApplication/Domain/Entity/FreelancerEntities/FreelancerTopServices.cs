using Domain.Common;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntities
{
    public class FreelancerTopServices:BaseEntity
    {
        public Guid FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
