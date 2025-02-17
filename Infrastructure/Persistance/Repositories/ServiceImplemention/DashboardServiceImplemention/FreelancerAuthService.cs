using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Entity.UserEntities;
using Domain.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Maintenance.Application.Common;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Common.Utility;
using Maintenance.Application.Dto_s.AdminUser;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Security;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.FreelancerAuth.Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class FreelancerAuthService : IFreelancerAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public FreelancerAuthService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService,
            ITokenService tokenService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

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


            // Check if an OTP already exists for this email
            var existingOtp = await _unitOfWork.FreelancerAuthRepository.GetFreelancerOTPByEmail(email);

            // Generate a new OTP
            var otpCode = Helper.GenerateNumericOtp(6);

            if (existingOtp != null)
            {
                // Update existing OTP
                existingOtp.OtpCode = int.Parse(otpCode);
                existingOtp.ExpiresAt = DateTime.UtcNow.AddMinutes(5);
                existingOtp.CreatedAt = DateTime.UtcNow;
                existingOtp.IsUsed = false;
            }
            else
            {
                // Create new OTP record
                var otp = new FreelancerOtp
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    OtpCode = int.Parse(otpCode),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                    CreatedAt = DateTime.UtcNow,
                    IsUsed = false,
                    FreelancerId = freelancer.Id
                };


                await _unitOfWork.FreelancerAuthRepository.AddFreelancerOTP(otp);

            }
            try
            {

                await _unitOfWork.SaveChangesAsync(); // Ensure changes are saved

                // Send OTP via email
                await SendEmailAsync(email, otpCode);

                return Result<string>.Success("OTP sent successfully. Please check your email.", StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to send OTP: {ex.Message}", StatusCodes.Status500InternalServerError);
            }

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
            var CreatedFreelancer = await _unitOfWork.FreelancerAuthRepository.AddFreelancerAsync(freelancer).ConfigureAwait(false);

            if (CreatedFreelancer == null)
            {
                return Result<Freelancer>.Failure(ErrorMessages.FreelancerRegistrationFailed, StatusCodes.Status500InternalServerError);
            }

            // Generate and store OTP
            var otpCode = Helper.GenerateNumericOtp(6);
            var otp = new FreelancerOtp
            {
                Id = Guid.NewGuid(),
                Email = registrationDto.Email,
                OtpCode = int.Parse(otpCode),
                ExpiresAt = DateTime.UtcNow.AddMinutes(5), // Use UTC for consistency
                CreatedAt = DateTime.UtcNow,
                IsUsed = false,
                FreelancerId = CreatedFreelancer.Id
            };

            await _unitOfWork.FreelancerAuthRepository.AddFreelancerOTP(otp).ConfigureAwait(false);

            // Send Welcome Email (Registration should fail if email sending fails)
            try
            {

                await SendEmailAsync(registrationDto.Email, otpCode).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Rollback freelancer creation if email sending fails
                return Result<Freelancer>.Failure($"Registration failed due to email sending error: {ex.Message}", StatusCodes.Status500InternalServerError);
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


        #region Update Freelancer Profile
        public async Task<Result<Freelancer>> UpdateProfileAsync(Guid freelancerId, FreelancerEditProfileDto EditProfileDto, CancellationToken cancellationToken)
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

        public async Task<Result<bool>> ValidateOtpAsync(int OTP, CancellationToken cancellationToken)
        {

            var existingOtp = await _unitOfWork.FreelancerAuthRepository.GetValidOtpAsync(OTP, cancellationToken);

            if (existingOtp == null)
            {
                return Result<bool>.Failure("Invalid or expired OTP.", StatusCodes.Status400BadRequest);
            }

            // Check if OTP has expired
            if (existingOtp.ExpiresAt < DateTime.UtcNow)
            {
                return Result<bool>.Failure("OTP has expired. Please request a new one.", StatusCodes.Status400BadRequest);
            }

            // Check if OTP matches
            if (existingOtp.OtpCode != OTP)
            {
                return Result<bool>.Failure("Invalid OTP. Please try again.", StatusCodes.Status400BadRequest);

            }

            // Mark OTP as used
            existingOtp.IsUsed = true;


            // Save changes correctly
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true, "OTP is Verified Successfully.", StatusCodes.Status200OK);
        }



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

        #region Mail
        public async Task SendEmailAsync(string ToEmail, string otp)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_configuration.GetSection("MailSettings:Mail").Value);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = "OTP Verification!";


            // Load Email Template 

            var RootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/EmailTemplate");
            var FullPath = Path.Combine(RootPath, "Welcome_EmailTemplate.html");

            if (!File.Exists(FullPath))
            {
                throw new FileNotFoundException("The email template file was not found.", FullPath);
            }

            var EmailBody = await File.ReadAllTextAsync(FullPath);

            EmailBody = EmailBody.Replace("{OTP}", otp);
            var builder = new BodyBuilder();


            builder.HtmlBody = EmailBody;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();


            var host = _configuration.GetSection("MailSettings:Host").Value;
            var port = int.Parse(_configuration.GetSection("MailSettings:Port").Value);
            var emailAddress = _configuration.GetSection("MailSettings:Mail").Value;
            var emailPassword = _configuration.GetSection("MailSettings:Password").Value;

            // Connect to SMTP server
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);

            // Authenticate
            await smtp.AuthenticateAsync(emailAddress, emailPassword);

            // Send email
            await smtp.SendAsync(email);

            // Disconnect
            await smtp.DisconnectAsync(true);

        }

        public async Task<Result<bool>> ResendOtpAsync(string email, CancellationToken cancellationToken)
        {


            // Check if a valid OTP exists (not used and not expired)
            var validOtp = await _unitOfWork.FreelancerAuthRepository.GetValidFreelancerOTPByEmail(email, cancellationToken);

            if (validOtp != null)
            {
                return Result<bool>.Failure("An active OTP already exists. Please use the existing OTP.", StatusCodes.Status400BadRequest);
            }

            // Check if there's a recently expired OTP
            var expiredOtp = await _unitOfWork.ClientAuthRepository.GetRecentlyExpiredOTP(email, cancellationToken);

            if (expiredOtp == null)
            {
                return Result<bool>.Failure("No OTP found for this email. Please request a new OTP.", StatusCodes.Status404NotFound);
            }

            // Generate a new OTP
            var newOtp = Helper.GenerateNumericOtp(6);
            var newExpirationTime = DateTime.UtcNow.AddMinutes(5);

            // Update expired OTP instead of creating a new one
            expiredOtp.OtpCode = int.Parse(newOtp);
            expiredOtp.ExpiresAt = newExpirationTime;
            expiredOtp.IsUsed = false;

            await _unitOfWork.ClientAuthRepository.UpdateClientOTP(expiredOtp, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            // Send new OTP via email
            await SendEmailAsync(email, newOtp);

            return Result<bool>.Success(true, "A new OTP has been sent to your email.", StatusCodes.Status200OK);
        }
    }
        #endregion

}
