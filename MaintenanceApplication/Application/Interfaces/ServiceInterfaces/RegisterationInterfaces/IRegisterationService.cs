using Application.Dto_s.UserDto_s;
using Domain.Entity.UserEntities;
using Maintenance.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ServiceInterfaces.RegisterationInterfaces
{
    public interface IRegisterationService
    {

        public Task<(bool Success, string Message)> RegisterAsync(RegistrationRequestDto request);
        public Task<(bool Success, string Message, string Token)> LoginAsync(LoginRequestDto requestDto);
        public Task<(bool Success, string Message)> LogoutAsync();
        public Task<(bool Success, string Otp, string Message)> ForgotPasswordAsync(string Email);
        public Task<(bool Success, string Message)> ResetPasswordAsync(string email, string newPassword);
        public Task<(bool Success, string Message)> UserApprovalAsync();
        public Task<Result<UserDetailsResponseDto>> UserDetailsAsync(Guid Id);
        public Task<Result<string>> BlockUserAsync(Guid UserId);
        public Task<Result<string>> UnBlockUserAsync(Guid UserId);
        public Task<(bool Success, string Message)> UserProfileAsync();
        public Task<Result<List<UserDetailsResponseDto>>> UsersAsync();
        public Task<(bool Success, string Message)> ValidateOtpAsync(string otp);


    }
}
