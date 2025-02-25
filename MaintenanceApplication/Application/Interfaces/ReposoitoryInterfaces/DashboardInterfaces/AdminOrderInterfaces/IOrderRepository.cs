using Ardalis.Specification;
using Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces
{
    public interface IOrderRepository
    {
        public Task<List<OrderResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Order>? specification = null);
        public Task<List<OrderStatusResponseDto>> GetOrdersByStatusAsync(CancellationToken cancellationToken, ISpecification<Order>? specification = null);
        public Task<List<OrderDateRangeFilterDto>> GetOrdersByDateRangeAsync(CancellationToken cancellationToken, ISpecification<Order>? specification = null);
        public Task<OrderResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Order?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> CreateAsync(Order order, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken);
        public Task<bool> UpdateFieldsAsync(Order order, string[] fieldsToUpdate, CancellationToken cancellationToken = default);

        public Task<bool> RemoveAsync(Order order, CancellationToken cancellationToken);

        // InProgress ,Completed Order 
        public Task<List<PendingServicesResponseDto>> GetPendingClientServicesAsync(ISpecification<Bid> specification, CancellationToken cancellationToken);
        public Task<List<ClientOrderStatusResponseDto>> GetClientOrdersByStatusAsync(ISpecification<Order> specification, CancellationToken cancellationToken);


    }

}
