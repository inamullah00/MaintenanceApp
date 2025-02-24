using Maintenance.Domain.Entity.FreelancerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class ServiceBidsDto
    {
        public string FullName { get; set; }

        //public string TopService { get; set; }
        public List<ServiceBidPackageDTO> FreelancerPackages { get; set; }
    }

    public class ServiceBidPackageDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string OfferDetails { get; set; }
    }
}
