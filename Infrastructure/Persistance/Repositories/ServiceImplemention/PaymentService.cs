using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto;
using Maintenance.Application.Services.ClientPayment;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{

    public class PaymentService : IPaymentService
    {
        //private readonly IPaymentGateway _paymentGateway;
        //private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<PaymentService> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        //public PaymentService(IPaymentGateway paymentGateway, ITransactionRepository transactionRepository, ILogger<PaymentService> logger)
        //{
        //    //_paymentGateway = paymentGateway;
        //    //_transactionRepository = transactionRepository;
        //    _logger = logger;
        //}
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<PaymentResponseDto>> ProcessPaymentAsync(PaymentRequestDto paymentDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing payment for Order ID: {OrderId}", paymentDto.OrderId);

                // Trigger payment via gateway
                //var paymentResult = await _paymentGateway.ProcessPaymentAsync(paymentDto.Amount, paymentDto.PaymentMethod, cancellationToken);

                //if (!paymentResult.IsSuccess)
                //{
                //    _logger.LogWarning("Payment failed for Order ID: {OrderId}. Reason: {Message}", paymentDto.OrderId, paymentResult.Message);
                //    return Result<PaymentResponseDto>.Failure(paymentResult.Message, StatusCodes.Status400BadRequest);
                //}

                // Log the transaction
                //var transaction = new Transaction
                //{
                //    OrderId = paymentDto.OrderId,
                //    ClientId = paymentDto.ClientId,
                //    Amount = paymentDto.Amount,
                //    PaymentMethod = paymentDto.PaymentMethod,
                //    Status = "Completed",
                //    Description = paymentDto.Description,
                //    TransactionDate = DateTime.UtcNow
                //};

                //await _transactionRepository.AddAsync(transaction, cancellationToken);

                _logger.LogInformation("Payment successfully processed for Order ID: {OrderId}", paymentDto.OrderId);

                // Return response
                //var paymentResponseDto = new PaymentResponseDto
                //{
                //    TransactionId = transaction.Id,
                //    OrderId = transaction.OrderId,
                //    Amount = transaction.Amount,
                //    Status = transaction.Status
                //};
                return null;
                //return Result<PaymentResponseDto>.Success(paymentResponseDto, "Payment processed successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing payment for Order ID: {OrderId}", paymentDto.OrderId);
                return Result<PaymentResponseDto>.Failure("Internal Server Error: Unable to process payment.", StatusCodes.Status500InternalServerError);
            }
        }
    }


}
