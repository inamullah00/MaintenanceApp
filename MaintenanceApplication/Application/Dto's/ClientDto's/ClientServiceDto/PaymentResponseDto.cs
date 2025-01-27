using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto
{

    public class PaymentResponseDto
    {
        public Guid TransactionId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }
    }

}
