using Application.Dto_s.UserDto_s;
using Application.Interfaces.ServiceInterfaces;
using Azure.Core;
using Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class RegistrationService : IRegisterationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task<(bool Success, string Message)> LoginAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Message)> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegistrationRequestDto request)
        {

            // Check if the email already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return (false, "User with this email already exists.");
            }

            // Create a new ApplicationUser instance
            var userIdentity = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                Location = request.Location,
                Address = request.Address,
                ExpertiseArea = request.ExpertiseArea,
                Rating = request.Rating,
                Bio = request.Bio,
                ApprovedDate = request.ApprovedDate,
                RegistrationDate = request.RegistrationDate,
                Skills = request.Skills,
                HourlyRate = request.HourlyRate,
                IsVerified = request.IsVerified
            };

            // Register the user with the password
            var registerResult = await _userManager.CreateAsync(userIdentity, request.Password);
            if (!registerResult.Succeeded)
            {
                var errorMessage = string.Join(", ", registerResult.Errors.Select(e => e.Description));
                return (false, $"User registration failed. {errorMessage}");
            }

            // Assign the role to the user if specified
            if (!string.IsNullOrEmpty(request.Role.ToString()))
            {
                var roleExists = await _roleManager.RoleExistsAsync(request.Role.ToString());
                if (!roleExists)
                {
                    return (false, "The role specified does not exist.");
                }

                var assignedRole = await _userManager.AddToRoleAsync(userIdentity, request.Role.ToString());
                if (!assignedRole.Succeeded)
                {
                    return (false, "Failed to assign role to the user.");
                }
            }

            return (true, "User registered successfully.");

        }

        public Task<(bool Success, string Message)> UserApprovalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Message)> UserDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Message)> UserProfileAsync()
        {
            throw new NotImplementedException();
        }
    }
}
