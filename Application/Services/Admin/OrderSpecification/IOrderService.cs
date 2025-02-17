using Maintenance.Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Admin.OrderSpecification
{
    public interface IOrderService
    {
        public Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(CancellationToken cancellationToken, string Keyword = "");
        public Task<Result<OrderResponseDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Result<string>> AssignOrderAsync(Guid id, AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken);
        public Task<Result<OrderResponseDto>> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto, CancellationToken cancellationToken);
        public Task<Result<string>> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken);
        public Task<Result<string>> RejectOrderAsync(Guid id, RejectOrderRequestDTO RejectOrderRequest, CancellationToken cancellationToken);
        public Task<Result<string>> CompleteWorkAsync(Guid orderId, CompleteWorkDTORequest WorkDTORequest, CancellationToken cancellationToken);
        public Task<Result<string>> ApproveOrderAsync(Guid orderId, CancellationToken cancellationToken);




        // Client ( InProgress , Completed  Order ) & inProcess Services
        // Retrieves all client services that are pending (awaiting bid approval).

       public Task<Result<List<PendingServicesResponseDto>>> GetPendingClientServicesAsync(CancellationToken cancellationToken = default);

        // Retrieves all orders for the client, filtered by their status.
       public Task<Result<List<ClientOrderStatusResponseDto>>> GetClientOrdersByStatusAsync( OrderStatus status, CancellationToken cancellationToken = default);


    }
}
