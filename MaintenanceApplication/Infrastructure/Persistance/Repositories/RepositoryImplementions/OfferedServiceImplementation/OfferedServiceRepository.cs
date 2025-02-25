using Application.Dto_s.ClientDto_s;
using Application.Dto_s.UserDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Maintenance.Infrastructure.Persistance.Data;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Ardalis.Specification.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.OfferedServiceImplementation
{
    public class OfferedServiceRepository : IOfferedServiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferedServiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OfferedService> CreateAsync(OfferedService entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.OfferedServices.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<List<Guid>> CreateRangeAsync(List<OfferedService> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.OfferedServices.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            // Return the list of Ids of the newly added entities
            return entities.Select(e => e.Id).ToList();
        }

        public Task<bool> ExistsAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<OfferedService?> FindAsync(Expression<Func<OfferedService, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.OfferedServices.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<List<OfferedServiceResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            var offeredServices = await (from service in _dbContext.OfferedServices
                                         join user in _dbContext.Users
                                         on service.ClientId.ToString() equals user.Id
                                         join category in _dbContext.OfferedServiceCategories
                                         on service.CategoryID equals category.Id
                                         select new OfferedServiceResponseDto
                                         {
                                             Id = service.Id,
                                             ClientId = service.ClientId,
                                             Title = service.Title,
                                             Description = service.Description,
                                             PreferredTime = service.PreferredTime,
                                             SetAsCurrentHomeAddress = service.SetAsCurrentHomeAddress,
                                             CreatedAt = service.CreatedAt,
                                             UpdatedAt = service.UpdatedAt,

                                             // Map Category
                                             Category = new OfferedServiceCategoryResponseDto
                                             {
                                                 Id = category.Id,
                                                 CategoryName = category.CategoryName,
                                                 IsActive = category.IsActive
                                             },

                                             // Map Client
                                             //Client = new ApplicationUsersResponseDto
                                             //{
                                             //    Id = user.Id,
                                             //    FirstName = user.FullName,
 
                                             //}
                                         }).ToListAsync(cancellationToken);

            return offeredServices;
        }

        public async Task<OfferedServiceResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {

            var offeredServices = await (
                   from service in _dbContext.OfferedServices
                   join client in _dbContext.Clients
                       on service.ClientId equals client.Id
                   join category in _dbContext.OfferedServiceCategories
                       on service.CategoryID equals category.Id
                   where service.Id == id
               select new OfferedServiceResponseDto
               {
                   Id = service.Id,
                   ClientId = service.ClientId,
                   Title = service.Title,
                   Description = service.Description,
                   PreferredTime = service.PreferredTime,
                   SetAsCurrentHomeAddress = service.SetAsCurrentHomeAddress,
                   CreatedAt = service.CreatedAt,
                   UpdatedAt = service.UpdatedAt,
                   // Map Category
                   Category = new OfferedServiceCategoryResponseDto
                   {
                       Id = category.Id,
                       CategoryName = category.CategoryName,
                       IsActive = category.IsActive
                   },
                   // Map Client
                   //Client = new ClientResponseDto
                   //{
                   //    Id = client.Id,
                   //    FirstName = client.FullName,
                   //}
               }).FirstOrDefaultAsync(cancellationToken);

            return offeredServices;
        }

        public async Task<bool> RemoveAsync(OfferedService offeredService, CancellationToken cancellationToken = default)
        {

            _dbContext.OfferedServices.Remove(offeredService);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<(bool, OfferedService?)> UpdateAsync(OfferedService entity, Guid id, CancellationToken cancellationToken = default)
        {
            var service = await _dbContext.OfferedServices.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (service == null)
            {
                return (false, null);
            }

            // Update the service's properties
            service.ClientId = entity.ClientId;
            service.CategoryID = entity.CategoryID;
            service.Title = entity.Title;
            service.Description = entity.Description;
            service.VideoUrls = entity.VideoUrls;
            service.ImageUrls = entity.ImageUrls;
            service.AudioUrls = entity.AudioUrls;
            service.PreferredTime = entity.PreferredTime;
            service.SetAsCurrentHomeAddress = entity.SetAsCurrentHomeAddress;
            service.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return (true, service);
        }

       public async Task<List<RequestedServiceResponseDto>> GetRequestedServicesAsync(ISpecification<OfferedService> specification, CancellationToken cancellationToken)
        {
            var query = SpecificationEvaluator.Default.GetQuery(
                       _dbContext.OfferedServices.AsQueryable(), specification);

            var requestedServices = await (from service in query
                                           join bid in _dbContext.Bids on service.Id equals bid.OfferedServiceId
                                           join freelancer in _dbContext.Freelancers on bid.FreelancerId equals freelancer.Id
                                           where bid.BidStatus == BidStatus.Pending
                                           select new RequestedServiceResponseDto
                                           {
                                               FreelancerName = freelancer.FullName,
                                               TotalNoOfFreelancerApplied = _dbContext.Bids.Count(b => b.OfferedServiceId == service.Id).ToString(),
                                               Title = service.Title,
                                               Description = service.Description,
                                               ServiceTime = service.PreferredTime.Value,
                                               Images = service.ImageUrls,
                                               Videos = service.VideoUrls,
                                               Audios = service.AudioUrls,
                                               BookingDate = bid.CreatedAt
                                           }).ToListAsync(cancellationToken);

            return requestedServices;
        }

       public async Task<ClientAddress> SaveAddressAsync(ClientAddress clientAddress, CancellationToken cancellationToken)
        {
           await _dbContext.ClientAddresses.AddAsync(clientAddress);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return clientAddress;
        }

           public async Task<bool> DeleteSaveAddressAsync(ClientAddress clientAddress, CancellationToken cancellationToken)
            {
            if (clientAddress is null)
                return false;

            _dbContext.ClientAddresses.Remove(clientAddress);
           return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
           
            }

        public async Task<bool> UpdateSaveAddressAsync(ClientAddress clientAddress, CancellationToken cancellationToken)
        {
            _dbContext.ClientAddresses.Update(clientAddress);

            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }

        public async Task<List<ClientAddress>> GetSaveAddressesAsync(Guid clientId ,CancellationToken cancellationToken)
        {
            return await _dbContext.ClientAddresses
              .Where(address => address.ClientId == clientId)
              .ToListAsync(cancellationToken)
              .ConfigureAwait(false);
         }

        public async Task<ClientAddress?> GetSaveAddressByIdAsync(Guid AddressId, CancellationToken cancellationToken)
        {
            return await _dbContext.ClientAddresses
              .Where(address => address.Id == AddressId)
              .FirstOrDefaultAsync(cancellationToken)
              .ConfigureAwait(false);
        }

    }
}
