using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Domain.Entity.FreelancerEntities
{
    public class Country :BaseEntity
    {

        public string Name { get; set; } // Country name, e.g., "United States", "Canada", etc.
        public string CountryCode { get; set; } // Optional: to store country codes (like 'US', 'CA', etc.)
        public string FlagCode { get; set; }

        public string DialCode { get; set; }
    }
}
