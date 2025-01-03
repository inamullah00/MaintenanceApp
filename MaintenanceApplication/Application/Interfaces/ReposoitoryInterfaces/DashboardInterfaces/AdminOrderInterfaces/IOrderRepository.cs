using Ardalis.Specification;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.Freelancer;
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
        public Task<OrderResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> CreateAsync(Order order, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken);
        public Task<bool> UpdateFieldsAsync(Order order, string[] fieldsToUpdate, CancellationToken cancellationToken = default);

        public Task<bool> RemoveAsync(Order order, CancellationToken cancellationToken);  
    }
}
