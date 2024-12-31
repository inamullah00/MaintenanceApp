using Application.Dto_s.UserDto_s;
using Application.Interfaces.ServiceInterfaces.RegisterationInterfaces;
using AutoMapper;
using Domain.Entity.UserEntities;
using Infrastructure.Repositories.ServiceImplemention;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        #region Constructor & Fields
        private readonly IRegisterationService _registerationService;
        private readonly IMapper _mapper;

        public AuthController( IRegisterationService registerationService)
        {
        
            _registerationService = registerationService;
          
        }
        #endregion

        #region Signup

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(RegistrationRequestDto request)
        {
            try
            {
                var result = await _registerationService.RegisterAsync(request);

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
                var result = await _registerationService.LoginAsync(request);

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
                var result = await _registerationService.LogoutAsync();
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
                var result = await _registerationService.ForgotPasswordAsync(requestDto.Email);
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

                var result = await _registerationService.ResetPasswordAsync(requestDto.Email, requestDto.NewPassword);
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
                var result = await _registerationService.ValidateOtpAsync(otp);

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
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {

                var result = await _registerationService.UsersAsync();
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

                var result = await _registerationService.UserDetailsAsync(Id);
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
                var result = await _registerationService.BlockUserAsync(UserId);

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
                var result = await _registerationService.UnBlockUserAsync(UserId);

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
