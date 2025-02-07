﻿using Domain.Entity.UserEntities;
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

        // Get all freelancers (optionally with filtering keyword)
        Task<List<FreelancerProfileDto>> GetFreelancersAsync(string keyword = null);

        // Update freelancer details
        Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer);

        // Delete freelancer (soft delete or permanent delete)
        Task<bool> DeleteFreelancerAsync(Guid freelancerId);

        // Check if a freelancer exists by ID
        Task<bool> FreelancerExistsAsync(Guid freelancerId);

        // Block a freelancer
        Task<bool> BlockFreelancerAsync(Guid freelancerId);

        // Unblock a freelancer
        Task<bool> UnblockFreelancerAsync(Guid freelancerId);

        // Approve a freelancer
        Task<bool> ApproveFreelancerAsync(Guid freelancerId);

        // Suspend a freelancer
        Task<bool> SuspendFreelancerAsync(Guid freelancerId);

        // Reactivate a suspended freelancer
        Task<bool> ReactivateFreelancerAsync(Guid freelancerId);

        Task<FreelancerOtp?> GetValidOtpAsync(int otp, CancellationToken cancellationToken);

        // Paginated list of freelancers
        Task<(List<Freelancer>, int)> GetFreelancersPaginatedAsync(int pageNumber, int pageSize);

    }
}
