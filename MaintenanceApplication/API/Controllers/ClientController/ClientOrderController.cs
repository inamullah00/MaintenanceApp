using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<ClientOrderController> _logger;
        public ClientOrderController(IOrderService orderService, ILogger<ClientOrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        #region Get All Orders
        [HttpGet("Order")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken, string Keyword = "")
        {
            try
            {
                _logger.LogInformation("Fetching all orders with keyword: {Keyword}", Keyword);

                var result = await _orderService.GetAllOrdersAsync(cancellationToken, Keyword);
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
                var result = await _orderService.GetOrderByIdAsync(Id, cancellationToken);

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
        [Route("Order")]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequestDto createOrderDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating a new order.");
                var result = await _orderService.CreateOrderAsync(createOrderDto, cancellationToken);

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
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Update Order Status
        [HttpPut("{id:guid}/update-status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating order status for Order ID: {OrderId}", id);
                var result = await _orderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);
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
        [HttpPut("Orders/{orderId:guid}/Approve")]
        public async Task<IActionResult> ApproveOrder(Guid orderId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client attempting to approve order with Order ID: {OrderId}", orderId);
                var result = await _orderService.ApproveOrderAsync(orderId, cancellationToken);

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
        public async Task<IActionResult> RejectOrder(Guid orderId, [FromBody] RejectOrderRequestDTO rejectOrderDTO, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Client attempting to reject order with Order ID: {OrderId}. Reason: {Reason}", orderId, rejectOrderDTO.OrderStatus);
                var result = await _orderService.RejectOrderAsync(orderId, rejectOrderDTO, cancellationToken);

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
    }
}
