using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Order order, CancellationToken cancellationToken)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Order>? specification = null)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Orders.AsQueryable(),
            specification: specification);


            return await (from orders in queryResult.AsSplitQuery()
                          join client in _dbContext.Clients.AsNoTracking()
                          on orders.ClientId equals client.Id
                          join service in _dbContext.OfferedServices.AsNoTracking()
                          on orders.ServiceId equals service.Id

                          select new OrderResponseDto
                          {
                              Id = orders.Id,
                              ClientId = orders.ClientId,

                              ServiceId = service.Id,
                              ServiceTitle = service.Title,
                              ServiceDescription = service.Description,
                              ServiceLocation = service.Location,
                              ServicePreferredTime = service.PreferredTime,

                              Description = orders.Description,
                              Budget = orders.Budget,

                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,
                              UpdatedAt = orders.UpdatedAt,

                              TotalAmount = orders.TotalAmount,
                              FreelancerAmount = orders.FreelancerAmount
                          })
               .AsNoTracking()
               .ToListAsync(cancellationToken);
        }

        public async Task<OrderResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await (from orders in _dbContext.Orders.AsNoTracking()
                          join client in _dbContext.Clients.AsNoTracking()
                            on orders.ClientId equals client.Id
                          join service in _dbContext.OfferedServices.AsNoTracking()
                            on orders.ServiceId equals service.Id
                          where orders.Id == id
                          select new OrderResponseDto
                          {
                              Id = orders.Id,
                              ClientId = orders.ClientId,
                              FreelancerId = orders.FreelancerId,
                              ClientFirstName = orders.Client.FullName,
                              ServiceId = orders.ServiceId,
                              ServiceTitle = orders.Service.Title,
                              ServiceDescription = orders.Service.Description,
                              ServiceLocation = orders.Service.Location,
                              ServicePreferredTime = orders.Service.PreferredTime,

                              Description = orders.Description,
                              Budget = orders.Budget,

                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,
                              UpdatedAt = orders.UpdatedAt,

                              TotalAmount = orders.TotalAmount,
                              FreelancerAmount = orders.FreelancerAmount
                          }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> RemoveAsync(Order order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFieldsAsync(Order order, string[] fieldsToUpdate, CancellationToken cancellationToken)
        {
            var entry = _dbContext.Entry(order);

            foreach (var field in fieldsToUpdate)
            {
                entry.Property(field).IsModified = true;
            }

            var changes = await _dbContext.SaveChangesAsync(cancellationToken);
            return changes > 0;
        }

        public async Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            // Update the order
            _dbContext.Orders.Update(order);

            // Save changes
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return true to indicate the update was successful
            return true;
        }

        public async Task<Order?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        {

            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken); ;
        }
    }
}
