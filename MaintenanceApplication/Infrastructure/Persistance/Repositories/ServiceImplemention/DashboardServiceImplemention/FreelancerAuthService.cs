using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Enums;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class FreelancerAuthService : IFreelancerAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public FreelancerAuthService(IUnitOfWork unitOfWork , IMapper mapper , IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
        }
        public Task<bool> BlockFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(Guid freelancerId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FreelancerApprovalAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<FreelancerProfileDto>> FreelancerPaginatedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<FreelancerProfileDto> GetFreelancerProfileAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FreelancerProfileDto>> GetFreelancersAsync(string Keyword)
        {
            throw new NotImplementedException();
        }

        public Task<FreelancerLoginResponseDto> LoginAsync(FreelancerLoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReactivateAccountAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        #region Register Freelancer
        public async Task<Result<Freelancer>> RegisterFreelancerAsync(FreelancerRegistrationDto registrationDto, CancellationToken cancellationToken)
        {
            if (registrationDto == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);

            }
            // Check if freelancer with the same email already exists
            var existingFreelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByEmailAsync(registrationDto.Email).ConfigureAwait(false);
            if (existingFreelancer != null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.EmailAlreadyExists, StatusCodes.Status409Conflict);
            }

            // Map DTO to Freelancer entity
            var freelancer = _mapper.Map<Freelancer>(registrationDto);

            // Hash the password before saving
            freelancer.Password = _passwordService.HashPassword(registrationDto.Password);
            //freelancer.Status = UserStatus.Pending; // Default status for new freelancers


            // Save the freelancer to the database
            var freelancerCreated = await _unitOfWork.FreelancerAuthRepository.AddFreelancerAsync(freelancer).ConfigureAwait(false);

            if (freelancerCreated == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.FreelancerRegistrationFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<Freelancer>.Success(freelancer, SuccessMessages.UserRegisteredSuccessfully, StatusCodes.Status201Created);
        }
        #endregion


        public Task<bool> ResetPasswordAsync(string resetToken, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SuspendAccountAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnBlockFreelancerAsync(Guid freelancerId)
        {
            throw new NotImplementedException();
        }

        public Task<Freelancer> UpdateProfileAsync(Guid freelancerId, FreelancerUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyAccountAsync(Guid freelancerId, string verificationCode)
        {
            throw new NotImplementedException();
        }
    }
}
