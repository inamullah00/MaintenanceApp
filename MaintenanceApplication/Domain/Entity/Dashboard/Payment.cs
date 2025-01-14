using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.Dashboard
{
    public class Payment
    {
        public Guid Id { get; set; } // Unique identifier for the payment
        public Guid OrderId { get; set; } // Foreign key for the associated order

        public decimal ClientPaymentAmount { get; set; } // Total amount paid by the client

        public decimal FreelancerEarning { get; set; } // Amount earned by the freelancer

        public decimal PlatformCommission { get; set; } // Commission earned by the platform

        public DateTime PaymentDate { get; set; } // Date when the payment was processed

        // Navigation Properties
        public Order Order { get; set; }
    }
}


//public class Payment
//{
//    public Guid Id { get; set; }
//    public Guid FreelancerId { get; set; }
//    public decimal Amount { get; set; }
//    public DateTime PaymentDate { get; set; }

//    // Navigation property
//    public ApplicationUser Freelancer { get; set; }
//}

