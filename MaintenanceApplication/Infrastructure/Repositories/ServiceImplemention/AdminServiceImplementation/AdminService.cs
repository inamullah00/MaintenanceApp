using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.AdminSpecification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Infrastructure.Extensions;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention.AdminServiceImplementation
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public AdminService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<GridResponseViewModel> GetFilteredUsers(UserFilterViewModel model)
        {
            var query = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(x => x.Id.ToString() == model.UserId);
            }
            var totalCount = await query.CountAsync();
            var data = await query.Skip(model.Skip).Take(model.Take)
                .Select(u => new UserResponseViewModel
                {
                    Id = u.Id,
                    FirstName = u.FullName ?? string.Empty,
                    PhoneNumber = u.PhoneNumber ?? string.Empty,
                    EmailAddress = u.Email ?? string.Empty,
                    //Role = u.Role,
                    IsBlocked = u.LockoutEnd >= DateTime.Now
                }).ToListAsync();
            return new GridResponseViewModel
            {
                TotalCount = totalCount,
                Data = data
            };
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
                //Role = Role.Admin.ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var registerResult = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!registerResult.Succeeded)
            {
                var errorMessage = string.Join(", ", registerResult.Errors.Select(e => e.Description));
                throw new CustomException($"User registration failed: {errorMessage}");
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
                FirstName = user.FullName ?? string.Empty,
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

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                user.FullName = model.FirstName;
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

