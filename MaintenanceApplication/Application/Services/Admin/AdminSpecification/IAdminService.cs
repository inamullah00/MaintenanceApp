using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;

namespace Maintenance.Application.Services.Admin.AdminSpecification
{
    public interface IAdminService
    {
        Task CreateAdmin(CreateUserViewModel model);
        Task EditAdminProfileAsync(UpdateUserViewModel model);
        Task<UpdateUserViewModel> GetAdminById(string id);
        Task<GridResponseViewModel> GetFilteredUsers(UserFilterViewModel model);
        Task<List<DropdownDto>> GetUsersForDropdown();
    }
}
