using Application.Dto_s.UserDto_s;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Maintenance.Application.Common;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Common.Utility;
using Maintenance.Application.Dto_s.UserDto_s.ClientAuthDtos;
using Maintenance.Application.Security;
using Maintenance.Application.Services.ClientAuth;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class ClientAuthService : IClientAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ClientAuthService(
            IUnitOfWork unitOfWork,
            IPasswordService passwordService,
            ITokenService tokenService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Task<bool> ChangePasswordAsync(Guid clientId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> ForgotPasswordAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result<string>.Failure("Email cannot be empty.", StatusCodes.Status400BadRequest);
            }

            // Check if the client exists in the system
            var client = await _unitOfWork.ClientAuthRepository.GetClientByEmailAsync(email, cancellationToken);
            if (client == null)
            {
                return Result<string>.Failure("Client with this email does not exist.", StatusCodes.Status404NotFound);
            }

            // Check if a valid OTP already exists
            var validOtp = await _unitOfWork.ClientAuthRepository.GetValidClientOTPByEmail(email, cancellationToken);
            if (validOtp != null)
            {
                return Result<string>.Failure("An active OTP already exists. Please use the existing OTP.", StatusCodes.Status400BadRequest);
            }

            // Check if there is a recently expired OTP
            var expiredOtp = await _unitOfWork.ClientAuthRepository.GetRecentlyExpiredOTP(email, cancellationToken);

            // Generate a new OTP
            var otpCode = Helper.GenerateNumericOtp(6);
            var expirationTime = DateTime.UtcNow.AddMinutes(5);

            if (expiredOtp != null)
            {
                // Reuse and update the expired OTP instead of creating a new one
                expiredOtp.OtpCode = int.Parse(otpCode);
                expiredOtp.ExpiresAt = expirationTime;
                expiredOtp.CreatedAt = DateTime.UtcNow;
                expiredOtp.IsUsed = false;

                await _unitOfWork.ClientAuthRepository.UpdateClientOTP(expiredOtp, cancellationToken);
            }
            else
            {
                // Create a new OTP record
                var otp = new ClientOtp
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    OtpCode = int.Parse(otpCode),
                    ExpiresAt = expirationTime,
                    CreatedAt = DateTime.UtcNow,
                    IsUsed = false,
                    ClientId = client.Id
                };

                await _unitOfWork.ClientAuthRepository.AddClientOTP(otp,cancellationToken);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Ensure changes are saved

                // Send OTP via email
                await SendEmailAsync(email, otpCode);

                return Result<string>.Success("OTP sent successfully. Please check your email.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to send OTP: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<Result<ClientLoginResponseDto>> LoginAsync(ClientLoginDto loginDto, CancellationToken cancellationToken)
        {

            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return Result<ClientLoginResponseDto>.Failure("Email and Password are required.", StatusCodes.Status400BadRequest);
            }

            var client = await _unitOfWork.ClientAuthRepository.GetClientByEmailAsync(loginDto.Email, cancellationToken);

            if (client == null || !_passwordService.ValidatePassword(loginDto.Password, client.Password))
            {
                return Result<ClientLoginResponseDto>.Failure("Invalid email or password.", StatusCodes.Status401Unauthorized);
            }

            var token = _tokenService.GenerateToken<Client>(client);

            var response = new ClientLoginResponseDto
            {
                Token = token,
                ClientId = client.Id,
                FullName = $"{client.FullName}",
                Email = client.Email,
            };

            return Result<ClientLoginResponseDto>.Success(response, "Login successful.", StatusCodes.Status200OK);
        }

        public async Task<Result<bool>> LogoutAsync(Guid clientId, CancellationToken cancellationToken)
        {
            if (clientId == Guid.Empty)
            {
                return Result<bool>.Failure("Client ID is required.", StatusCodes.Status400BadRequest);
            }

            var client = await _unitOfWork.ClientAuthRepository.GetClientByIdAsync(clientId, cancellationToken);

            if (client == null)
            {
                return Result<bool>.Failure("Client not found.", StatusCodes.Status404NotFound);
            }

            // Remove refresh token from DB if stored
            // client.RefreshTokenExpiryTime = null;
            client.IsActive = false;

            await _unitOfWork.ClientAuthRepository.UpdateClientAsync(client, cancellationToken);

            return Result<bool>.Success(true, "Logout successful.", StatusCodes.Status200OK);

        }

        public async Task<Result<string>> RegisterClientAsync(ClientRegistrationDto registrationDto, CancellationToken cancellationToken)
        {

            if (registrationDto == null)
            {
                return Result<string>.Failure("Invalid or empty data.", StatusCodes.Status400BadRequest);
            }

            // Check if client with the same email already exists
            var existingClient = await _unitOfWork.ClientAuthRepository.GetClientByEmailAsync(registrationDto.Email, cancellationToken);
            if (existingClient != null)
            {
                return Result<string>.Failure("Email already exists.", StatusCodes.Status409Conflict);
            }

            // Map DTO to Client entity
            var client = _mapper.Map<Client>(registrationDto);

            // Hash password before saving
            client.Password = _passwordService.HashPassword(registrationDto.Password);

         
            // Save client to database
            var createdClient = await _unitOfWork.ClientAuthRepository.AddClientAsync(client, cancellationToken);
            if (createdClient == null)
            {
                return Result<string>.Failure("Client registration failed.", StatusCodes.Status500InternalServerError);
            }

            // Generate and store OTP
            var otpCode = Helper.GenerateNumericOtp(6);
            var otp = new ClientOtp
            {
                Id = Guid.NewGuid(),
                Email = registrationDto.Email,
                OtpCode = int.Parse(otpCode),
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                CreatedAt = DateTime.UtcNow,
                IsUsed = false,
                ClientId = createdClient.Id
            };

            await _unitOfWork.ClientAuthRepository.AddClientOTP(otp, cancellationToken);

            // Send Welcome Email
            try
            {
                await SendEmailAsync(registrationDto.Email, otpCode);
            }
            catch (Exception ex)
            {
                // Rollback client creation if email sending fails
                await _unitOfWork.ClientAuthRepository.DeleteClientAsync(client.Id ,cancellationToken);
                return Result<string>.Failure($"Registration failed due to email sending error: {ex.Message}", StatusCodes.Status500InternalServerError);
            }

            return Result<string>.Success(client.FullName, "Client registered successfully.", StatusCodes.Status201Created);
        }

        public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordRequestDto request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.OtpCode) ||
                    string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return Result<bool>.Failure("All fields are required.", StatusCodes.Status400BadRequest);
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return Result<bool>.Failure("Passwords do not match.", StatusCodes.Status400BadRequest);
            }

            // Retrieve OTP from database
            var existingOtp = await _unitOfWork.ClientAuthRepository.GetValidClientOTPByEmail(request.Email, cancellationToken);

            if (existingOtp == null || existingOtp.IsUsed || existingOtp.ExpiresAt < DateTime.UtcNow)
            {
                return Result<bool>.Failure("Invalid or expired OTP.", StatusCodes.Status400BadRequest);
            }

            // Retrieve Client from database
            var client = await _unitOfWork.ClientAuthRepository.GetClientByEmailAsync(request.Email, cancellationToken);
            if (client == null)
            {
                return Result<bool>.Failure("Client with this email does not exist.", StatusCodes.Status404NotFound);
            }

            // Hash the new password
            client.Password = _passwordService.HashPassword(request.NewPassword);

            // Mark OTP as used
            existingOtp.IsUsed = true;

            await _unitOfWork.ClientAuthRepository.UpdateClientAsync(client , cancellationToken);
            await _unitOfWork.ClientAuthRepository.UpdateClientOTP(existingOtp, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true, "Password has been reset successfully.", StatusCodes.Status200OK);

        }

        public async Task<Result<Client>> UpdateProfileAsync(Guid clientId, ClientEditProfileDto editProfileDto, CancellationToken cancellationToken)
        {

            if (clientId == Guid.Empty || editProfileDto == null)
            {
                return Result<Client>.Failure(ErrorMessages.InvalidOrEmpty, StatusCodes.Status400BadRequest);
            }

            var client = await _unitOfWork.ClientAuthRepository.GetClientByIdAsync(clientId, cancellationToken).ConfigureAwait(false);

            if (client == null)
            {
                return Result<Client>.Failure(ErrorMessages.ClientNotFound, StatusCodes.Status404NotFound);
            }

            // Update fields
            client.FullName = editProfileDto.FullName ?? client.FullName;
            client.Email = editProfileDto.Email ?? client.Email;
            client.PhoneNumber = editProfileDto.PhoneNumber ?? client.PhoneNumber;
            client.ProfilePicture = editProfileDto.ProfilePicture ?? client.ProfilePicture;
            client.UpdatedAt = DateTime.UtcNow; // Ensure timestamp is updated

            // Save changes
            var updatedClient = await _unitOfWork.ClientAuthRepository.UpdateClientAsync(client, cancellationToken).ConfigureAwait(false);

            if (updatedClient == null)
            {
                return Result<Client>.Failure(ErrorMessages.UpdateFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<Client>.Success(updatedClient, SuccessMessages.ProfileUpdatedSuccessfully, StatusCodes.Status200OK);

        }

        public async Task<Result<bool>> ValidateOtpAsync(int OTP, CancellationToken cancellationToken)
        {
            var existingOtp = await _unitOfWork.ClientAuthRepository.GetValidOtpAsync(OTP, cancellationToken);

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

        public async Task<Result<bool>> ResendOtpAsync(string email, CancellationToken cancellationToken)
        {

            // Check if a valid OTP exists (not used and not expired)
            var validOtp = await _unitOfWork.ClientAuthRepository.GetValidClientOTPByEmail(email, cancellationToken);

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







        #region Send Email (OTP)
        public async Task SendEmailAsync(string toEmail, string otp)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "OTP Verification";

            // Load Email Template
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/EmailTemplate");
            var fullPath = Path.Combine(rootPath, "Welcome_EmailTemplate.html");

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("The email template file was not found.", fullPath);
            }

            var emailBody = await File.ReadAllTextAsync(fullPath);
            emailBody = emailBody.Replace("{OTP}", otp);

            var builder = new BodyBuilder
            {
                HtmlBody = emailBody
            };

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();

            var host = _configuration["MailSettings:Host"];
            var port = int.Parse(_configuration["MailSettings:Port"]);
            var emailAddress = _configuration["MailSettings:Mail"];
            var emailPassword = _configuration["MailSettings:Password"];

            // Connect to SMTP server
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailAddress, emailPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        #endregion
    }
}
