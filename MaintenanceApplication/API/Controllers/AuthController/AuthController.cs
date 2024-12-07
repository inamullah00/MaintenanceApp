using Application.Dto_s.UserDto_s;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entity.UserEntities;
using Infrastructure.Repositories.ServiceImplemention;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IRegisterationService _registerationService;

        public AuthController( IRegisterationService registerationService)
        {
        
            _registerationService = registerationService;
        }

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


    }
}
