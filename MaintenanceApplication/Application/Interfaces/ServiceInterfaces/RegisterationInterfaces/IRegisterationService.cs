using Application.Dto_s.UserDto_s;
using Domain.Entity.UserEntities;
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
        public Task<(bool Success, ApplicationUser? User, string Message)> UserDetailsAsync(Guid Id);
        public Task<(bool Success, string Message)> BlockUserAsync(string Email, bool IsBlock);
        public Task<(bool Success, string Message)> UserProfileAsync();
        public Task<(bool Success, List<AllUsersResponseDto>? Users, string Message)> UsersAsync();
        public Task<(bool Success, string Message)> ValidateOtpAsync(string otp);


    }
}
