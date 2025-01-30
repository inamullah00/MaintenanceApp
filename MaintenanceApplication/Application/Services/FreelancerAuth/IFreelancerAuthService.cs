using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Task<Freelancer> UpdateProfileAsync(Guid freelancerId, FreelancerUpdateDto updateDto);

        // Change freelancer password
        Task<bool> ChangePasswordAsync(Guid freelancerId, string oldPassword, string newPassword);

        // Verify freelancer account (e.g., email verification or activation)
        Task<bool> VerifyAccountAsync(Guid freelancerId, string verificationCode);

        // Suspend freelancer account
        Task<bool> SuspendAccountAsync(Guid freelancerId);

        // Reactivate freelancer account
        Task<bool> ReactivateAccountAsync(Guid freelancerId);

        // Get freelancer profile by ID
        Task<FreelancerProfileDto> GetFreelancerProfileAsync(Guid freelancerId);
        Task<List<FreelancerProfileDto>> GetFreelancersAsync(string Keyword);

        // Forgot password (send reset email)
        Task<bool> ForgotPasswordAsync(string email);

        // Reset password using a reset token
        Task<bool> ResetPasswordAsync(string resetToken, string newPassword);
        Task<bool> FreelancerApprovalAsync(Guid freelancerId); // Approve a freelancer
        Task<Result<FreelancerProfileDto>> FreelancerPaginatedAsync(int pageNumber, int pageSize); // Paginated list of freelancer
        Task<Result<bool>> BlockFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto updateDto, CancellationToken cancellationToken = default);
        Task<Result<bool>> UnBlockFreelancerAsync(Guid freelancerId, FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken); // Unblock a freelancer

    }
}
