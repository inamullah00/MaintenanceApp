using Application.Dto_s.UserDto_s;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Domain.Entity.UserEntities;
using Domain.Enums;
using Infrastructure.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Maintenance.Application.Dto_s.Common;
using Maintenance.Application.Dto_s.UserDto_s;
using Maintenance.Application.Services.Account;
using Maintenance.Application.Specifications;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class RegistrationService : IRegisterationService
    {

        #region Constructor & Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _Cache;

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache, ApplicationDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _Cache = cache;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion


        #region Admin Panel
        public async Task<(bool Success, string Message)> CreateUser(CreateUserViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (existingUser != null)
            {
                return (false, "User with this email already exists.");
            }
            var userIdentity = new ApplicationUser
            {
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Status = UserStatus.Approved,
                Location = model.Location,
                Address = model.Address,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var registerResult = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!registerResult.Succeeded)
            {
                var errorMessage = string.Join(", ", registerResult.Errors.Select(e => e.Description));
                return (false, $"User registration failed. {errorMessage}");
            }

            if (!string.IsNullOrEmpty(model.Role.ToString()))
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.Role.ToString());
                if (!roleExists)
                {
                    return (false, "The role specified does not exist.");
                }

                var assignedRole = await _userManager.AddToRoleAsync(userIdentity, model.Role.ToString());
                if (!assignedRole.Succeeded)
                {
                    return (false, "Failed to assign role to the user.");
                }
            }
            return (true, "User created successfully.");
        }
        #endregion

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
                Status = request.Status.Value,
                Location = request.Location,
                Address = request.Address,
                ExpertiseArea = request.ExpertiseArea,
                Rating = 0,
                Experience = request.Experience,
                Bio = request.Bio,
                ApprovedDate = DateTime.UtcNow,
                RegistrationDate = DateTime.UtcNow,
                Skills = request.Skills,
                HourlyRate = request.HourlyRate,
                IsApprove = false,
                TotalEarnings = 0,
                MonthlyLimit = 0,
                OrdersCompleted = 0,
                ReportMonth = DateTime.UtcNow,
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


            // Send a registration email
            try
            {
                var subject = "Welcome to Our Platform!";
                var body = $"Hi {request.FirstName},<br><br>Thank you for registering! We're excited to have you onboard.";
                await SendEmailAsync(request.Email, subject, body);
            }
            catch (Exception ex)
            {
                // Log the error (if a logger is available) and inform the caller
                return (true, $"User registered successfully, but email sending failed: {ex.Message}");
            }
            return (true, "User registered successfully.");
        }

        public Task<(bool Success, string Message)> UserApprovalAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserDetailsResponseDto>> UserDetailsAsync(ISpecification<ApplicationUser> specification)
        {
            var queryResult = SpecificationEvaluator.Default.GetQuery(

                query: _dbContext.Users.AsQueryable(),
                specification: specification
                );


            var result = await queryResult
                         .Select(user => new UserDetailsResponseDto
                         {
                             Id = user.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Status = user.Status.ToString(),
                             Location = user.Location,
                             Address = user.Address,
                             ExpertiseArea = user.ExpertiseArea,
                             Rating = user.Rating.ToString(),
                             MonthlyLimit = user.MonthlyLimit,
                             OrdersCompleted = user.OrdersCompleted,
                             TotalEarnings = user.TotalEarnings,
                             ReportMonth = user.ReportMonth,
                             Bio = user.Bio,
                             ApprovedDate = user.ApprovedDate,
                             RegistrationDate = user.RegistrationDate,
                             Skills = user.Skills,
                             HourlyRate = user.HourlyRate,
                             IsVerified = user.IsApprove,
                             Email = user.Email,
                             EmailConfirmed = user.EmailConfirmed
                         }).FirstOrDefaultAsync();

            if (result == null)
            {
                return Result<UserDetailsResponseDto>.Failure("User with the given ID does not exist.", 404);
            }
            return Result<UserDetailsResponseDto>.Success(result, "User found.", 200);
        }


        public async Task<Result<PagedResult<UserDetailsResponseDto>>> UsersAsync(ISpecification<ApplicationUser>? specification = null, int pageNumber = 1, int pageSize = 10)
        {
            specification ??= new DefaultSpecification<ApplicationUser>();
            var queryResult = SpecificationEvaluator.Default.GetQuery(
                query: _dbContext.Users.AsQueryable(),
                specification: specification
            );

            var totalCount = await queryResult.CountAsync();

            var items = await queryResult
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDetailsResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Status = user.Status.ToString(),
                    Location = user.Location,
                    Address = user.Address,
                    ExpertiseArea = user.ExpertiseArea,
                    MonthlyLimit = user.MonthlyLimit,
                    OrdersCompleted = user.OrdersCompleted,
                    TotalEarnings = user.TotalEarnings,
                    ReportMonth = user.ReportMonth,
                    Rating = user.Rating.ToString(),
                    Bio = user.Bio,
                    ApprovedDate = user.ApprovedDate,
                    RegistrationDate = user.RegistrationDate,
                    Skills = user.Skills,
                    HourlyRate = user.HourlyRate,
                    IsVerified = user.IsApprove,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed
                })
                .ToListAsync();

            var pagedResult = new PagedResult<UserDetailsResponseDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
            };

            return Result<PagedResult<UserDetailsResponseDto>>.Success(pagedResult, "Users retrieved successfully.");
        }


        public Task<(bool Success, string Message)> UserProfileAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<(bool Success, string Otp, string Message)> ForgotPasswordAsync(string emailOrPhone)
        {

            // Check if email exists in the system
            var user = await _userManager.FindByEmailAsync(emailOrPhone);
            if (user == null)
            {
                return (false, "0", "User with this email does not exist.");
            }

            // Generate a numeric OTP
            var otp = GenerateNumericOtp(6); // Generate a 6-digit OTP

            // Store OTP in the database
            var userOtp = new UserOtp
            {
                Id = Guid.NewGuid(),
                Otp = otp,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            //await _genericRepository.OtpAsync<UserOtp,Guid>(userOtp);


            // Send the OTP to the user
            try
            {
                await SendEmailAsync(
                    emailOrPhone,
                    "Your OTP Code",
                    $"Your OTP for verification is: <b>{otp}</b>. This code is valid for 5 minutes."
                );

                return (true, otp, "OTP sent successfully.");
            }
            catch (Exception ex)
            {
                return (false, string.Empty, $"Failed to send OTP: {ex.Message}");
            }

        }


        public async Task<(bool Success, string Message)> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "User not found.");
            }


            // Step 2: Reset the user's password
            var resetResult = await _userManager.RemovePasswordAsync(user);
            if (!resetResult.Succeeded)
            {
                var errors = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                return (false, $"Failed to reset password. {errors}");
            }

            resetResult = await _userManager.AddPasswordAsync(user, newPassword);
            if (!resetResult.Succeeded)
            {
                var errors = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                return (false, $"Failed to set new password. {errors}");
            }

            return (true, "Password reset successful.");
        }

        public async Task<Result<string>> BlockUserAsync(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
            {
                return Result<string>.Failure("User not found.", "User with the given ID does not exist.", 404);
            }

            // Lock the user account
            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<string>.Failure($"Failed to update user status. {errors}", "An error occurred while updating the user status.", 500);
            }
            return Result<string>.Success("User has been blocked successfully.", 200);
        }


        public async Task<Result<string>> UnBlockUserAsync(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
            {
                return Result<string>.Failure("User not found.", "User with the given ID does not exist.", 404);
            }

            var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<string>.Failure($"Failed to update user status. {errors}", "An error occurred while updating the user status.", 500);
            }

            return Result<string>.Success("User has been unblocked successfully.", 200);
        }


        public async Task<(bool Success, string Message)> ValidateOtpAsync(string otp)
        {
            //// Retrieve the OTP from the database
            // var userOtp = await _genericRepository.GetOtpAsync<UserOtp,Guid>(otp);

            //if (userOtp == null)
            //{
            //    return (false, "Invalid or expired OTP.");
            //}

            //// Mark the OTP as used
            //userOtp.IsUsed = true;
            //await _dbContext.SaveChangesAsync();

            return (true, "OTP validated successfully.");

        }


        public async Task<Result<UserProfileDto>> EditUserProfileAsync(Guid userId, UserProfileEditDto userUpdateRequestDto)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Result<UserProfileDto>.Failure("No user found with the provided ID", 404);
            }

            if (!string.IsNullOrEmpty(userUpdateRequestDto.Email))
            {
                if (!IsValidEmail(userUpdateRequestDto.Email))
                {
                    return Result<UserProfileDto>.Failure("Invalid email format", 400);
                }
                var existingUser = await _userManager.FindByEmailAsync(userUpdateRequestDto.Email);

                if (existingUser != null && existingUser.Id != userId.ToString())
                {
                    return Result<UserProfileDto>.Failure("Email conflict: The provided email is already in use by another user", 400);
                }

                user.Email = userUpdateRequestDto.Email;
            }

            if (!string.IsNullOrEmpty(userUpdateRequestDto.FirstName))
            {
                user.FirstName = userUpdateRequestDto.FirstName;
            }

            if (!string.IsNullOrEmpty(userUpdateRequestDto.LastName))
            {
                user.LastName = userUpdateRequestDto.LastName;
            }

            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return Result<UserProfileDto>.Failure("Failed to update user profile", 500);
                }
            }
            catch (Exception ex)
            {
                return Result<UserProfileDto>.Failure($"An error occurred while saving the profile: {ex.Message}", 500);
            }
            var updatedUserProfile = _mapper.Map<UserProfileDto>(user);

            return Result<UserProfileDto>.Success(updatedUserProfile, "Profile updated successfully", 200);
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

        #region Token
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

        #endregion

        #region Mail
        public async Task SendEmailAsync(string ToEmail, string Subject, string Body)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_configuration.GetSection("MailSettings:Mail").Value);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = Subject;


            // Load Email Template 

            var RootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/EmailTemplate");
            var FullPath = Path.Combine(RootPath, "Welcome_EmailTemplate.html");

            if (!File.Exists(FullPath))
            {
                throw new FileNotFoundException("The email template file was not found.", FullPath);
            }

            var EmailBody = await File.ReadAllTextAsync(FullPath);

            var builder = new BodyBuilder();


            builder.HtmlBody = EmailBody;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();


            var host = _configuration.GetSection("MailSettings:Host").Value;
            var port = int.Parse(_configuration.GetSection("MailSettings:Port").Value);
            var emailAddress = _configuration.GetSection("MailSettings:Mail").Value;
            var emailPassword = _configuration.GetSection("MailSettings:Password").Value;

            // Connect to SMTP server
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);

            // Authenticate
            await smtp.AuthenticateAsync(emailAddress, emailPassword);

            // Send email
            await smtp.SendAsync(email);

            // Disconnect
            await smtp.DisconnectAsync(true);

        }
        #endregion

        #region Generate-Otp

        private string GenerateNumericOtp(int length)
        {
            var random = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10)); // Append a random digit (0-9)
            }

            return otp.ToString();
        }


        #endregion
    }
}
