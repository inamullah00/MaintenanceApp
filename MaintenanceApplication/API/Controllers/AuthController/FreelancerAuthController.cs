using Application.Dto_s.UserDto_s;
using AutoMapper;
using Maintenance.Application.Dto_s.FreelancerDto_s.FreelancerAccount;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.FreelancerAuth.Filter;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerAuthController : ControllerBase
    {

        #region Constructor & Fields

        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<FreelancerAuthController> _logger;

        public FreelancerAuthController(IServiceManager serviceManager, ILogger<FreelancerAuthController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #endregion

        #region Freelancer Registration

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(FreelancerRegistrationDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("SignUp request started for freelancer: {FullName}", request.FullName);

                var result = await _serviceManager.FreelancerAuthService.RegisterFreelancerAsync(request, cancellationToken).ConfigureAwait(false);

                if (result == null)
                {
                    _logger.LogWarning("SignUp failed for freelancer: {FullName}, Message: {Message}", request.FullName, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("SignUp successful for freelancer: {FullName}", request.FullName);
                return Ok(new { Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during SignUp process for freelancer.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Login

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(FreelancerLoginDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Login request started for email: {Email}", request.Email);

                var result = await _serviceManager.FreelancerAuthService.LoginAsync(request, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Login failed for email: {Email}, Message: {Message}", request.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Login successful for email: {Email}", request.Email);

                // Extracting the response DTO
                var response = result.Value;

                return Ok(new
                {
                    Token = response.Token,
                    RefreshToken = response.RefreshToken,
                    FreelancerId = response.FreelancerId,
                    FullName = response.FullName,
                    Email = response.Email,
                    ProfilePicture = response.ProfilePicture,
                    Message = result.Message
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Login process for email: {Email}", request.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Logout

        [HttpPost]
        [Route("Logout/{FreelancerId:guid}")]
        public async Task<IActionResult> Logout(Guid FreelancerId,CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Logout request started.");

                var result = await _serviceManager.FreelancerAuthService.LogoutAsync(FreelancerId,cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Logout failed.");
                    return BadRequest("Logout failed.");
                }

                _logger.LogInformation("Logout successful.");
                return Ok(new { Message = "User logged out successfully." });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Logout process.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Forgot Password

        [HttpPost]
        [Route("Forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto requestDto , CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Forgot Password request started for email: {Email}", requestDto.Email);

                var result = await _serviceManager.FreelancerAuthService.ForgotPasswordAsync(requestDto.Email, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Forgot Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Forgot Password email sent successfully to: {Email}", requestDto.Email);
                return Ok(new { Message = "Password reset email has been sent successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Forgot Password process for email: {Email}", requestDto.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Reset Password

        [HttpPost]
        [Route("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDto , CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Reset Password request started for email: {Email}", requestDto.Email);

                if (requestDto.NewPassword == null || requestDto.NewPassword != requestDto.ConfirmPassword)
                {
                    return BadRequest("Password mismatch");
                }

                var result = await _serviceManager.FreelancerAuthService.ResetPasswordAsync(requestDto.Email, requestDto.NewPassword, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Reset Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Password reset successful for email: {Email}", requestDto.Email);
                return Ok(new { Message = result.Message });
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Reset Password process for email: {Email}", requestDto.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Validate OTP

        [HttpPost]
        [Route("Validate-Otp")]
        public async Task<IActionResult> ValidateOtp([FromBody] int otp , CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validate OTP request started for OTP: {Otp}", otp);

                var result = await _serviceManager.FreelancerAuthService.ValidateOtpAsync(otp, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("OTP validation failed for OTP: {Otp}, Message: {Message}", otp, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("OTP validation successful for OTP: {Otp}", otp);
                return Ok(new { Message = result.Message });
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during OTP validation process.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion


        #region Freelancer Profile Management

        #region List-OF-Freelancers
        [HttpGet]
        [Route("Freelancers")]
        public async Task<IActionResult> GetAllFreelancers(string? Keyword = "")
        {
            try
            {
                _logger.LogInformation("Fetching list of freelancers with keyword: {Keyword}", Keyword);

                var result = await _serviceManager.FreelancerAuthService.GetFreelancersAsync(Keyword);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancer list retrieved successfully.");
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to retrieve freelancer list. Message: {Message}", result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching freelancer list.");
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region List-OF-Paginated-Freelancers
        [HttpPost]
        [Route("Freelancers-Paginated")]
        public async Task<IActionResult> GetAllFreelancersPaginatedAsync(FreelancerTableFilter filter)
        {
            try
            {
                _logger.LogInformation("Fetching paginated list of freelancers with filter: {Filter}", filter);

                //var result = await _serviceManager.FreelancerAuthService.FreelancerPaginatedAsync(filter);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Paginated freelancer list retrieved successfully.");
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to retrieve paginated freelancer list. Message: {Message}", result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching paginated freelancer list.");
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Freelancer-Details
        [HttpGet]
        [Route("Freelancer-Details/{freelancerId:guid}")]
        public async Task<IActionResult> GetFreelancerDetails(Guid freelancerId,CancellationToken cancellationToken)
        {
            try
            {
                if (freelancerId == Guid.Empty)
                {
                    return BadRequest(new { Message = "Freelancer ID cannot be empty." });
                }

                _logger.LogInformation("Fetching details for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _serviceManager.FreelancerAuthService.GetFreelancerProfileAsync(freelancerId, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancer details retrieved successfully for ID: {FreelancerId}", freelancerId);
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to retrieve freelancer details for ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching freelancer details for ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Block-Freelancer
        [HttpPut]
        [Route("Block-Freelancer/{freelancerId:guid}")]
        public async Task<IActionResult> BlockFreelancer(Guid freelancerId , [FromBody] FreelancerStatusUpdateDto updateDto)
        {
            try
            {
                _logger.LogInformation("Block request started for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _serviceManager.FreelancerAuthService.BlockFreelancerAsync(freelancerId,updateDto);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancer blocked successfully with ID: {FreelancerId}", freelancerId);
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to block freelancer with ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while blocking freelancer with ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region UnBlock-Freelancer
        [HttpPut]
        [Route("UnBlock-Freelancer/{freelancerId:guid}")]
        public async Task<IActionResult> UnBlockFreelancer(Guid freelancerId , [FromBody] FreelancerStatusUpdateDto updateDto , CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Unblock request started for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _serviceManager.FreelancerAuthService.UnBlockFreelancerAsync(freelancerId, updateDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancer unblocked successfully with ID: {FreelancerId}", freelancerId);
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to unblock freelancer with ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while unblocking freelancer with ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Edit-Freelancer-Profile
        [HttpPut]
        [Route("Edit-Freelancer-Profile/{FreelancerId:guid}")]
        public async Task<IActionResult> EditFreelancerProfile(Guid FreelancerId, [FromBody] FreelancerEditProfileDto freelancerProfileEditDto,CancellationToken cancellationToken)
        {
            try
            {
                if (freelancerProfileEditDto == null || !ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, Success = false, Message = "Invalid profile data." });
                }

                _logger.LogInformation("Edit profile request started for freelancer with ID: {FreelancerId}", FreelancerId);

                var result = await _serviceManager.FreelancerAuthService.UpdateProfileAsync(FreelancerId, freelancerProfileEditDto , cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancer profile updated successfully for ID: {FreelancerId}", FreelancerId);
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to edit freelancer profile for ID: {FreelancerId}. Message: {Message}", FreelancerId, result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing freelancer profile for ID: {FreelancerId}", FreelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region ApproveFreelancer Account
        [HttpPut]
        [Route("FreelancerApproval/{freelancerId:guid}")]
        public async Task<IActionResult> ApproveFreelancer(Guid freelancerId,FreelancerStatusUpdateDto statusUpdateDto  ,  CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Approval request started for freelancer ID: {FreelancerId}", freelancerId);


                var result = await _serviceManager.FreelancerAuthService.ApproveFreelancerAsync(freelancerId, statusUpdateDto , cancellationToken).ConfigureAwait(false);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Freelancer approval failed for ID: {FreelancerId}, Message: {Message}", freelancerId, result.Message);
                    return BadRequest(new { Success = false, Message = result.Message });
                }

                _logger.LogInformation("Freelancer approved successfully for ID: {FreelancerId}");
                return Ok(new { Success = true, Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while approving freelancer with ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }


        #endregion

        #endregion
    }
}
