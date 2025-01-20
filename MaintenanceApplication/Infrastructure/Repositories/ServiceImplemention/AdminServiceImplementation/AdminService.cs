using Application.Dto_s.UserDto_s;
using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Dto_s.UserDto_s;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.AdminInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Application.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Repositories.ServiceImplemention.AdminServiceImplementation
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserProfileDto>> EditUserProfileAsync(Guid userId, UserProfileEditDto userUpdateRequestDto)
        {
            var user = await _adminRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<UserProfileDto>.Failure("User not found", 404);
            }

            if (!string.IsNullOrEmpty(userUpdateRequestDto.Email))
            {
                user.Email = userUpdateRequestDto.Email;
            }

            user.FirstName = userUpdateRequestDto.FirstName ?? user.FirstName;
            user.LastName = userUpdateRequestDto.LastName ?? user.LastName;

            await _adminRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var updatedUser = _mapper.Map<UserProfileDto>(user);
            return Result<UserProfileDto>.Success(updatedUser, "User updated successfully", 200);
        }

        public async Task<Result<List<DropdownDto>>> GetUsersForDropdownAsync()
        {
            var users = await _adminRepository.GetAllAsync();
            var dropdownList = users.Select(user => new DropdownDto
            {
                Id = user.Id,
                Name = user.UserName
            }).ToList();

            return Result<List<DropdownDto>>.Success(dropdownList, "Fetched users successfully", 200);
        }

        public async Task<Result<GridResponseViewModel>> GetFilteredUsersAsync(UserFilterViewModel model)
        {
            var query = _adminRepository.QueryUsers();

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(x => x.Id.ToString() == model.UserId);
            }

            var totalCount = await query.CountAsync();
            var users = await query.Skip(model.Skip).Take(model.Take).ToListAsync();

            var response = new GridResponseViewModel
            {
                TotalCount = totalCount,
                Data = users.Select(user => new UserDetailsResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    Location = user.Location,
                    Status = user.Status.ToString()
                }).ToList()
            };

            return Result<GridResponseViewModel>.Success(response, "Filtered users retrieved successfully", 200);
        }

        public async Task<Result<string>> CreateUserAsync(ApplicationUser user, string role)
        {
            await _adminRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Assign role logic (if necessary)...

            return Result<string>.Success("User created successfully", 200);
        }
    }

}
