using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Dto_s.ClientDto_s;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Infrastructure.Data;
using Maintenance.Domain.Entity.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation
{
    public class OfferedServiceCategoryRepository : IOfferedServiceCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferedServiceCategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OfferedServiceCategory> CreateAsync(OfferedServiceCategory entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.OfferedServiceCategories.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public Task<List<Guid>> CreateRangeAsync(List<OfferedServiceCategory> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<OfferedServiceCategory, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

     
        public async Task<List<OfferedServiceCategoryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            //var res =  await (from category in _dbContext.OfferedServiceCategories
            //              join service in _dbContext.OfferedServices
            //              on category.Id equals service.CategoryID
            //              select new OfferedServiceCategoryResponseDto
            //              {
            //                  Id = category.Id,
            //                  CategoryName = category.CategoryName,
            //                  IsActive = category.IsActive,
            //                  OfferedServices = category.OfferedServices.Select(s => new OfferedServiceResponseDto
            //                  {
            //                      Id = s.Id,
            //                      ClientId = s.ClientId,
            //                      Title = s.Title,
            //                      Description = s.Description,
            //                      Location = s.Location,
            //                      CreatedAt = s.CreatedAt,
            //                      UpdatedAt = s.UpdatedAt
            //                  }).ToList()
            //              }).ToListAsync(cancellationToken);

            //return res;

            var res = await (from category in _dbContext.OfferedServiceCategories
                             select new OfferedServiceCategoryResponseDto
                             {
                                 Id = category.Id,
                                 CategoryName = category.CategoryName,
                                 IsActive = category.IsActive,
                                 OfferedServices = _dbContext.OfferedServices
                                    .Where(s => s.CategoryID == category.Id)
                                    .Select(s => new OfferedServiceResponseDto
                                    {
                                        Id = s.Id,
                                        ClientId = s.ClientId,
                                        Title = s.Title,
                                        Description = s.Description,
                                        Location = s.Location,
                                        CreatedAt = s.CreatedAt,
                                        UpdatedAt = s.UpdatedAt
                                    }).ToList()
                             }).ToListAsync(cancellationToken);

            return res;

        }

        public async Task<OfferedServiceCategoryResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await(from category in _dbContext.OfferedServiceCategories
                         join service in _dbContext.OfferedServices
                         on category.Id equals service.CategoryID
                         select new OfferedServiceCategoryResponseDto
                         {
                             Id = category.Id,
                             CategoryName = category.CategoryName,
                             IsActive = category.IsActive,
                             OfferedServices = category.OfferedServices.Select(s => new OfferedServiceResponseDto
                             {
                                 Id = s.Id,
                                 ClientId = s.ClientId,
                                 Title = s.Title,
                                 Description = s.Description,
                                 Location = s.Location,
                                 CreatedAt = s.CreatedAt,
                                 UpdatedAt = s.UpdatedAt
                             }).ToList()
                         }).FirstOrDefaultAsync(cancellationToken);
        }

      

        public Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, OfferedServiceCategory? UpdatedCategory)> UpdateAsync(OfferedServiceCategory entity, Guid id, CancellationToken cancellationToken = default)
        {
           var serviceCategory = await _dbContext.OfferedServiceCategories.FirstOrDefaultAsync(x =>x.Id == id, cancellationToken);
            if (serviceCategory == null)
            {
                return (false, null);
            }
            serviceCategory.CategoryName = entity.CategoryName;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return (true, serviceCategory);

        }
    }
}
