using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.FreelancerDto_s
{
    public class FilteredFreelancerResponseDto
    {
        public Guid FreelancerId { get; set; }
        public string Name { get; set; }
        public decimal BidPrice { get; set; }
        public double Rating { get; set; }
        public string SkillSet { get; set; }
    }

}
