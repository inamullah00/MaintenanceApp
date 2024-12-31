using Infrastructure.Data;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.Freelancer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.DashboardRepositories
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

        public async Task<List<OrderResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await (from orders in _dbContext.Orders
                   join client in _dbContext.Users
                     on orders.ClientId equals client.Id
                   join service in _dbContext.OfferedServices
                     on orders.ServiceId equals service.Id
                 select new OrderResponseDto
               {
                   Id = orders.Id,
                   ClientId = orders.ClientId,
                   ClientFirstName = orders.Client.FirstName, 
                   ClientLastName = orders.Client.LastName, 
                   ClientLocation = orders.Client.Location,

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
               }).ToListAsync(cancellationToken);
        }

        public async Task<OrderResponseDto?> GetByIdAsync(Guid id , CancellationToken cancellationToken)
        {
            return await(from orders in _dbContext.Orders
                         join client in _dbContext.Users
                           on orders.ClientId equals client.Id
                         join service in _dbContext.OfferedServices
                           on orders.ServiceId equals service.Id
                         where orders.Id == id
                         select new OrderResponseDto
                         {
                             Id = orders.Id,
                             ClientId = orders.ClientId,
                             FreelancerId = orders.FreelancerId,
                             ClientFirstName = orders.Client.FirstName,
                             ClientLastName = orders.Client.LastName,
                             ClientLocation = orders.Client.Location,

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
                         }).FirstOrDefaultAsync(cancellationToken);
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

        public Task<bool> UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      
    }
}
