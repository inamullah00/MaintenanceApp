using Application.Dto_s.UserDto_s;
using Ardalis.Specification;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Dto_s.UserDto_s;
using Maintenance.Application.Services.Account.Filter;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Account
{
    public interface IRegisterationService
    {

        public Task<(bool Success, string Message)> RegisterAsync(RegistrationRequestDto request);
        public Task<(bool Success, string Message, string Token)> LoginAsync(LoginRequestDto requestDto);
        public Task<(bool Success, string Message)> LogoutAsync();
        public Task<(bool Success, string Otp, string Message)> ForgotPasswordAsync(string Email);
        public Task<(bool Success, string Message)> ResetPasswordAsync(string email, string newPassword);
        public Task<(bool Success, string Message)> UserApprovalAsync();
        public Task<Result<UserDetailsResponseDto>> UserDetailsAsync(ISpecification<ApplicationUser> specification);
        public Task<Result<PaginatedResponse<UserDetailsResponseDto>>> UsersPaginatedAsync(UserTableFilter filter);
        public Task<Result<string>> BlockUserAsync(Guid UserId);
        public Task<Result<string>> UnBlockUserAsync(Guid UserId);
        public Task<(bool Success, string Message)> UserProfileAsync();
        public Task<(bool Success, string Message)> ValidateOtpAsync(string otp);

        public Task<Result<UserProfileDto>> EditUserProfileAsync(Guid Id, UserProfileEditDto editUserProfile);
        Task<(bool Success, string Message)> CreateUser(CreateUserViewModel model);
        Task<Result<CustomPagedResult<UserDetailsResponseDto>>> UsersAsync(ISpecification<ApplicationUser>? specification = null, int pageNumber = 1, int pageSize = 10);
        Task<GridResponseViewModel> GetFilteredUsers(UserFilterViewModel model);
        Task<List<DropdownDto>> GetUsersForDropdown();
    }
}
