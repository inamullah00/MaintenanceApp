using Application.Dto_s.UserDto_s;
using Application.Interfaces.ServiceInterfaces;
using Azure.Core;
using Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class RegistrationService : IRegisterationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<(bool Success, string Message, string Token)> LoginAsync(LoginRequestDto request)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return (false, "Invalid email or password.", null);
            }

            // Check if the password is correct
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return (false, "Invalid email or password.", null);
            }

            // Ensure the user is verified, active, or meets specific conditions (optional)
            //if (!user.IsVerified)
            //{
            //    return (false, "Account is not verified.", null);
            //}

            // Generate a JWT token
            var token = await GenerateJwtTokenAsync(user);

            return (true, "Login successful.", token);
        }

        public async Task<(bool Success, string Message)> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return (true, "User logged out successfully.");
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


        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

            // Add user roles as claims
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
