using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.FreelancerServiceImplementation
{
    public class FreelancerAuthRepository : IFreelancerAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FreelancerAuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Freelancer> AddFreelancerAsync(Freelancer freelancer)
        {
            await _dbContext.Freelancers.AddAsync(freelancer);
            await _dbContext.SaveChangesAsync();
            return freelancer;
        }

        public Task<bool> ApproveFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FreelancerExistsAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Freelancer?> GetFreelancerByIdAsync(Guid freelancerId , CancellationToken cancellationToken)
        {

            return await _dbContext.Freelancers
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);
                }

        public Task<List<Freelancer>> GetFreelancersAsync(string keyword = null)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Freelancer>, int)> GetFreelancersPaginatedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReactivateFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SuspendFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnblockFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Freelancer> UpdateFreelancerAsync (Freelancer freelancer)
        {
            if (freelancer == null)
            {
                throw new ArgumentNullException(nameof(freelancer), "Freelancer cannot be null");
            }

            _dbContext.Freelancers.Update(freelancer); // Update the freelancer in the DB
           await _dbContext.SaveChangesAsync(); // Save changes to the database
            return freelancer;
        }

        public async Task<Freelancer?> GetFreelancerByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Freelancers
          .AsNoTracking()
          .FirstOrDefaultAsync(f => f.Email == email, cancellationToken);
        }
    }
}
