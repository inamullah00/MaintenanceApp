using Domain.Common;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.ClientEntities
{
    public class Client:BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Country { get; set; }
        public ICollection<Order> ClientOrders { get; set; } // Orders placed by the client
        public ICollection<Feedback> TotalProvidedFeedbacksByClient { get; set; } // Feedback given by Client


    }
}
