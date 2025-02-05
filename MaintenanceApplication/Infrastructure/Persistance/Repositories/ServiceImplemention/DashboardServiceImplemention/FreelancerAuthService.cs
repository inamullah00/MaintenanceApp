using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Entity.UserEntities;
using Domain.Enums;
using Maintenance.Application.Common;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Common.Utility;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Security;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.FreelancerAuth.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class FreelancerAuthService : IFreelancerAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public FreelancerAuthService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService, ITokenService tokenService)
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

        public async Task<Result<string>> ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result<string>.Failure("Email cannot be empty.", StatusCodes.Status400BadRequest);
            }

            // Check if the freelancer exists in the system
            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByEmailAsync(email, cancellationToken);
            if (freelancer == null)
            {
                return Result<string>.Failure("Freelancer with this email does not exist.", StatusCodes.Status404NotFound);
            }

            // Generate a 6-digit numeric OTP
            var otp = Helper.GenerateNumericOtp(6);

            // Store OTP in the database
            //var freelancerOtp = new FreelancerOtp
            //{
            //    Id = Guid.NewGuid(),
            //    FreelancerId = freelancer.Id,
            //    Otp = otp,
            //    CreatedAt = DateTime.UtcNow,
            //    ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            //    IsUsed = false
            //};

            //await _unitOfWork.FreelancerOtpRepository.AddAsync(freelancerOtp);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send the OTP to the freelancer via email
            try
            {
                //await SendEmailAsync(
                //    email,
                //    "Your OTP Code",
                //    $"Your OTP for password reset is: <b>{otp}</b>. This code is valid for 5 minutes."
                //);

                return Result<string>.Success(otp, "OTP sent successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to send OTP: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }



        public Task<Result<FreelancerProfileDto>> FreelancerPaginatedAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        #region Get Freelancer Profile
        public async Task<Result<FreelancerProfileDto>> GetFreelancerProfileAsync(Guid freelancerId, CancellationToken cancellationToken)
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


            if (freelancer == null || !_passwordService.ValidatePassword(loginDto.Password, freelancer.Password))
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
            if (freelancerId != Guid.Empty)
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
            freelancer.Status = AccountStatus.Pending; // Default status for new freelancers


            // Handle Profile Picture Upload (Single File)
            if (registrationDto.ProfilePicture != null)
            {
                var (uploadSuccess, uploadedImageUrl, uploadMessage) = await ImageUploadAsync(registrationDto.ProfilePicture);

                if (!uploadSuccess)
                {
                    return Result<Freelancer>.Failure(uploadMessage, StatusCodes.Status400BadRequest);
                }

                // Store the uploaded image URL in the freelancer entity
                freelancer.ProfilePicture = uploadedImageUrl;
            }

            // Save the freelancer to the database
            var freelancerCreated = await _unitOfWork.FreelancerAuthRepository.AddFreelancerAsync(freelancer).ConfigureAwait(false);

            if (freelancerCreated == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.FreelancerRegistrationFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<Freelancer>.Success(freelancer, SuccessMessages.UserRegisteredSuccessfully, StatusCodes.Status201Created);
        }
        #endregion


        public async Task<Result<bool>> ResetPasswordAsync(string resetToken, string newPassword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(resetToken) || string.IsNullOrWhiteSpace(newPassword))
            {
                return Result<bool>.Failure("Reset token and new password are required.", StatusCodes.Status400BadRequest);
            }

            // Find the user by the reset token
            //var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByResetTokenAsync(resetToken, cancellationToken);

            //if (freelancer == null)
            //{
            //    return Result<bool>.Failure("Invalid or expired reset token.", StatusCodes.Status400BadRequest);
            //}

            //// Validate reset token expiration (assuming you have an expiration field)
            //if (freelancer.ResetTokenExpiry < DateTime.UtcNow)
            //{
            //    return Result<bool>.Failure("Reset token has expired.", StatusCodes.Status400BadRequest);
            //}

            //// Hash the new password
            //var hashedPassword = _passwordService.HashPassword(newPassword);

            //// Update the freelancer's password and reset token fields
            //freelancer.Password = hashedPassword;
            //freelancer.ResetToken = null; // Clear the token after reset
            //freelancer.ResetTokenExpiry = null;

            //// Save changes to the database
            //_unitOfWork.FreelancerAuthRepository.UpdateFreelancer(freelancer);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            //return Result<bool>.Success(true, "Password reset successful.", StatusCodes.Status200OK);
            return null;
        }

        

        public async Task<Result<bool>> UnBlockFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken)
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

        #region Update Freelancer Profile
        public async Task<Result<Freelancer>> UpdateProfileAsync(Guid freelancerId, FreelancerEditProfileDto EditProfileDto , CancellationToken cancellationToken)
        {

            if (freelancerId == Guid.Empty || EditProfileDto == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
            }

            var freelancer = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId, cancellationToken).ConfigureAwait(false);

            if (freelancer == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.FreelancerNotFound, StatusCodes.Status404NotFound);
            }

            // Update fields
            freelancer.FullName = EditProfileDto.FullName ?? freelancer.FullName;
            freelancer.Email = EditProfileDto.Email ?? freelancer.Email;
            freelancer.PhoneNumber = EditProfileDto.PhoneNumber ?? freelancer.PhoneNumber;
            freelancer.ProfilePicture = EditProfileDto.ProfilePicture ?? freelancer.ProfilePicture;
            freelancer.Bio = EditProfileDto.Bio ?? freelancer.Bio;
            freelancer.PreviousWork = EditProfileDto.PreviousWork ?? freelancer.PreviousWork;
            freelancer.UpdatedAt = DateTime.UtcNow; // Ensure timestamp is updated

            // Save changes
            var updatedFreelancer = await _unitOfWork.FreelancerAuthRepository.UpdateFreelancerAsync(freelancer).ConfigureAwait(false);

            if (updatedFreelancer == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.UpdateFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<Freelancer>.Success(updatedFreelancer, SuccessMessages.ProfileUpdatedSuccessfully, StatusCodes.Status200OK);


        }
        #endregion

        public async Task<Result<bool>> ValidateOtpAsync(string OTP, CancellationToken cancellationToken)
        {
            var existingOtp = await _unitOfWork.FreelancerAuthRepository.GetValidOtpAsync(OTP, cancellationToken);

            if (existingOtp == null)
            {
                return Result<bool>.Failure("Invalid or expired OTP.", StatusCodes.Status400BadRequest);
            }

            return Result<bool>.Success(true, "OTP is valid.", StatusCodes.Status200OK);
        }



        #region Approve Freelancer
        public async Task<Result<Freelancer>> ApproveFreelancerAsync(Guid freelancerId,FreelancerStatusUpdateDto statusUpdateDto, CancellationToken cancellationToken)
        {
            if (freelancerId == Guid.Empty)
            {
                return Result<Freelancer>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
            }

            var FreelancerEntity = await _unitOfWork.FreelancerAuthRepository.GetFreelancerByIdAsync(freelancerId, cancellationToken).ConfigureAwait(false);

            if (FreelancerEntity == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.FreelancerNotFound, StatusCodes.Status404NotFound);
            }

            if (FreelancerEntity.Status is AccountStatus.Active)
            {
                return Result<Freelancer>.Failure(ErrorMessages.AlreadyApproved, StatusCodes.Status409Conflict);
            }

            FreelancerEntity.Status = statusUpdateDto.Status;
            //freelancer.ApprovedAt = DateTime.UtcNow;

            var UpdatedFreelancer = await _unitOfWork.FreelancerAuthRepository.UpdateFreelancerAsync(FreelancerEntity).ConfigureAwait(false);

            if (UpdatedFreelancer == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.ApprovalFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<Freelancer>.Success(UpdatedFreelancer, SuccessMessages.SuccessfullyApproved, StatusCodes.Status200OK);
        }
        #endregion




        #region Image Upload
        private async Task<(bool Success, string ImageUrl, string Message)> ImageUploadAsync(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return (false, string.Empty, "No image uploaded.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            try
            {
                // Generate a unique file name
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var fullPath = Path.Combine(uploadPath, fileName);

                // Save the file to the server
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Return the saved file path
                return (true, $"/UploadedFiles/{fileName}", "File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return (false, string.Empty, $"An error occurred during file upload: {ex.Message}");
            }
        }
        #endregion



    }
}
