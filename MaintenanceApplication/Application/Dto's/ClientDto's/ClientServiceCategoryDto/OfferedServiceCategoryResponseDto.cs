using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s.ClientServiceCategoryDto
{
    public class OfferedServiceCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OfferedServiceResponseDto>? OfferedServices { get; set; }

    }
}
