using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IServiceManager _serviceManager;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IServiceManager serviceManager, ILogger<DashboardController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #region Order Management

        #region Get All Orders

        [HttpGet("Order")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken, string Keyword = "")
        {

            _logger.LogInformation("GetAllOrders called with Keyword: {Keyword}", Keyword);

            try
            {
                var result = await _serviceManager.OrderService.GetAllOrdersAsync(cancellationToken, Keyword);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved orders.");

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to retrieve orders. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all orders.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Get Order by ID
        [HttpGet("Order/{Id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid Id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetOrderById called for Order ID: {Id}", Id);

            try
            {

                var result = await _serviceManager.OrderService.GetOrderByIdAsync(Id, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved order with ID: {Id}.", Id);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to retrieve order with ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", Id, result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching order with ID: {Id}.", Id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Create Order
        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto createOrderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateOrder called with request: {@CreateOrderRequestDto}", createOrderDto);

            try
            {

                var result = await _serviceManager.OrderService.CreateOrderAsync(createOrderDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully created order.");

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to create order. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Assign Order
        [HttpPut("{id:guid}/assign")]
        public async Task<IActionResult> AssignOrder(Guid id, [FromBody] AssignOrderRequestDto assignOrderDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AssignOrder called for Order ID: {Id} with request: {@AssignOrderRequestDto}", id, assignOrderDto);

            try
            {
                var result = await _serviceManager.OrderService.AssignOrderAsync(id, assignOrderDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully assigned order with ID: {Id}.", id);
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message });
                }

                _logger.LogWarning("Failed to assign order with ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while assigning order with ID: {Id}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Update Order Status
        [HttpPut("{id:guid}/update-status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateOrderStatus called for Order ID: {Id} with request: {@UpdateOrderStatusDto}", id, updateOrderStatusDto);

            try
            {
                var result = await _serviceManager.OrderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully updated order status for Order ID: {Id}.", id);
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message });
                }

                _logger.LogWarning("Failed to update order status for Order ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Success = false, Message = result.Message });
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

        #endregion

        #region Order Dispute Management

        #region Get All Disputes
        [HttpGet("Disputes")]
        public async Task<IActionResult> GetAllDisputes(CancellationToken cancellationToken, string Keyword = "")
        {
            _logger.LogInformation("GetAllDisputes called with Keyword: {Keyword}", Keyword);

            try
            {
                var result = await _serviceManager.DisputeService.GetAllDisputesAsync(cancellationToken, Keyword);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved all disputes.");

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to retrieve all disputes. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all disputes.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Get Dispute by ID
        [HttpGet("Dispute/{id:guid}")]
        public async Task<IActionResult> GetDisputeById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDisputeById called for Dispute ID: {Id}", id);

            try
            {
                var result = await _serviceManager.DisputeService.GetDisputeByIdAsync(id, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved dispute with ID: {Id}.", id);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to retrieve dispute with ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching dispute with ID: {Id}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Create Dispute
        [HttpPost("Dispute")]
        public async Task<IActionResult> CreateDispute([FromBody] CreateDisputeRequest createDisputeDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateDispute called with request: {@CreateDisputeRequest}", createDisputeDto);

            try
            {
                var result = await _serviceManager.DisputeService.CreateDisputeAsync(createDisputeDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully created dispute.");

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to create dispute. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating dispute.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Resolve Dispute
        [HttpPost("Resolve-Dispute/{id:guid}")]
        public async Task<IActionResult> ResolveDispute(Guid id, [FromBody] CreateDisputeResolveDto DisputeResolveRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ResolveDispute called for Dispute ID: {Id} with request: {@DisputeResolveRequest}", id, DisputeResolveRequest);

            try
            {

                var result = await _serviceManager.DisputeService.ResolveDisputeAsync(id, DisputeResolveRequest, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully resolved dispute with ID: {Id}.", id);
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message });
                }

                _logger.LogWarning("Failed to resolve dispute with ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resolving dispute with ID: {Id}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Delete Dispute
        [HttpDelete("Dispute/{id:guid}")]
        public async Task<IActionResult> DeleteDispute(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteDispute called for Dispute ID: {Id}", id);

            try
            {
                var result = await _serviceManager.DisputeService.DeleteDisputeAsync(id, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully deleted dispute with ID: {Id}.", id);
                    return Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message });
                }

                _logger.LogWarning("Failed to delete dispute with ID: {Id}. StatusCode: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting dispute with ID: {Id}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #endregion

        #region Content Management

        #region Get All Content
        [HttpGet("Content")]
        public async Task<IActionResult> GetAllContent(CancellationToken cancellationToken, string Keyword = "")
        {
            try
            {
                var result = await _serviceManager.ContentService.GetAllContentAsync(cancellationToken, Keyword);
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

        #region Get Content by ID
        [HttpGet("Content/{Id:Guid}")]
        public async Task<IActionResult> GetContentById(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.ContentService.GetContentByIdAsync(Id, cancellationToken);

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

        #region Create Content
        [HttpPost("Content")]
        public async Task<IActionResult> CreateContent([FromBody] CreateContentRequestDto createContentDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.ContentService.CreateContentAsync(createContentDto, cancellationToken);

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

        #region Update Content
        [HttpPut("Content/{id:Guid}")]
        public async Task<IActionResult> UpdateContent(Guid id, [FromBody] UpdateContentRequestDto updateContentDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.ContentService.UpdateContentAsync(id, updateContentDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Success = false, Message = result.Message });
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

        #region Delete Content
        [HttpDelete("Content/{id:Guid}")]
        public async Task<IActionResult> DeleteContent(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.ContentService.DeleteContentAsync(id, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Success = false, Message = result.Message });
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

        #endregion

        #region Feedback Management

        #region Get All Feedback
        [HttpGet("Feedback")]
        public async Task<IActionResult> GetAllFeedback(CancellationToken cancellationToken, string keyword = "")
        {
            try
            {
                var result = await _serviceManager.FeedbackService.GetAllFeedbackAsync(cancellationToken, keyword);
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

        #region Get Feedback by ID
        [HttpGet("Feedback/{id:Guid}")]
        public async Task<IActionResult> GetFeedbackById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.FeedbackService.GetFeedbackRatingForFreelancerAsync(id, cancellationToken);

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

        #region Create Feedback
        [HttpPost("Feedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequestDto createFeedbackDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.FeedbackService.CreateFeedbackAsync(createFeedbackDto, cancellationToken);

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

        #region Update Feedback
        [HttpPut("Feedback/{id:Guid}")]
        public async Task<IActionResult> UpdateFeedback(Guid id, [FromBody] UpdateFeedbackRequestDto updateFeedbackDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.FeedbackService.UpdateFeedbackAsync(id, updateFeedbackDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Success = false, Message = result.Message });
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

        #region Delete Feedback
        [HttpDelete("Feedback/{id:Guid}")]
        public async Task<IActionResult> DeleteFeedback(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.FeedbackService.DeleteFeedbackAsync(id, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = StatusCodes.Status200OK, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Success = false, Message = result.Message });
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

        #endregion

        #region 1.5	Order Limit and Performance Reporting

        #region Set Freelancer Order Limit
        [HttpPut("freelancer/{freelancerId}/set-order-limit")]
        public async Task<IActionResult> SetOrderLimitAsync(string freelancerId, [FromBody] SetOrderLimitRequestDto RequestDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.AdminFreelancerService.SetOrderLimitAsync(freelancerId, RequestDto, cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Message = "Order limit set successfully."
                    });
                }

                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
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

        #region Can Freelancer Accept New Order
        [HttpGet("freelancer/{freelancerId}/can-accept-order")]
        public async Task<IActionResult> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.AdminFreelancerService.CanAcceptNewOrderAsync(freelancerId, cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
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

        #region API endpoint to generate the performance report for a freelancer
        [HttpGet("performance-report/{freelancerId}")]
        public async Task<IActionResult> GenerateFreelancerPerformanceReport(string freelancerId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.AdminFreelancerService.GeneratePerformanceReportAsync(freelancerId, startDate, endDate, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Message = "Freelancer performance report generated successfully.",
                        Data = result.Value
                    });
                }

                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
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

        #region Generate Top Performers Report
        [HttpGet("top-performers/{month:int}")]
        public async Task<IActionResult> GenerateTopPerformersReport(int month, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceManager.AdminFreelancerService.GenerateTopPerformersReportAsync(month, cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Message = "Top performers report generated successfully.",
                        Data = result.Value
                    });
                }

                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
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
