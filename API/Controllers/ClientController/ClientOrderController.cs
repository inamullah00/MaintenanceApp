﻿using Maintenance.Application.Common.Constants;
using Maintenance.Application.Common.Utility;
using Maintenance.Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<ClientOrderController> _logger;
        public ClientOrderController(IServiceManager serviceManager , ILogger<ClientOrderController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        #region Get All Orders
        [HttpGet("Order")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken, string Keyword = "")
        {
            try
            {
                _logger.LogInformation("Fetching all orders with keyword: {Keyword}", Keyword);

                var result = await _serviceManager.OrderService.GetAllOrdersAsync(cancellationToken, Keyword);
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
                _logger.LogWarning("Failed to fetch orders. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching orders.");
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
            try
            {
                _logger.LogInformation("Fetching order with ID: {Id}", Id);
                var result = await _serviceManager.OrderService.GetOrderByIdAsync(Id, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully fetched order with ID: {Id}", Id);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Order with ID: {Id} not found. StatusCode: {StatusCode}, Message: {Message}", Id, result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching order with ID: {Id}", Id);
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
        [Route("Order/AssignOrder-To-Freelancer")]
        public async Task<IActionResult> AssignOrder([FromBody] CreateOrderRequestDto createOrderDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating a new order.");
                var result = await _serviceManager.OrderService.CreateOrderAsync(createOrderDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order created successfully.");
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
                _logger.LogError(ex, "An error occurred while creating a new order.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.InnerException}"
                });
            }
        }
        #endregion

        #region Update Order Status
        [HttpPut("Order/update-Order-status/{id:guid}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating order status for Order ID: {OrderId}", id);
                var result = await _serviceManager.OrderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);
                _logger.LogInformation("Order status updated successfully for Order ID: {OrderId}", id);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order status updated successfully for Order ID: {OrderId}", id);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to update order status for Order ID: {OrderId}. Reason: {Message}", id, result.Message);
                return BadRequest(new
                {
                    StatusCode = HttpResponseCodes.BadRequest,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating order status for Order ID: {OrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region  Client Reviews and Approves the Work
        [HttpPut("Orders/Approve-Client-Work/{orderId:guid}")]
        public async Task<IActionResult> ApproveOrder(Guid orderId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client attempting to approve order with Order ID: {OrderId}", orderId);
                var result = await _serviceManager.OrderService.ApproveOrderAsync(orderId, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order approved successfully. Order ID: {OrderId}", orderId);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value // Updated order details with approved status
                    });
                }
                _logger.LogWarning("Failed to approve order with Order ID: {OrderId}. Reason: {Message}", orderId, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while approving order with Order ID: {OrderId}", orderId);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Client Rejects the Work 

        [HttpPut("Orders/{orderId:guid}/Reject")]
        public async Task<IActionResult> RejectOrder(Guid orderId, [FromBody] RejectOrderRequestDTO rejectOrderDTO,CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client attempting to reject order with Order ID: {OrderId}. Reason: {Reason}", orderId, rejectOrderDTO.OrderStatus);
                var result = await _serviceManager.OrderService.RejectOrderAsync(orderId, rejectOrderDTO, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order rejected successfully. Order ID: {OrderId}", orderId);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value // Order status updated to rejected with comments
                    });
                }
                _logger.LogWarning("Failed to reject order with Order ID: {OrderId}. Reason: {Message}", orderId, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while rejecting order with Order ID: {OrderId}", orderId);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion


        #region Get All Pending Services for Client (Awaiting Bid Acceptance)
        [HttpGet("client/pending-services")]
        public async Task<IActionResult> GetAllPendingServices(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all pending services for client");
            try
            {
                var result = await _serviceManager.OrderService.GetPendingClientServicesAsync(cancellationToken);

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
                _logger.LogError(ex, "Error fetching pending services for client");
                return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
            }
        }
        #endregion

        #region Filter-Freelancer-Orders

        [HttpGet("Filter-Freelancer-Orders")]
        public async Task<IActionResult> GetClientOrdersByStatus([FromQuery] OrderStatus orderStatus, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching client orders with status: {Status}", orderStatus);

            var result = await _serviceManager.OrderService.GetClientOrdersByStatusAsync(orderStatus, cancellationToken);

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

            _logger.LogWarning("No client orders found for status: {Status}", orderStatus);
            return StatusCode(result.StatusCode, new
            {
                StatusCode = result.StatusCode,
                Success = false,
                Message = result.Message
            });
        }
        #endregion

        #region No OF Bids

        #region Get Total No of Bids by Freelancers

        [HttpGet("TotalBidsByFreelancers/{OfferedServiceId}:Guid")]
        public async Task<IActionResult> GetBidsByFreelancer(Guid OfferedServiceId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetBidsByFreelancer called");

            try
            {
                var result = await _serviceManager.FreelancerService.GetBidsByFreelancerAsync(OfferedServiceId , cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully fetched bids ");
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                _logger.LogWarning("Failed to fetch bids. Message: {Message}", result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching bids ");

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

        #region Check Freelancer or Company Details Screen

        #region Get Freelancer Details
        [HttpGet("Check-Freelancer-Details/{freelancerId}")]
        public async Task<IActionResult> GetFreelancerDetails(Guid freelancerId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching freelancer details for ID: {FreelancerId}", freelancerId);

                var result = await _serviceManager.FreelancerService.GetFreelancerDetailsAsync(freelancerId, cancellationToken);

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

                _logger.LogWarning("Freelancer not found. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching freelancer details.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion



        #region Get Company Details
        [HttpGet("Check-Company-Details/{companyId}")]
        public async Task<IActionResult> GetCompanyDetails(Guid companyId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching company details for ID: {CompanyId}", companyId);

                var result = await _serviceManager.FreelancerService.GetCompanyDetailsAsync(companyId, cancellationToken);

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

                _logger.LogWarning("Company not found. StatusCode: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);
                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching company details.");
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
    }
}
