using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntities
{

    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // e.g., Plumbing, Electrician, etc.

        // Navigation property to the many-to-many relationship
        public ICollection<FreelancerTopServices> FreelancerTopServices { get; set; }
    }
}
