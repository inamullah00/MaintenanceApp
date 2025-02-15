using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos
{
    public class DisputeResponseDto
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public DisputeType DisputeType { get; set; }
        public string DisputeDescription { get; set; }
        public DisputeStatus DisputeStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
