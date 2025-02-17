using Application.Dto_s.UserDto_s;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.UserDto_s.ClientAuthDtos;
using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAuthController : ControllerBase
    {

        #region Constructor & Fields

        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<ClientAuthController> _logger;

        public ClientAuthController(IServiceManager serviceManager, ILogger<ClientAuthController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #endregion


        #region Client Registration

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] ClientRegistrationDto request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Signup request started for client: {FullName}", request.FullName);

            try
            {
                var result = await _serviceManager.ClientAuthService.RegisterClientAsync(request, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Signup successful for client: {FullName}", request.FullName);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                _logger.LogWarning("Signup failed for client: {FullName}, Message: {Message}", request.FullName, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Signup process for client: {FullName}", request.FullName);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.InnerException?.Message ?? ex.Message}"
                });
            }
        }

        #endregion

        #region Client Login

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(ClientLoginDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client login request started for email: {Email}", request.Email);

                var result = await _serviceManager.ClientAuthService.LoginAsync(request, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client login failed for email: {Email}, Message: {Message}", request.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Client login successful for email: {Email}", request.Email);

                var response = result.Value;

                return Ok(new
                {
                    Token = response.Token,
                    //RefreshToken = response.RefreshToken,
                    ClientId = response.ClientId,
                    FullName = response.FullName,
                    Email = response.Email,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client login process for email: {Email}", request.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Client Logout

        [HttpPost]
        [Route("Logout/{ClientId:guid}")]
        public async Task<IActionResult> Logout(Guid ClientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client logout request started.");

                var result = await _serviceManager.ClientAuthService.LogoutAsync(ClientId, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client logout failed.");
                    return BadRequest("Logout failed.");
                }

                _logger.LogInformation("Client logout successful.");
                return Ok(new { Message = "Client logged out successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client logout process.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Forgot Password

        [HttpPost]
        [Route("client/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client Forgot Password request started for email: {Email}", requestDto.Email);

                var result = await _serviceManager.ClientAuthService.ForgotPasswordAsync(requestDto.Email, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client Forgot Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Client Forgot Password email sent successfully to: {Email}", requestDto.Email);
                return Ok(new { Message = "Password reset email has been sent successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client Forgot Password process for email: {Email}", requestDto.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Reset Password

        [HttpPost]
        [Route("client/reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client Reset Password request started for email: {Email}", requestDto.Email);

                if (string.IsNullOrWhiteSpace(requestDto.NewPassword) || requestDto.NewPassword != requestDto.ConfirmPassword)
                {
                    return BadRequest("Password mismatch");
                }

                var result = await _serviceManager.ClientAuthService.ResetPasswordAsync(requestDto, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client Reset Password failed for email: {Email}, Message: {Message}", requestDto.Email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Client Password reset successful for email: {Email}", requestDto.Email);
                return Ok(new { Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client Reset Password process for email: {Email}", requestDto.Email);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Validate OTP

        [HttpPost]
        [Route("client/validate-otp")]
        public async Task<IActionResult> ValidateOtp([FromBody] int otp, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client Validate OTP request started for OTP: {Otp}", otp);

                var result = await _serviceManager.ClientAuthService.ValidateOtpAsync(otp, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client OTP validation failed for OTP: {Otp}, Message: {Message}", otp, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Client OTP validation successful for OTP: {Otp}", otp);
                return Ok(new { Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client OTP validation process.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Resend OTP

        [HttpPost]
        [Route("client/resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] string email, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client Resend OTP request started for email: {Email}", email);

                var result = await _serviceManager.ClientAuthService.ResendOtpAsync(email, cancellationToken);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Client OTP resend failed for email: {Email}, Message: {Message}", email, result.Message);
                    return BadRequest(result.Message);
                }

                _logger.LogInformation("Client OTP resent successfully for email: {Email}", email);
                return Ok(new { Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Client OTP resend process.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Edit-Client-Profile

        [HttpPut]
        [Route("Edit-Client-Profile/{ClientId:guid}")]
        public async Task<IActionResult> EditClientProfile(Guid ClientId, [FromBody] ClientEditProfileDto clientProfileEditDto, CancellationToken cancellationToken)
        {
            try
            {
                if (clientProfileEditDto == null || !ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, Success = false, Message = "Invalid profile data." });
                }

                _logger.LogInformation("Edit profile request started for client with ID: {ClientId}", ClientId);

                var result = await _serviceManager.ClientAuthService.UpdateProfileAsync(ClientId, clientProfileEditDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Client profile updated successfully for ID: {ClientId}", ClientId);
                    return Ok(new { StatusCode = result.StatusCode, Success = true, Message = result.Message, Data = result.Value });
                }

                _logger.LogWarning("Failed to edit client profile for ID: {ClientId}. Message: {Message}", ClientId, result.Message);
                return StatusCode(result.StatusCode, new { StatusCode = result.StatusCode, Success = false, Message = result.Message });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing client profile for ID: {ClientId}", ClientId);
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion



    }
}


