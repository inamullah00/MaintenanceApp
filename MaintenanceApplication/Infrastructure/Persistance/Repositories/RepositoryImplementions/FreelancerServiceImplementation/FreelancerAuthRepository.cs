using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.UserEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Freelancer> GetFreelancerByIdAsync(Guid freelancerId , CancellationToken cancellationToken)
        {

            var freelancer = await _dbContext.Freelancers
                 .AsNoTracking()
                 .Where(f => f.Id == freelancerId)
                 //.Select(f => new FreelancerProfileDto
                 //{
                 //    Id = f.Id,
                 //    FullName = f.FullName,
                 //    Email = f.Email,
                 //    ProfilePicture = f.ProfilePicture,
                 //    AreaOfExpertise = f.AreaOfExpertise.ToString(),
                 //    Status = f.Status.ToString()
                 //})
                 .FirstOrDefaultAsync(cancellationToken);

            return freelancer;
        }

        public async Task<List<FreelancerProfileDto>> GetFreelancersAsync(string keyword = null)
        {
            var freelancers = await _dbContext.Freelancers
                  .AsNoTracking()
                  .Select(f => new FreelancerProfileDto
                  {
                      Id = f.Id,
                      FullName = f.FullName,
                      Email = f.Email,
                      ProfilePicture = f.ProfilePicture,
                      Status = f.Status.ToString()
                  })
                  .ToListAsync();

            return freelancers;
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

        public async Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer)
        {
      
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

        public async Task<FreelancerOtp?> GetValidOtpAsync(int otp, CancellationToken cancellationToken)
        {
            return await _dbContext.FreelancerOtps
                    .Where(o => o.OtpCode == otp && o.ExpiresAt > DateTime.UtcNow)
                    .FirstOrDefaultAsync(cancellationToken);

        }

        public async Task<FreelancerOtp> AddFreelancerOTP(FreelancerOtp otp)
        {
            await _dbContext.FreelancerOtps.AddAsync(otp);
            return otp;
        }

        public async Task<FreelancerOtp?> DeleteFreelancerOTP(Guid Id)
        {
           var entity = await _dbContext.FreelancerOtps.FirstOrDefaultAsync(f => f.Id == Id).ConfigureAwait(false);
             _dbContext.Remove(entity);
            return entity;
        }

        public async Task<FreelancerOtp?> GetFreelancerOTPByEmail(string Email)
        {
            return await _dbContext.FreelancerOtps.Where(f => f.Email == Email).FirstOrDefaultAsync();
        }
    }
}
