using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Infrastructure.Persistance.Data;
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

        public Task<Freelancer> GetFreelancerByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Freelancer> GetFreelancerByIdAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
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

        public Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer)
        {
            throw new NotImplementedException();
        }
    }
}
