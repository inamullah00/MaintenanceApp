using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Infrastructure.Data;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.Client;
using Maintenance.Domain.Entity.Freelancer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.FreelancerServiceImplementation
{
    public class FreelancerRepository : IFreelancerRepository
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public FreelancerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #region CreateAsync
        public async Task<Bid> CreateAsync(Bid entity, CancellationToken cancellationToken = default)
        {
            await _applicationDbContext.Bids.AddAsync(entity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
        #endregion

        #region CreateRangeAsync
        public async Task<List<Guid>> CreateRangeAsync(List<Bid> entities, CancellationToken cancellationToken = default)
        {
            await _applicationDbContext.Bids.AddRangeAsync(entities, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return entities.Select(e => e.Id).ToList();
        }
        #endregion

        #region ExistsAsync
        public async Task<bool> ExistsAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Bids.AnyAsync(predicate, cancellationToken);
        }
        #endregion

        #region FindAsync
        public async Task<Bid?> FindAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Bids.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        #endregion

        #region GetAllAsync
        public async Task<List<BidResponseDto>> GetAllAsync(CancellationToken cancellationToken = default , ISpecification<Bid>? specification = null )
        {
            var QueryResult = SpecificationEvaluator.Default.GetQuery(

                query : _applicationDbContext.Bids.AsQueryable(),
                specification : specification
                );

            var result = await (from bid in QueryResult
                                join service in _applicationDbContext.OfferedServices
                                    on bid.OfferedServiceId equals service.Id
                                join freelancer in _applicationDbContext.Users
                                    on bid.FreelancerId equals freelancer.Id
                                join category in _applicationDbContext.OfferedServiceCategories
                                    on service.CategoryID equals category.Id
                                select new BidResponseDto
                                {
                                    Id = bid.Id,
                                    OfferedServiceId = bid.OfferedServiceId,
                                    FreelancerId = bid.FreelancerId,
                                    FreelancerName = freelancer.FirstName,
                                    ServiceTitle = service.Title,
                                    BidAmount = bid.BidAmount,
                                    Status = bid.Status,
                                    CreatedAt = bid.CreatedAt,
                                    ClientId = service.ClientId,
                                    CategoryName = category.CategoryName,
                                    Description = service.Description,
                                    Location = service.Location,
                                    PreferredTime = service.PreferredTime,
                                    Building = service.Building,
                                    Apartment = service.Apartment,
                                    Floor = service.Floor,
                                    Street = service.Street,
                                })
                    .ToListAsync(cancellationToken);

            return result;

        }
        #endregion

        #region GetByIdAsync
        public async Task<BidResponseDto?> GetByIdAsync(ISpecification<Bid> specification, CancellationToken cancellationToken = default)
        {

            var QueryResult = SpecificationEvaluator.Default.GetQuery(

              query: _applicationDbContext.Bids.AsQueryable(),
              specification: specification
              );

            return await (from Bid in QueryResult
                          join OfferedService in _applicationDbContext.OfferedServices
                          on Bid.OfferedServiceId equals OfferedService.Id
                          join Category in _applicationDbContext.OfferedServiceCategories
                            on OfferedService.CategoryID equals Category.Id
                          join Freelancer in _applicationDbContext.Users
                          on Bid.FreelancerId equals Freelancer.Id
      
                          select new BidResponseDto
                          {
                              Id = Bid.Id,
                              OfferedServiceId = Bid.OfferedServiceId,
                              FreelancerId = Bid.FreelancerId,
                              FreelancerName = Freelancer.FirstName,
                              ServiceTitle = OfferedService.Title,
                              BidAmount = Bid.BidAmount,
                              Status = Bid.Status,
                              CreatedAt = Bid.CreatedAt,
                              ClientId = OfferedService.ClientId,
                              CategoryName = Category.CategoryName,
                              Description = OfferedService.Description,
                              Location = OfferedService.Location,
                              PreferredTime = OfferedService.PreferredTime,
                              Building = OfferedService.Building,
                              Apartment = OfferedService.Apartment,
                              Floor = OfferedService.Floor,
                              Street = OfferedService.Street,
                          
                          }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region RemoveAsync
        public async Task<bool> RemoveAsync(Bid entity, CancellationToken cancellationToken = default)
        {
            _applicationDbContext.Bids.Remove(entity);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        #endregion

        #region UpdateAsync
        public async Task<(bool, Bid?)> UpdateAsync(Bid entity, Guid id, CancellationToken cancellationToken = default)
        {
            var existingEntity = await _applicationDbContext.Bids.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (existingEntity == null)
            {
                return (false, null);
            }

             existingEntity.BidAmount = entity.BidAmount;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return (true, existingEntity);
        }
        #endregion

        #region ApproveBidAsync
        public async Task<(bool, Bid?)> ApproveBidAsync(Bid entity, Guid id, CancellationToken cancellationToken)
        {
            var existingEntity = await _applicationDbContext.Bids.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (existingEntity == null)
            {
                return (false, null);
            }

            existingEntity.Status = entity.Status;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return (true, existingEntity);
        }
        #endregion
    }
}
