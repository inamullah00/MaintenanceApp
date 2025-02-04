using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Application.Wrapper;

namespace Maintenance.Application.Services.Admin.AdminSpecification
{
    public interface IAdminService
    {
        Task BlockUser(string id);
        Task ChangePassword(ChangePasswordViewModel model);
        Task CreateAdmin(CreateUserViewModel model);
        Task EditAdminProfileAsync(UpdateUserViewModel model);
        Task<UserResponseViewModel> GetAdminById(string id);
        Task<PaginatedResponse<UserResponseViewModel>> GetFilteredUsers(UserFilterViewModel model);
        Task<List<DropdownDto>> GetUsersForDropdown();
        Task ResetPassword(ResetPasswordViewModel model);
        Task UnblockUser(string id);
    }
}
