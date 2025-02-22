using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class CompanyDetailsDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string TopService { get; set; }
        public string Experience { get; set; }
        public double? Rating { get; set; }
        public string About { get; set; }

        public List<Feedback> Feedbacks { get; set; }
    }
}
