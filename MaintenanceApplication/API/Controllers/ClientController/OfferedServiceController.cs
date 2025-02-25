using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.AddressDtos;
using Maintenance.Application.Dto_s.ClientDto_s.ClientServiceDto;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Services.ClientPayment;
using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferedServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<OfferedServiceController> _logger;

        public OfferedServiceController( IServiceManager serviceManager, ILogger<OfferedServiceController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #region List-OF-Services
        [HttpGet("Services")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {

                var result = await _serviceManager.OfferedServices.GetServicesAsync();
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
                return StatusCode(HttpResponseCodes.InternalServerError, new
                {
                    StatusCode = HttpResponseCodes.InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }

        #endregion

        #region Single-Service
        [HttpGet("Service/{Id:guid}")]
        public async Task<IActionResult> GetServiceById(Guid Id)
        {
            try
            {

                var result = await _serviceManager.OfferedServices.GetServiceAsync(Id);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }

        #endregion

        #region Add-Service
        [HttpPost("Service")]
        public async Task<IActionResult> AddService([FromForm] OfferedServiceRequestDto serviceRequestDto)
        {
            try
            {

                var result = await _serviceManager.OfferedServices.AddServiceAsync(serviceRequestDto);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }

        #endregion

        #region Update-Service
        [HttpPut("Service/{Id:guid}")]
        public async Task<IActionResult> UpdateService([FromBody] OfferedUpdateRequestDto serviceRequestDto, Guid Id)
        {
            try
            {

                var result = await _serviceManager.OfferedServices.UpdateServiceAsync(Id, serviceRequestDto);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Delete-Service
        [HttpDelete("Service/{Id:guid}")]
        public async Task<IActionResult> DeleteService(Guid Id)
        {
            try
            {

                var result = await _serviceManager.OfferedServices.DeleteServiceAsync(Id);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }

        #endregion

        #region Notify Relavant Freelancers When Client Post thier Service
        [HttpPost("Notifications/Freelancers")]
        public async Task<IActionResult> NotifyFreelancers([FromBody] NotificationRequestDto notificationDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to notify freelancers.");
            try
            {
                var result = await _serviceManager.NotificationService.SendNotificationAsync(notificationDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Freelancers notified successfully.");
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message
                    });
                }

                _logger.LogWarning("Failed to notify freelancers. Status Code: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while notifying freelancers.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }
        #endregion

        #region Process Payment
        [HttpPost("Payments")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto paymentDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing payment.");
            try
            {
                var result = await _serviceManager.PaymentService.ProcessPaymentAsync(paymentDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Payment processed successfully.");
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                _logger.LogWarning("Payment processing failed. Status Code: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing payment.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }
        #endregion



        #region Client Location/Address Api's



        #region Save-Address
        [HttpPost("Save-Address")]
        public async Task<IActionResult> SaveAddress([FromBody] ClientAddressRequestDto addressDto)
        {
            try
            {
                var result = await _serviceManager.OfferedServices.SaveAddressAsync(addressDto);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }
        #endregion

        #region Get-Saved-Addresses
        [HttpGet("Get-Saved-Addresses/{clientId}")]
        public async Task<IActionResult> GetSavedAddresses(Guid clientId)
        {
            try
            {
                var result = await _serviceManager.OfferedServices.GetSavedAddressesAsync(clientId);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }
        #endregion

        #region Delete-Address
        [HttpDelete("Delete-Address/{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            try
            {
                var result = await _serviceManager.OfferedServices.DeleteAddressAsync(addressId);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message
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
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"Internal Server Error: {ex.Message}"
                });
            }
        }
        #endregion




        #endregion
    }
}
