using AutoMapper;
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

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(ClientRegistrationDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("SignUp request started for client: {FullName}", request.FullName);

                //var result = await _serviceManager.ClientAuthService.RegisterClientAsync(request, cancellationToken).ConfigureAwait(false);

                //if (result != null)
                //{
                //    _logger.LogWarning("SignUp failed for client: {FullName}, Message: {Message}", request.FullName, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("SignUp successful for client: {FullName}", request.FullName);
                //return Ok(new { Message = result.Message });

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during SignUp process for client.");
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion


        #region Login

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(ClientLoginDto request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Login request started for email: {Email}", request.Email);

                //var result = await _serviceManager.ClientAuthService.LoginAsync(request, cancellationToken);

                //if (!result.IsSuccess)
                //{
                //    _logger.LogWarning("Login failed for email: {Email}, Message: {Message}", request.Email, result.Message);
                //    return BadRequest(result.Message);
                //}

                //_logger.LogInformation("Login successful for email: {Email}", request.Email);

                //// Extracting the response DTO
                //var response = result.Value;

                //return Ok(new
                //{
                //    Token = response.Token,
                //    RefreshToken = response.RefreshToken,
                //    ClientId = response.ClientId,
                //    FullName = response.FullName,
                //    Email = response.Email,
                //    ProfilePicture = response.ProfilePicture,
                //    Message = result.Message
                //});
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
        [Route("Logout/{ClientId:guid}")]
        public async Task<IActionResult> Logout(Guid ClientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Logout request started for ClientId: {ClientId}", ClientId);

                //var result = await _serviceManager.ClientAuthService.LogoutAsync(ClientId, cancellationToken);

                //if (!result.IsSuccess)
                //{
                //    _logger.LogWarning("Logout failed for ClientId: {ClientId}", ClientId);
                //    return BadRequest("Logout failed.");
                //}

                //_logger.LogInformation("Logout successful for ClientId: {ClientId}", ClientId);
                //return Ok(new { Message = "User logged out successfully." });

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Logout process for ClientId: {ClientId}", ClientId);
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion


    }
}
