using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
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
                              ServicePreferredTime = service.PreferredTime,

                              Description = orders.Description,
                              Budget = orders.Budget,

                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,
                              UpdatedAt = orders.UpdatedAt.Value,

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
                              ServicePreferredTime = orders.Service.PreferredTime,

                              Description = orders.Description,
                              Budget = orders.Budget,

                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,
                              UpdatedAt = orders.UpdatedAt.Value,

                          }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> RemoveAsync(Order order, CancellationToken cancellationToken)
        {
              _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

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

        public async Task<List<OrderStatusResponseDto>> GetOrdersByStatusAsync(CancellationToken cancellationToken, ISpecification<Order>? specification)
        {

            var queryResult = SpecificationEvaluator.Default.GetQuery(
         query: _dbContext.Orders.AsQueryable(),
         specification: specification);


            return await (from orders in queryResult.AsSplitQuery()
                          join client in _dbContext.Clients.AsNoTracking()
                          on orders.ClientId equals client.Id
                          join service in _dbContext.OfferedServices.AsNoTracking()
                          on orders.ServiceId equals service.Id
                          select new OrderStatusResponseDto
                          {
                              ClientName = client.FullName,
                              ClientEmail = client.Email,
                              //ClientLocation = client.location,
                              ServiceTitle = service.Title,
                              ServiceDescription = service.Description,
                              //ServiceTime = service.PreferredTime,
                
                          })
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);

        }



        public async Task<List<PendingServicesResponseDto>> GetPendingClientServicesAsync(ISpecification<Bid> specification, CancellationToken cancellationToken)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                        query: _dbContext.Bids.AsQueryable().AsNoTracking(),
                        specification: specification
                    );

            return await (
                from bids in queryResult
                join offeredService in _dbContext.OfferedServices
                    on bids.OfferedServiceId equals offeredService.Id
                join serviceCategory in _dbContext.OfferedServiceCategories
                    on offeredService.CategoryID equals serviceCategory.Id
                select new PendingServicesResponseDto
                {
                    ServiceCategoryName = serviceCategory.CategoryName,
                    ServiceTitle = offeredService.Title,
                    ServiceDescription = offeredService.Description
                }
            ).AsNoTracking().ToListAsync(cancellationToken);

        }

        public async Task<List<ClientOrderStatusResponseDto>> GetClientOrdersByStatusAsync(ISpecification<Order> specification, CancellationToken cancellationToken)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                               query: _dbContext.Orders.AsQueryable(),
                               specification: specification);

            return await (from orders in queryResult.AsSplitQuery()
                          join freelancer in _dbContext.Freelancers.AsNoTracking()
                          on orders.FreelancerId equals freelancer.Id

                          join service in _dbContext.OfferedServices.AsNoTracking()
                          on orders.ServiceId equals service.Id

                          join category in _dbContext.OfferedServiceCategories.AsNoTracking()
                          on service.CategoryID equals category.Id

                          select new ClientOrderStatusResponseDto
                          {
                              FreelancerName = freelancer.FullName,
                              FreelancerEmail = freelancer.Email,
                              Rating = freelancer.ClientFeedbacks.Any() ? freelancer.ClientFeedbacks.Average(f => f.Rating).ToString("F1") : "0.0", // Handling rating calculation
                              ServiceTitle = service.Title,
                              ServiceCategory = category.CategoryName,
                              ServiceDescription = service.Description,
                              BookingDate = orders.CreatedAt, // Assuming CreatedAt is used for booking
                              ServiceTime = service.PreferredTime, // Using PreferredTime from service
                              BidPrice = orders.TotalAmount.ToString("F2"), // Formatting price properly
                              Image = service.ImageUrls != null && service.ImageUrls.Any() ? service.ImageUrls.FirstOrDefault() : null,
                              Video = service.VideoUrls != null && service.VideoUrls.Any() ? service.VideoUrls.FirstOrDefault() : null,
                              Audio = service.AudioUrls != null && service.AudioUrls.Any() ? service.AudioUrls.FirstOrDefault() : null
                          })
                          .AsNoTracking()
                          .ToListAsync(cancellationToken);
        }

        public async Task<List<OrderDateRangeFilterDto>> GetOrdersByDateRangeAsync(CancellationToken cancellationToken, ISpecification<Order>? specification)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                                    query: _dbContext.Orders.AsQueryable(),
                                    specification: specification);

            return await (from order in queryResult.AsSplitQuery()
                          join freelancer in _dbContext.Freelancers.AsNoTracking()
                          on order.FreelancerId equals freelancer.Id

                          join service in _dbContext.OfferedServices.AsNoTracking()
                          on order.ServiceId equals service.Id

                          join category in _dbContext.OfferedServiceCategories.AsNoTracking()
                          on service.CategoryID equals category.Id

                          select new OrderDateRangeFilterDto
                          {
                          
                              ServiceTitle = service.Title,
                              ServiceCategory = category.CategoryName,
                              ServiceDescription = service.Description,
                              //BookingDate = order.CreatedAt,
                          
                          })
                          .ToListAsync(cancellationToken);
        }
    }
}
