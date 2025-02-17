using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Domain.Entity.Dashboard;
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

       
        public Task<bool> FreelancerExistsAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Freelancer> GetFreelancerByIdAsync(Guid freelancerId , CancellationToken cancellationToken)
        {

            var freelancer = await _dbContext.Freelancers
                 .AsNoTracking()
                 .Where(f => f.Id == freelancerId)
                 .FirstOrDefaultAsync(cancellationToken);

            return freelancer;
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

        //Get Valid OTP 
        public async Task<FreelancerOtp?> GetValidFreelancerOTPByEmail(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.FreelancerOtps
                      .Where(otp => otp.Email == email && !otp.IsUsed && otp.ExpiresAt > DateTime.UtcNow)
                      .OrderByDescending(otp => otp.ExpiresAt) // Get the latest valid OTP
                      .FirstOrDefaultAsync(cancellationToken);
        }
        //Get Recently Expired OTP
        public async Task<FreelancerOtp?> GetRecentlyExpiredOTP(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.FreelancerOtps
               .Where(otp => otp.Email == email && otp.ExpiresAt < DateTime.UtcNow) // Only expired OTPs
               .OrderByDescending(otp => otp.ExpiresAt) // Get the most recently expired OTP
               .FirstOrDefaultAsync(cancellationToken);
        }

          // Add a freelancer OTP to the database
        public async Task<FreelancerOtp> AddFreelancerOTP(FreelancerOtp otp, CancellationToken cancellationToken)
        {
            await _dbContext.FreelancerOtps.AddAsync(otp, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return otp;
        }

        // Update freelancer OTP
        public async Task<FreelancerOtp> UpdateFreelancerOTP(FreelancerOtp otp, CancellationToken cancellationToken)
        {
             _dbContext.FreelancerOtps.Update(otp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return otp;
        }

        // Delete freelancer OTP
        public async Task<FreelancerOtp?> DeleteFreelancerOTP(Guid id, CancellationToken cancellationToken)
        {
            var otp = await _dbContext.FreelancerOtps.FindAsync(new object[] { id }, cancellationToken);
            if (otp == null) return null;

            _dbContext.FreelancerOtps.Remove(otp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return otp;
        }

    }
}   
