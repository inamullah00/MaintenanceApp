using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class FreelancerDetailsDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string TopService { get; set; }
        public string Experience { get; set; }
        public double? Rating { get; set; }
        public string About { get; set; }

        public List<Feedback> Feedbacks { get; set; }
    }
}
