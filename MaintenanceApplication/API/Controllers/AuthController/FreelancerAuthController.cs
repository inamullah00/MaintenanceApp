using Application.Dto_s.UserDto_s;
using AutoMapper;
using Maintenance.Application.Dto_s.UserDto_s.FreelancerAuthDtos;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.FreelancerAuth.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerAuthController : ControllerBase
    {

        #region Constructor & Fields

        private readonly IMapper _mapper;
        private readonly IFreelancerAuthService _freelancerAuthService;
        private readonly ILogger<FreelancerAuthController> _logger;

        public FreelancerAuthController(IFreelancerAuthService freelancerAuthService, ILogger<FreelancerAuthController> logger)
        {
            _freelancerAuthService = freelancerAuthService;
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

                var result = await _freelancerAuthService.RegisterFreelancerAsync(request, cancellationToken).ConfigureAwait(false);

                if (result != null)
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
        public async Task<IActionResult> Login(FreelancerLoginDto request)
        {
            try
            {
                _logger.LogInformation("Login request started for email: {Email}", request.Email);

                var result = await _freelancerAuthService.LoginAsync(request);

                //if (!result.Success)
                //{
                //    _logger.LogWarning("Login failed for email: {Email}, Message: {Message}", request.Email, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("Login successful for email: {Email}", request.Email);
                //return Ok(new { Token = result.Token, Message = result.Message });
                return null;
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
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                _logger.LogInformation("Logout request started.");

                var result = await _freelancerAuthService.LogoutAsync();

                //if (!result.Success)
                //{
                //    _logger.LogWarning("Logout failed.");
                //    return BadRequest("Logout failed.");
                //}

                //_logger.LogInformation("Logout successful.");
                //return Ok(new { Message = "User logged out successfully." });
                return null;
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
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation("Forgot Password request started for email: {Email}", requestDto.Email);

                var result = await _freelancerAuthService.ForgotPasswordAsync(requestDto.Email);

                //if (!result.Success)
                //{
                //    _logger.LogWarning("Forgot Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("Forgot Password email sent successfully to: {Email}", requestDto.Email);
                //return Ok(new { Message = "Password reset email has been sent successfully." });
                return null;
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
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation("Reset Password request started for email: {Email}", requestDto.Email);

                if (requestDto.NewPassword == null || requestDto.NewPassword != requestDto.ConfirmPassword)
                {
                    return BadRequest("Password mismatch");
                }

                var result = await _freelancerAuthService.ResetPasswordAsync(requestDto.Email, requestDto.NewPassword);

                //if (!result.Success)
                //{
                //    _logger.LogWarning("Reset Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("Password reset successful for email: {Email}", requestDto.Email);
                //return Ok(new { Message = result.Message });
                return null;
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
        public async Task<IActionResult> ValidateOtp([FromBody] string otp)
        {
            try
            {
                _logger.LogInformation("Validate OTP request started for OTP: {Otp}", otp);

                //var result = await _freelancerAuthService.ValidateOtpAsync(otp);

                //if (!result.Success)
                //{
                //    _logger.LogWarning("OTP validation failed for OTP: {Otp}, Message: {Message}", otp, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("OTP validation successful for OTP: {Otp}", otp);
                //return Ok(new { Message = result.Message });
                return null;
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

                var result = await _freelancerAuthService.GetFreelancersAsync(Keyword);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Freelancer list retrieved successfully.");
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to retrieve freelancer list. Message: {Message}", result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null;
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

                //var result = await _freelancerAuthService.FreelancerPaginatedAsync(filter);

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
        [Route("Freelancer-Details/{FreelancerId:guid}")]
        public async Task<IActionResult> GetFreelancerDetails(Guid freelancerId)
        {
            try
            {
                if (freelancerId == Guid.Empty)
                {
                    return BadRequest(new { Message = "Freelancer ID cannot be empty." });
                }

                _logger.LogInformation("Fetching details for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _freelancerAuthService.GetFreelancerProfileAsync(freelancerId);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Freelancer details retrieved successfully for ID: {FreelancerId}", freelancerId);
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to retrieve freelancer details for ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching freelancer details for ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Block-Freelancer
        [HttpPost]
        [Route("Block-Freelancer/{FreelancerId:guid}")]
        public async Task<IActionResult> BlockFreelancer(Guid freelancerId)
        {
            try
            {
                _logger.LogInformation("Block request started for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _freelancerAuthService.BlockFreelancerAsync(freelancerId);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Freelancer blocked successfully with ID: {FreelancerId}", freelancerId);
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to block freelancer with ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while blocking freelancer with ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region UnBlock-Freelancer
        [HttpPost]
        [Route("UnBlock-Freelancer/{FreelancerId:guid}")]
        public async Task<IActionResult> UnBlockFreelancer(Guid freelancerId)
        {
            try
            {
                _logger.LogInformation("UnBlock request started for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _freelancerAuthService.UnBlockFreelancerAsync(freelancerId);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Freelancer unblocked successfully with ID: {FreelancerId}", freelancerId);
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to unblock freelancer with ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null;
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
        public async Task<IActionResult> EditFreelancerProfile(Guid freelancerId, [FromBody] FreelancerUpdateDto freelancerProfileEditDto)
        {
            try
            {
                if (freelancerProfileEditDto == null || !ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, Success = false, Message = "Invalid profile data." });
                }

                _logger.LogInformation("Edit profile request started for freelancer with ID: {FreelancerId}", freelancerId);

                var result = await _freelancerAuthService.UpdateProfileAsync(freelancerId, freelancerProfileEditDto);

                //if (result.IsSuccess)
                //{
                //    _logger.LogInformation("Freelancer profile updated successfully for ID: {FreelancerId}", freelancerId);
                //    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                //}

                //_logger.LogWarning("Failed to edit freelancer profile for ID: {FreelancerId}. Message: {Message}", freelancerId, result.Message);
                //return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing freelancer profile for ID: {FreelancerId}", freelancerId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion


        #endregion


    }
}
