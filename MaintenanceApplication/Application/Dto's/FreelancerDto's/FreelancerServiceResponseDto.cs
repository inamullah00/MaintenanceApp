using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.FreelancerDto_s
{
    public class FreelancerServiceResponseDto
    {
        public Guid ServiceID { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid CategoryID { get; set; }
        public string ServiceTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
