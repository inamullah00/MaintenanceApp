using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Enums;
using Maintenance.Application.Common;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Security;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.FreelancerAuth.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class FreelancerAuthService : IFreelancerAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public FreelancerAuthService(IUnitOfWork unitOfWork , IMapper mapper , IPasswordService passwordService , ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }
        #region Block Freelancer
        public async Task<Result<bool>> BlockFreelancerAsync(Guid freelancerId, FreelancerStatusUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            if (freelancerId == Guid.Empty)
            {
                return Result<bool>.Failure("Freelancer ID is required.", StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId, cancellationToken);

            if (freelancer == null)
            {
                return Result<bool>.Failure("Freelancer not found.", StatusCodes.Status404NotFound);
            }

            //if (freelancer.Status == "Blocked")
            //{
            //    return Result<bool>.Failure("Freelancer is already blocked.", StatusCodes.Status400BadRequest);
            //}

            // Update freelancer status to blocked
            //freelancer.Status = updateDto.Status ;
            //freelancer.BlockedAt = DateTime.UtcNow;

            var freelancerEntity = _mapper.Map<Freelancer>(freelancer);

            await _unitOfWork.FreelancerAuthRepository.UpdateFreelancerAsync(freelancerEntity);

            return Result<bool>.Success(true, "Freelancer has been successfully blocked.", StatusCodes.Status200OK);
        }
        #endregion


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

        #region Get Freelancer Profile
        public async Task<Result<FreelancerProfileDto>> GetFreelancerProfileAsync(Guid freelancerId , CancellationToken cancellationToken)
        {
            if (freelancerId == Guid.Empty)
            {
                return Result<FreelancerProfileDto>.Failure(ErrorMessages.InvalidFreelancerId, StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId, cancellationToken);

            if (freelancer == null)
            {
                return Result<FreelancerProfileDto>.Failure(ErrorMessages.FreelancerNotFound, StatusCodes.Status404NotFound);
            }

            var freelancerProfile = _mapper.Map<FreelancerProfileDto>(freelancer);

            return Result<FreelancerProfileDto>.Success(freelancerProfile, SuccessMessages.FreelancersFetchedSuccessfully, StatusCodes.Status200OK);
        }
        #endregion


        #region Get All Freelancers
        public async Task<Result<List<FreelancerProfileDto>>> GetFreelancersAsync(string keyword)
        {
            // Apply search filter
            FreelancerSearchSpecification specification = new(keyword);

            // Fetch freelancers from the repository
            var freelancers = await _unitOfWork.FreelancerAuthRepository.GetFreelancersAsync();

            return Result<List<FreelancerProfileDto>>.Success(freelancers, SuccessMessages.FreelancersFetchedSuccessfully, StatusCodes.Status200OK);
        }
        #endregion


        #region Login
        public async Task<Result<FreelancerLoginResponseDto>> LoginAsync(FreelancerLoginDto loginDto, CancellationToken cancellationToken)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return Result<FreelancerLoginResponseDto>.Failure("Email and Password are required.", StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByEmailAsync(loginDto.Email, cancellationToken);

            if (freelancer == null)
            {
                return Result<FreelancerLoginResponseDto>.Failure("Invalid credentials.", StatusCodes.Status401Unauthorized);
            }


            if (freelancer == null || !_passwordService.ValidatePassword(loginDto.Password,freelancer.Password))
            {
                return Result<FreelancerLoginResponseDto>.Failure("Invalid email or password.", StatusCodes.Status401Unauthorized);
            }

            var token = _tokenService.GenerateToken(freelancer);

            var response = new FreelancerLoginResponseDto
            {
                Token = token,
                FreelancerId = freelancer.Id,
                FullName = $"{freelancer.FullName}",
                Email = freelancer.Email,
                ProfilePicture = freelancer.ProfilePicture
            };
            return Result<FreelancerLoginResponseDto>.Success(response, "Login successful.", StatusCodes.Status200OK);
        }
        #endregion


        #region Logout
        public async Task<Result<bool>> LogoutAsync(Guid freelancerId, CancellationToken cancellationToken)
        {
            if (freelancerId!=Guid.Empty)
            {
                return Result<bool>.Failure("Freelancer ID is required.", StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId, cancellationToken);

            if (freelancer == null)
            {
                return Result<bool>.Failure("Freelancer not found.", StatusCodes.Status404NotFound);
            }

            // Remove refresh token from DB if stored
            //freelancer.RefreshTokenExpiryTime = null;

            var FreelancerEntity = _mapper.Map<Freelancer>(freelancer);

            await _unitOfWork.FreelancerAuthRepository.UpdateFreelancerAsync(FreelancerEntity);
           

            return Result<bool>.Success(true, "Logout successful.", StatusCodes.Status200OK);
        }
        #endregion

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
            var existingFreelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByEmailAsync(registrationDto.Email, cancellationToken).ConfigureAwait(false);
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

        public async Task<Result<bool>> UnBlockFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken)
        {

            if (freelancerId == Guid.Empty)
            {
                return Result<bool>.Failure("Freelancer ID is required.", StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId,cancellationToken);

            if (freelancer == null)
            {
                return Result<bool>.Failure("Freelancer not found.", StatusCodes.Status404NotFound);
            }

            //if (freelancer.Status == "Active")
            //{
            //    return Result<bool>.Failure("Freelancer is already an Active State.", StatusCodes.Status400BadRequest);
            //}

            // Update freelancer status to blocked
            //freelancer.Status = updateDto.Status;
            //freelancer.BlockedAt = DateTime.UtcNow;

            var FreelancerEntity = _mapper.Map<Freelancer>(freelancer);

            await _unitOfWork.FreelancerAuthRepository.UpdateFreelancerAsync(FreelancerEntity);

            return Result<bool>.Success(true, "Freelancer has been successfully Unblocked.", StatusCodes.Status200OK);
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
