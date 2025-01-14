using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.DashboardDtos.ContentDtos
{
    public class CreateContentRequestDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ContentType { get; set; }
        public bool IsActive { get; set; }
    }

}
