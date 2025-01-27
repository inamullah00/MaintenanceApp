using Application.Dto_s.UserDto_s;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.UserDto_s;
using Maintenance.Application.Services.Account;
using Maintenance.Application.Services.Account.Filter;
using Maintenance.Application.Services.Account.Specification;
using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {

        #region Constructor & Fields
      
        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public UserAuthController(IServiceManager serviceManager )
        {
            _serviceManager = serviceManager;
        }
        #endregion

        #region Signup

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(CreateAdminRegistrationRequestDto request)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.RegisterAsync(request);

                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Login
       
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.LoginAsync(request);

                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                return Ok(new { Token = result.Token, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
                var result = await _serviceManager.RegisterationService.LogoutAsync();
                if (!result.Success)
                {
                    return BadRequest("Logout failed.");
                }

                return Ok(new { Message = "User logged out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Forgot-password
        [HttpPost]
        [Route("Forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto requestDto)
        {
            try
            {
         
                // Call the ForgotPasswordAsync method in the service
                var result = await _serviceManager.RegisterationService.ForgotPasswordAsync(requestDto.Email);
                if (!result.Success)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new { Message = "Password reset email has been sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion

        #region Reset-password
        [HttpPost]
        [Route("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDto)
        {
            try
            {
                if(requestDto.NewPassword == null || requestDto.NewPassword != requestDto.ConfirmPassword)
                {
                    return BadRequest("Password Miss Match");
                }

                var result = await _serviceManager.RegisterationService.ResetPasswordAsync(requestDto.Email, requestDto.NewPassword);
                if (!result.Success)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new { Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Validate-Otp
        [HttpPost]
        [Route("Validate-Otp")]
        public async Task<IActionResult> ValidateOtp([FromBody] string otp)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.ValidateOtpAsync(otp);

                if (!result.Success)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new { Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region User & Account Management

        #region List-OF-Users
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetAllUsers(string? Keyword = "")
        {
            try
            {
                UserSearchList Specification = new(Keyword);
                var result = await _serviceManager.RegisterationService.UsersAsync(Specification);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #region List-OF-Paginated-Users
        [HttpPost]
        [Route("Users-Paginated")]
        public async Task<IActionResult> GetAllUsersPaginatedAsync(UserTableFilter filter)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.UsersPaginatedAsync(filter);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #region User-Details
        [HttpGet]
        [Route("User-Details/Id:Guid")]
        public async Task<IActionResult> UserDetails(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "User ID cannot be empty."
                    });
                }

                UserSearchList Specification = new UserSearchList(Id.ToString());
                var result = await _serviceManager.RegisterationService.UserDetailsAsync(Specification);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #region Block-User
      
        [HttpPost]
        [Route("Block-User/{UserId:guid}")]
        public async Task<IActionResult> BlockUser(Guid UserId)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.BlockUserAsync(UserId);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #region UnBlock-User

        [HttpPost]
        [Route("UnBlock-User/{UserId:guid}")]
        public async Task<IActionResult> UnBlockUser(Guid UserId)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.UnBlockUserAsync(UserId);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #region Edit-User-Profile

        [HttpPut]
        [Route("Edit-User-Profile/{UserId:guid}")]
        public async Task<IActionResult> EditUserProfile(Guid UserId, [FromBody] UserProfileEditDto userProfileEditDto)
        {
            try
            {
                if (userProfileEditDto == null || !ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid profile data."
                    });
                }

                var result = await _serviceManager.RegisterationService.EditUserProfileAsync(UserId, userProfileEditDto);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        #endregion

        #endregion

    }
}
