using Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.ClientPayment
{
    public interface IPaymentService
    {
        Task<Result<PaymentResponseDto>> ProcessPaymentAsync(PaymentRequestDto paymentDto, CancellationToken cancellationToken);
    }

}
