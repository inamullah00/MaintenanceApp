using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class DisputeRepository : IDisputeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DisputeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Create Dispute
        public async Task<Dispute> CreateAsync(Dispute dispute, CancellationToken cancellationToken)
        {
            await _dbContext.Disputes.AddAsync(dispute, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return dispute;
        }
        #endregion

        #region Get All Disputes
        public async Task<List<DisputeResponseDto>> GetAllAsync(CancellationToken cancellationToken, ISpecification<Dispute>? specification = null)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                query: _dbContext.Disputes.AsQueryable(),
                specification: specification
            );

            return await (from disputes in queryResult.AsSplitQuery()
                          join order in _dbContext.Orders.AsNoTracking()
                          on disputes.OrderId equals order.Id
                          join client in _dbContext.Users.AsNoTracking()
                          on order.ClientId equals client.Id

                          select new DisputeResponseDto
                          {
                              Id = disputes.Id,
                              OrderId = disputes.OrderId,
                              DisputeType = disputes.DisputeType,
                              DisputeDescription = disputes.DisputeDescription,
                              DisputeStatus = disputes.DisputeStatus,
                              CreatedAt = disputes.CreatedAt,
                              //ClientFirstName = client.FirstName,
                              //ClientLastName = client.LastName
                          })
                          .AsNoTracking()
                          .ToListAsync(cancellationToken);
        }
        #endregion

        #region Get Dispute by ID
        public async Task<DisputeResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await (from disputes in _dbContext.Disputes.AsNoTracking()
                          join order in _dbContext.Orders.AsNoTracking()
                          on disputes.OrderId equals order.Id
                          join client in _dbContext.Users.AsNoTracking()
                          on order.ClientId equals client.Id
                          where disputes.Id == id
                          select new DisputeResponseDto
                          {
                              Id = disputes.Id,
                              OrderId = disputes.OrderId,
                              DisputeType = disputes.DisputeType,
                              DisputeDescription = disputes.DisputeDescription,
                              DisputeStatus = disputes.DisputeStatus,
                              CreatedAt = disputes.CreatedAt,
                              //ClientFirstName = client.FirstName,
                              //ClientLastName = client.LastName
                          })
                          .AsNoTracking()
                          .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region Update Dispute Fields
        public async Task<bool> UpdateFieldsAsync(Dispute dispute, string[] fieldsToUpdate, CancellationToken cancellationToken)
        {
            var entry = _dbContext.Entry(dispute);

            foreach (var field in fieldsToUpdate)
            {
                entry.Property(field).IsModified = true;
            }

            var changes = await _dbContext.SaveChangesAsync(cancellationToken);
            return changes > 0;
        }
        #endregion

        #region Remove Dispute
        public async Task<Dispute> RemoveAsync(Dispute dispute, CancellationToken cancellationToken)
        {
             _dbContext.Disputes.Remove(dispute);
            await _dbContext.SaveChangesAsync();
            return dispute;
        }
        #endregion

        #region Update Dispute
        public async Task<Dispute> UpdateAsync(Dispute dispute, CancellationToken cancellationToken)
        {
            _dbContext.Disputes.Update(dispute);
            await _dbContext.SaveChangesAsync();
            return dispute;
        }
        #endregion
    }
}
