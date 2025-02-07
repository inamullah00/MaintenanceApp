using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.FreelancerAuth
{
    public interface IFreelancerAuthService
    {

        // Register a new freelancer account
       public Task<Result<Freelancer>> RegisterFreelancerAsync(FreelancerRegistrationDto registrationDto, CancellationToken cancellationToken);

        // Login a freelancer
        public Task<Result<FreelancerLoginResponseDto>> LoginAsync(FreelancerLoginDto loginDto, CancellationToken cancellationToken);

        // Logout method for Freelancer
        public Task<Result<bool>> LogoutAsync(Guid freelancerId, CancellationToken cancellationToken);

        // Update freelancer profile information
        public Task<Result<Freelancer>> UpdateProfileAsync(Guid freelancerId, FreelancerEditProfileDto EditProfileDto , CancellationToken cancellationToken);

        // Change freelancer password
        public Task<bool> ChangePasswordAsync(Guid freelancerId, string oldPassword, string newPassword);

        // Get freelancer profile by ID
        public Task<Result<FreelancerProfileDto>> GetFreelancerProfileAsync(Guid freelancerId,CancellationToken cancellationToken);
        public Task<Result<List<FreelancerProfileDto>>> GetFreelancersAsync(string Keyword);

        // Forgot password (send reset email)
        public Task<Result<string>> ForgotPasswordAsync(string email , CancellationToken cancellationToken);

        // Reset password using a reset token
        public Task<Result<bool>> ValidateOtpAsync (int OTP, CancellationToken cancellationToken);
        public Task<Result<bool>> ResetPasswordAsync(string resetToken, string newPassword , CancellationToken cancellationToken);        public Task<Result<bool>> BlockFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto updateDto, CancellationToken cancellationToken = default);
        public Task<Result<bool>> UnBlockFreelancerAsync(Guid freelancerId, FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken); // Unblock a freelancer
        public Task<Result<Freelancer>> ApproveFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto statusUpdateDto, CancellationToken cancellationToken);
    }
}
