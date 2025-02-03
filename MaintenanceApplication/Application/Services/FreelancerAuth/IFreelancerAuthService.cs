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
        Task<Result<Freelancer>> RegisterFreelancerAsync(FreelancerRegistrationDto registrationDto, CancellationToken cancellationToken);

        // Login a freelancer
        Task<Result<FreelancerLoginResponseDto>> LoginAsync(FreelancerLoginDto loginDto, CancellationToken cancellationToken);

        // Logout method for Freelancer
        Task<Result<bool>> LogoutAsync(Guid freelancerId, CancellationToken cancellationToken);

        // Update freelancer profile information
        Task<Result<Freelancer>> UpdateProfileAsync(Guid freelancerId, FreelancerEditProfileDto EditProfileDto , CancellationToken cancellationToken);

        // Change freelancer password
        Task<bool> ChangePasswordAsync(Guid freelancerId, string oldPassword, string newPassword);

        // Get freelancer profile by ID
        Task<Result<FreelancerProfileDto>> GetFreelancerProfileAsync(Guid freelancerId,CancellationToken cancellationToken);
        Task<Result<List<FreelancerProfileDto>>> GetFreelancersAsync(string Keyword);

        // Forgot password (send reset email)
        Task<Result<string>> ForgotPasswordAsync(string email , CancellationToken cancellationToken);

        // Reset password using a reset token
        Task<Result<bool>> ValidateOtpAsync (string OTP, CancellationToken cancellationToken);
        Task<Result<bool>> ResetPasswordAsync(string resetToken, string newPassword , CancellationToken cancellationToken);
        Task<Result<FreelancerProfileDto>> FreelancerPaginatedAsync(int pageNumber, int pageSize); // Paginated list of freelancer
        Task<Result<bool>> BlockFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto updateDto, CancellationToken cancellationToken = default);
        Task<Result<bool>> UnBlockFreelancerAsync(Guid freelancerId, FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken); // Unblock a freelancer
        Task<Result<Freelancer>> ApproveFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto statusUpdateDto, CancellationToken cancellationToken);
    }
}
