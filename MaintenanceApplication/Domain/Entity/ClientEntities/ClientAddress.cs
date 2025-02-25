using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.ClientEntities
{
    public class ClientAddress : BaseEntity
    {
        public string Title { get; set; }  // Home, Office, etc.
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; } // For map functionality
        public string Longitude { get; set; }

        public bool IsCurrent { get; set; } // Default Address?
        public Guid ClientId { get; set; } // Foreign Key
        public Client Client { get; set; }
    }

}
