using Maintenance.API.Controllers.AdminController;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly ILogger<DashboardController> _logger;

        public SettingsController(IServiceManager serviceManager, ILogger<DashboardController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #region Application Setting Endpoints
        [HttpPost]
        [Route("contact-us")]
        public async Task<IActionResult> CreateContactUs([FromForm] ContactUsRequestModel model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateContactUs called with request: {@ContactUsRequestModel}", model);

            if (model == null)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = ErrorMessages.InvalidOrEmpty
                });
            }

            var result = await _serviceManager.ContactUsService.CreateContactUsAsync(model, cancellationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully created contact us entry.");

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = true,
                    Message = result.Message,
                    Data = result.Value
                });
            }

            _logger.LogWarning("Failed to create contact us. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

            return StatusCode(result.StatusCode, new
            {
                StatusCode = result.StatusCode,
                Success = false,
                Message = result.Message
            });
        }

        [HttpGet("terms-and-conditions")]
        public async Task<IActionResult> GetTermsAndConditions()
        {
            _logger.LogInformation("Fetching terms and conditions");

            var result = await _serviceManager.ApplicationSettingService.GetTermsAndConditions();

            if (result == null)
            {
                _logger.LogWarning("Terms and conditions not found.");
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Terms and conditions not found."
                });
            }

            _logger.LogInformation("Successfully retrieved terms and conditions.");
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                Data = result
            });
        }

        [HttpGet("about-us")]
        public async Task<IActionResult> GetAboutUs()
        {
            _logger.LogInformation("Fetching About Us content");

            var result = await _serviceManager.ApplicationSettingService.GetAboutUs();

            if (result == null)
            {
                _logger.LogWarning("About Us content not found.");
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "About Us content not found."
                });
            }

            _logger.LogInformation("Successfully retrieved About Us.");
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                Data = result
            });
        }

        [HttpGet("privacy-policy")]
        public async Task<IActionResult> GetPrivacyPolicy()
        {
            _logger.LogInformation("Fetching Privacy Policy");

            var result = await _serviceManager.ApplicationSettingService.GetPrivacyPolicy();

            if (result == null)
            {
                _logger.LogWarning("Privacy Policy not found.");
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Privacy Policy not found."
                });
            }

            _logger.LogInformation("Successfully retrieved Privacy Policy.");
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Success = true,
                Data = result
            });
        }
    }
    #endregion
}

