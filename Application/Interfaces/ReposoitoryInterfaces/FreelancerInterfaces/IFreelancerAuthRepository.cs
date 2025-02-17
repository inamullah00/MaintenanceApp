using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces
{
    public interface IFreelancerAuthRepository
    {
        // Add a new freelancer to the database
        Task<Freelancer> AddFreelancerAsync(Freelancer freelancer);

        // Add a freelancer OTP to the database
        Task<FreelancerOtp> AddFreelancerOTP(FreelancerOtp otp);
        Task<FreelancerOtp?> DeleteFreelancerOTP(Guid Id);
        Task<FreelancerOtp?> GetFreelancerOTPByEmail(string Email);

        // Get freelancer by ID
        Task<Freelancer> GetFreelancerByIdAsync(Guid freelancerId , CancellationToken cancellationToken);

        // Get Freelancer By Email 

       Task<Freelancer?> GetFreelancerByEmailAsync(string email , CancellationToken cancellationToken);


        // Update freelancer details
        Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer);

        // Check if a freelancer exists by ID
        Task<bool> FreelancerExistsAsync(Guid freelancerId);




        // ===================== OTP METHODS =====================

        // Add a freelancer OTP to the database
        Task<FreelancerOtp> AddFreelancerOTP(FreelancerOtp otp, CancellationToken cancellationToken);

        // Update freelancer OTP
        Task<FreelancerOtp> UpdateFreelancerOTP(FreelancerOtp otp, CancellationToken cancellationToken);

        // Delete freelancer OTP
        Task<FreelancerOtp?> DeleteFreelancerOTP(Guid id, CancellationToken cancellationToken);

        Task<FreelancerOtp?> GetValidOtpAsync(int otp, CancellationToken cancellationToken);
        Task<FreelancerOtp?> GetValidFreelancerOTPByEmail(string email, CancellationToken cancellationToken);
        Task<FreelancerOtp?> GetRecentlyExpiredOTP(string email, CancellationToken cancellationToken);



    }
}
