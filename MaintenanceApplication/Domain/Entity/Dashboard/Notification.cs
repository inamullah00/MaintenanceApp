using Domain.Common;
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Notification:BaseEntity
    {
        public Guid? FreelancerId { get; set; } // Foreign key to User (nullable for global notifications)
        public string Message { get; set; } // Notification message
        public bool IsRead { get; set; } = false;// Whether the notification has been read

        [ForeignKey(nameof(FreelancerId))]
        public Freelancer Freelancer { get; set; }
    }
}
