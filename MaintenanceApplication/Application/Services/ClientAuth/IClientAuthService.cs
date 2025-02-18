using Application.Dto_s.UserDto_s;
using Maintenance.Application.Dto_s.UserDto_s.ClientAuthDtos;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientEntity =  Maintenance.Domain.Entity.ClientEntities.Client;
namespace Maintenance.Application.Services.ClientAuth
{
    public interface IClientAuthService
    {

        // Register a new client account
        public Task<Result<string>> RegisterClientAsync(ClientRegistrationDto registrationDto, CancellationToken cancellationToken);

        // Login a client
        public Task<Result<ClientLoginResponseDto>> LoginAsync(ClientLoginDto loginDto, CancellationToken cancellationToken);

        // Logout method for Client
        public Task<Result<bool>> LogoutAsync(Guid clientId, CancellationToken cancellationToken);

        // Update client profile information
        public Task<Result<ClientEntity>> UpdateProfileAsync(Guid clientId, ClientEditProfileDto editProfileDto, CancellationToken cancellationToken);

        // Change client password
        public Task<bool> ChangePasswordAsync(Guid clientId, string oldPassword, string newPassword);


        // Forgot password (send reset email)
        public Task<Result<string>> ForgotPasswordAsync(string email, CancellationToken cancellationToken);

        // Reset password using a reset token
        public Task<Result<bool>> ValidateOtpAsync(int OTP, CancellationToken cancellationToken);
        public Task<Result<bool>> ResendOtpAsync(string email, CancellationToken cancellationToken);
        public Task<Result<bool>> ResetPasswordAsync(ResetPasswordRequestDto request, CancellationToken cancellationToken);


    }
}
