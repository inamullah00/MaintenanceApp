using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Notification
    {
        public Guid Id { get; set; } // Unique Identifier for the notification
        public string? UserId { get; set; } // Foreign key to User (nullable for global notifications)
        public string Message { get; set; } // Notification message
        public bool IsRead { get; set; } // Whether the notification has been read
        public DateTime CreatedAt { get; set; } // Date and time when the notification was created

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
