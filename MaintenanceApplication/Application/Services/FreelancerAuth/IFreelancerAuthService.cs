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
        Task<FreelancerLoginResponseDto> LoginAsync(FreelancerLoginDto loginDto);

        // Logout method for Freelancer
        Task<bool> LogoutAsync();

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
        Task<Result<FreelancerProfileDto>> FreelancerPaginatedAsync(int pageNumber, int pageSize); // Paginated list of freelancers
        Task<bool> BlockFreelancerAsync(Guid freelancerId); // Block a freelancer
        Task<bool> UnBlockFreelancerAsync(Guid freelancerId); // Unblock a freelancer

    }
}
