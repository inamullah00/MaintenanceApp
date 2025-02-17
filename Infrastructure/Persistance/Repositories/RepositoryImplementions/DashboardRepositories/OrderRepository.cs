using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Dto_s.ClientDto_s.ClientOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
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
                              ServiceTitle = service.Title,
                              ServiceDescription = service.Description,
                              Budget = orders.Budget,
                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,

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
                              ClientName = orders.Client.FullName,
                              ServiceTitle = orders.Service.Title,
                              ServiceDescription = orders.Service.Description,
                              Budget = orders.Budget,
                              Status = orders.Status,
                              CreatedAt = orders.CreatedAt,


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
                              ServiceAddress = service.Location
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

       public async Task<List<ClientOrderStatusResponseDto>> GetClientOrdersByStatusAsync(ISpecification<Order> specification , CancellationToken cancellationToken)
        {

            var queryResult = SpecificationEvaluator.Default.GetQuery(
        query: _dbContext.Orders.AsQueryable(),
        specification: specification);


            return await (from orders in queryResult.AsSplitQuery()
                          join freelancer in _dbContext.Freelancers.AsNoTracking()
                          on orders.FreelancerId equals freelancer.Id into freelancerGroup
                          from freelancer in freelancerGroup.DefaultIfEmpty() // Left Join

                          join service in _dbContext.OfferedServices.AsNoTracking()
                          on orders.ServiceId equals service.Id into serviceGroup
                          from service in serviceGroup.DefaultIfEmpty() // Left Join

                          join category in _dbContext.OfferedServiceCategories.AsNoTracking()
                          on service.CategoryID equals category.Id into categoryGroup
                          from category in categoryGroup.DefaultIfEmpty() // Left Join

                          select new ClientOrderStatusResponseDto
                          {
                              FreelancerName = freelancer != null ? freelancer.FullName : "N/A",
                              FreelancerEmail = freelancer != null ? freelancer.Email : "N/A",
                              Rating = freelancer.ClientFeedbacks.Any()? (int?)freelancer.ClientFeedbacks.Average(r => r.Rating) :0,
                              ServiceTitle = service != null ? service.Title : "N/A",
                              ServiceCategory = category != null ? category.CategoryName : "N/A",
                              ServiceDescription = service != null ? service.Description : "N/A",
                              ServiceAddress = service != null ? service.Location : "N/A",
                              BookingDate = orders.CreatedAt,  // Nullable DateTime
                              ServiceTime = service != null ? service.PreferredTime : null, // Nullable DateTime
                              Image = service.ImageUrls,
                              Audio = service.AudioUrls,
                              Video = service.VideoUrls
                          })
                          .AsNoTracking()
                          .ToListAsync(cancellationToken);




        }
    }
}
