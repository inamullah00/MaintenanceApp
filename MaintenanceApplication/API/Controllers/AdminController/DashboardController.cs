using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Interfaces.ServiceInterfaces.DashboardInterfaces.AdminOrderInterafces;
using Maintenance.Application.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Maintenance.API.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #region Get All Orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _orderService.GetAllOrdersAsync(cancellationToken);
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

        #region Get Order by ID
        [HttpGet("Order/{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id,CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { StatusCode = 400, Message = "Invalid or empty order ID." });

                var result = await _orderService.GetOrderByIdAsync(id, cancellationToken);

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

        #region Create Order
        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto createOrderDto,CancellationToken cancellationToken)
        {
            try
            {
                if (createOrderDto == null)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data." });
                }

                var result = await _orderService.CreateOrderAsync(createOrderDto, cancellationToken);

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

        #region Assign Order
        [HttpPut("{id:guid}/assign")]
        public async Task<IActionResult> AssignOrder(Guid id, [FromBody] AssignOrderRequestDto assignOrderDto,CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || assignOrderDto == null)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data." });
                }
                var result = await _orderService.AssignOrderAsync(id, assignOrderDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion

        #region Update Order Status
        [HttpPut("{id:guid}/update-status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || updateOrderStatusDto == null)
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data." });

                var result = await _orderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion

        #region Resolve Order Dispute
        [HttpPost("{id:guid}/resolve-dispute")]
        public async Task<IActionResult> ResolveOrderDispute(Guid id, [FromBody] ResolveDisputeDto resolveDisputeDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || resolveDisputeDto == null)
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data." });

                var result = await _orderService.ResolveDisputeAsync(id, resolveDisputeDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion

    }
}
