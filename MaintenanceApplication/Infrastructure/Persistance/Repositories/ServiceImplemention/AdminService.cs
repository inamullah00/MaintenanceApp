using Domain.Entity.UserEntities;
using Domain.Enums;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.AdminSpecification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Application.Wrapper;
using Maintenance.Infrastructure.Extensions;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<List<DropdownDto>> GetUsersForDropdown()
        {
            var customers = await _dbContext.Users.Select(a => new DropdownDto
            {
                Id = a.Id,
                Name = a.FullName ?? string.Empty,
            }).ToListAsync().ConfigureAwait(false);
            return customers;
        }
        public async Task<PaginatedResponse<UserResponseViewModel>> GetFilteredUsers(UserFilterViewModel model)
        {
            var query = _dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(x => x.Id.ToString() == model.UserId);
            }

            var totalCount = await query.CountAsync();

            int skip = (model.Page - 1) * model.PageSize;

            var users = await query.Skip(skip).Take(model.PageSize).ToListAsync();

            var data = new List<UserResponseViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                data.Add(new UserResponseViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    EmailAddress = user.Email ?? string.Empty,
                    Role = roles.Any() ? string.Join(", ", roles) : "No Role",
                    IsBlocked = user.LockoutEnd >= DateTime.Now
                });
            }

            return new PaginatedResponse<UserResponseViewModel>(data, totalCount, model.Page, model.PageSize);
        }




        public async Task CreateAdmin(CreateUserViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (existingUser != null)
            {
                throw new CustomException("User with this email already exists.");
            }

            var userIdentity = new ApplicationUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var registerResult = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!registerResult.Succeeded)
            {
                var errorMessage = string.Join(", ", registerResult.Errors.Select(e => e.Description));
                throw new CustomException($"User registration failed: {errorMessage}");
            }

            var roleAssignmentResult = await _userManager.AddToRoleAsync(userIdentity, Role.Admin.ToString());
            if (!roleAssignmentResult.Succeeded)
            {
                var errorMessage = string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description));
                throw new CustomException($"Failed to assign Admin role: {errorMessage}");
            }
        }


        public async Task<UserResponseViewModel> GetAdminById(string id)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
            {
                throw new CustomException("User not found.");
            }

            var userViewModel = new UserResponseViewModel
            {
                Id = user.Id.ToString(),
                FullName = user.FullName ?? string.Empty,
                EmailAddress = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                IsBlocked = user.LockoutEnd >= DateTime.Now
            };

            return userViewModel;
        }

        public async Task EditAdminProfileAsync(UpdateUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                throw new CustomException("No user found with the provided ID");
            }

            if (!string.IsNullOrEmpty(model.EmailAddress))
            {
                if (!IsValidEmail(model.EmailAddress))
                {
                    throw new CustomException("Invalid email format");

                }

                var existingUser = await _userManager.FindByEmailAsync(model.EmailAddress);


                if (existingUser != null && existingUser.Id != model.Id)
                {
                    throw new CustomException("Email conflict: The provided email is already in use by another user");
                }

                user.Email = model.EmailAddress;
            }

            if (!string.IsNullOrEmpty(model.FullName))
            {
                user.FullName = model.FullName;
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                user.PhoneNumber = model.PhoneNumber;
            }

            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new CustomException("Failed to update user profile");
                }
            }
            catch (Exception ex)
            {
                throw new CustomException($"An error occurred while saving the profile: {ex.Message}");
            }
        }

        public async Task BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false) ?? throw new CustomException("User not found");
            user.BlockUser();
            await _userManager.UpdateAsync(user).ConfigureAwait(false);
        }

        public async Task UnblockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false) ?? throw new CustomException("User not found");
            user.UnBlockUser();
            await _userManager.UpdateAsync(user).ConfigureAwait(false);
        }

        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            var userId = AppHttpContext.GetAdminCurrentUserId();
            var user = await _userManager.FindByIdAsync(userId) ?? throw new CustomException("User not found");
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new CustomException(string.Join("<br>", result.Errors.Select(a => a.Description).ToList()));
            }
        }

        public async Task ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId) ?? throw new CustomException("User not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(string.Join("<br>", result.Errors.Select(a => a.Description).ToList()));
            }
        }


        #region Validate Email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }


}

