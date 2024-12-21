using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation
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

        public async Task<List<OfferedService>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.OfferedServices.ToListAsync(cancellationToken);
        }

        public async Task<OfferedService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.OfferedServices.FirstOrDefaultAsync(x =>x.Id ==id, cancellationToken);
        }

        public async Task<IEnumerable<OfferedService>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.OfferedServices.ToListAsync(cancellationToken);
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
                return (false,null);
            }

            // Update the service's properties
            service.ClientId = entity.ClientId; // Set ClientId
            service.CategoryID = entity.CategoryID; // Set CategoryID
            service.Title = entity.Title; // Set Title
            service.Description = entity.Description; // Set Description
            service.Location = entity.Location; // Set Location
            service.VideoUrls = entity.VideoUrls; // Set VideoUrl
            service.ImageUrls = entity.ImageUrls; // Set ImageUrls
            service.AudioUrls = entity.AudioUrls; // Set VoiceUrl
            service.PreferredTime = entity.PreferredTime; // Set PreferredTime
            service.Building = entity.Building; // Set Building
            service.Apartment = entity.Apartment; // Set Apartment
            service.Floor = entity.Floor; // Set Floor
            service.Street = entity.Street; // Set Street
            service.SetAsCurrentHomeAddress = entity.SetAsCurrentHomeAddress; // Set SetAsCurrentHomeAddress
            service.UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt timestamp

            await _dbContext.SaveChangesAsync(cancellationToken);
            return (true, service);


        }
    }
}
