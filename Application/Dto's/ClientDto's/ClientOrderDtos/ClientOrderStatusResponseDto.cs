using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos
{
    public class ClientOrderStatusResponseDto
    {
        public string? FreelancerName { get; set; }
        public string? FreelancerEmail { get; set; }
        public int? Rating { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceCategory { get; set; }
        public string? ServiceDescription { get; set; }
        public string? ServiceAddress { get; set; }
        public DateTime? BookingDate { get; set; }  // Now Nullable
        public DateTime? ServiceTime { get; set; }  // Now Nullable
        public List<string>? Image { get; set; }
        public List<string>? Video { get; set; }
        public List<string>? Audio { get; set; }

    }
}
