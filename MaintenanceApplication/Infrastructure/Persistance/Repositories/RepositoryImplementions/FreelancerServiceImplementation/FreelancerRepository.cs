using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.FreelancerServiceImplementation
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
        public async Task<List<BidResponseDto>> GetAllAsync(CancellationToken cancellationToken = default, ISpecification<Bid>? specification = null)
        {
            var QueryResult = SpecificationEvaluator.Default.GetQuery(

                query: _applicationDbContext.Bids.AsQueryable(),
                specification: specification
                );

            var result = await (from bid in QueryResult
                                join service in _applicationDbContext.OfferedServices
                                    on bid.OfferedServiceId equals service.Id
                                join freelancer in _applicationDbContext.Freelancers
                                    on bid.FreelancerId equals freelancer.Id
                                join category in _applicationDbContext.OfferedServiceCategories
                                    on service.CategoryID equals category.Id
                                select new BidResponseDto
                                {
                                    Id = bid.Id,
                                    OfferedServiceId = bid.OfferedServiceId,
                                    FreelancerId = bid.FreelancerId,
                                    FreelancerName = freelancer.FullName,
                                    ServiceTitle = service.Title,
                                    Status = bid.BidStatus.ToString(),
                                    CreatedAt = bid.CreatedAt,
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
                          join Freelancer in _applicationDbContext.Freelancers
                          on Bid.FreelancerId equals Freelancer.Id

                          select new BidResponseDto
                          {
                              Id = Bid.Id,
                              OfferedServiceId = Bid.OfferedServiceId,
                              FreelancerId = Bid.FreelancerId,
                              Status = Bid.BidStatus.ToString(),
                              CreatedAt = Bid.CreatedAt,
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

            //existingEntity.PackagePrice = entity.PackagePrice;
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

            existingEntity.BidStatus = entity.BidStatus;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return (true, existingEntity);
        }

        public async Task<List<FilteredFreelancerResponseDto>> GetByFilterAsync(CancellationToken cancellationToken, ISpecification<Bid>? specification)
        {
            // Apply specification to filter the query
            var query = SpecificationEvaluator.Default.GetQuery(
                query: _applicationDbContext.Bids.AsNoTracking().AsQueryable(),
                specification: specification
            );

            // Query with joins and projections using query syntax
            var filteredQuery = await (from bid in query
                                       join service in _applicationDbContext.OfferedServices.AsNoTracking()
                                           on bid.OfferedServiceId equals service.Id
                                       join freelancer in _applicationDbContext.Freelancers.AsNoTracking()
                                           on bid.FreelancerId equals freelancer.Id
                                       join category in _applicationDbContext.OfferedServiceCategories.AsNoTracking()
                                           on service.CategoryID equals category.Id
                                       select new FilteredFreelancerResponseDto
                                       {
                                           FreelancerId = freelancer.Id,
                                           Name = freelancer.FullName,
                                           //Rating = (float)freelancer.Rating, // Assuming Rating is a property in Users table
                                           //SkillSet = freelancer.Skills // Assuming SkillSet is a property in Users table
                                       })
                .ToListAsync(cancellationToken);

            return filteredQuery;

        }

        public async Task<Package?> GetPackageByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Packages.FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);

        }

        public async Task<List<Package>> GetAllPackagesAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Packages.ToListAsync(cancellationToken);
        }

        public async Task<Package> CreatePackageAsync(Package package, CancellationToken cancellationToken)
        {
            if (package == null) throw new ArgumentNullException(nameof(package));

            await _applicationDbContext.Packages.AddAsync(package, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return package;
        }

        public async Task<Package> UpdatePackageAsync(Package package, CancellationToken cancellationToken)
        {
            if (package == null) throw new ArgumentNullException(nameof(package));

            _applicationDbContext.Packages.Update(package);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return package;
        }

        public async Task<Package> DeletePackageAsync(Package package, CancellationToken cancellationToken)
        {
         
            _applicationDbContext.Packages.Remove(package);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return package;
        }
        #endregion
    }
}
