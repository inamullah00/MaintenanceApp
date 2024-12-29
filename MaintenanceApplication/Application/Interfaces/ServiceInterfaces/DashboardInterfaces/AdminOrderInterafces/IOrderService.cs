using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ServiceInterfaces.DashboardInterfaces.AdminOrderInterafces
{
    public interface IOrderService
    {
        public Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(CancellationToken cancellationToken);
        public Task<Result<OrderResponseDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Result<string>>  AssignOrderAsync(Guid id, AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken);
        public Task<Result<OrderResponseDto>> CreateOrderAsync(CreateOrderRequestDto createOrderRequestDto, CancellationToken cancellationToken);
        public Task<Result<string>>  UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken);
        public Task<Result<string>> ResolveDisputeAsync(Guid id, ResolveDisputeDto resolveDisputeDto, CancellationToken cancellationToken);
    }
}
