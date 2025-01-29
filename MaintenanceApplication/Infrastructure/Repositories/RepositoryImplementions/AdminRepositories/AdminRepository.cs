using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.AdminInterfaces;
using Maintenance.Application.ViewModel.User;
using Maintenance.Infrastructure.Persistance.Data;

using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories.RepositoryImplementions.AdminRepositories
{

    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region GetAdminUsersForDropdownAsync
        public async Task<List<DropdownDto>> GetAdminUsersForDropdownAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                //.Where(user => user.Role == Role.Admin.ToString())
                .Select(user => new DropdownDto
                {
                    Id = user.Id,
                    Name = user.UserName
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        #endregion


        #region GetFilteredUsers
        public async Task<List<ApplicationUser>> GetFilteredUsersAsync(UserFilterViewModel model, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(x => x.Id.ToString() == model.UserId);
            }

            return await query.Skip(model.Skip).Take(model.Take).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<int> GetFilteredUsersCountAsync(UserFilterViewModel model, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(x => x.Id.ToString() == model.UserId);
            }

            return await query.CountAsync(cancellationToken);
        }

        #endregion

        #region CreateAsync
        //public async Task<(bool Success, string Message)> CreateUserAsync(CreateUserViewModel model)
        //{
        //    var existingUser = await _dbContext.Users.FindByEmailAsync(model.EmailAddress);
        //    if (existingUser != null)
        //    {
        //        return (false, "User with this email already exists.");
        //    }

        //    var userIdentity = new ApplicationUser
        //    {
        //        UserName = model.EmailAddress,
        //        Email = model.EmailAddress,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        PhoneNumber = model.PhoneNumber,
        //        Status = UserStatus.Approved,
        //        Role = Role.Admin.ToString(),
        //        Location = model.Location,
        //        Address = model.Address,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //    };

        //    var registerResult = await _userManager.CreateAsync(userIdentity, model.Password);
        //    if (!registerResult.Succeeded)
        //    {
        //        var errorMessage = string.Join(", ", registerResult.Errors.Select(e => e.Description));
        //        return (false, $"User registration failed. {errorMessage}");
        //    }
        //    return (true, "User created successfully.");
        //}
        #endregion

        #region EditUserProfileAsync
        //public async Task<Result<ApplicationUser>> EditUserProfileAsync(Guid userId, UserProfileEditDto userUpdateRequestDto)
        //{
        //    var user = await _userManager.FindByIdAsync(userId.ToString());
        //    if (user == null)
        //    {
        //        return Result<ApplicationUser>.Failure("No user found with the provided ID", 404);
        //    }

        //    if (!string.IsNullOrEmpty(userUpdateRequestDto.Email))
        //    {
        //        var existingUser = await _userManager.FindByEmailAsync(userUpdateRequestDto.Email);
        //        if (existingUser != null && existingUser.Id != userId.ToString())
        //        {
        //            return Result<ApplicationUser>.Failure("Email conflict: The provided email is already in use by another user", 400);
        //        }
        //        user.Email = userUpdateRequestDto.Email;
        //    }

        //    user.FirstName = userUpdateRequestDto.FirstName ?? user.FirstName;
        //    user.LastName = userUpdateRequestDto.LastName ?? user.LastName;

        //    var updateResult = await _userManager.UpdateAsync(user);
        //    if (!updateResult.Succeeded)
        //    {
        //        return Result<ApplicationUser>.Failure("Failed to update user profile", 500);
        //    }

        //    return Result<ApplicationUser>.Success(user, "Profile updated successfully", 200);
        //}
    }
    #endregion

}
