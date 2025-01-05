using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Services.Admin;
using Maintenance.Application.Services.Admin.Specification;
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

        #region Order Management

        #region Get All Orders
        [HttpGet("Order")]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken , string Keyword = "")
        {
            try
            {
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

        #region Get Order by ID
        [HttpGet("Order/{Id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
               
                var result = await _orderService.GetOrderByIdAsync(Id,cancellationToken);

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

        #region Create Order
        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto createOrderDto,CancellationToken cancellationToken)
        {
            try
            {

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
        public async Task<IActionResult> AssignOrder(Guid id, [FromBody] AssignOrderRequestDto assignOrderDto,CancellationToken cancellationToken)
        {
            try
            {
                var result = await _orderService.AssignOrderAsync(id, assignOrderDto, cancellationToken);

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

        #region Update Order Status
        [HttpPut("{id:guid}/update-status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);

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

        #region Resolve Order Dispute
        [HttpPost("{id:guid}/resolve-dispute")]
        public async Task<IActionResult> ResolveOrderDispute(Guid id, [FromBody] ResolveDisputeDto resolveDisputeDto, CancellationToken cancellationToken)
        {
            try
            {
                if (id == Guid.Empty || resolveDisputeDto == null)
                    return BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Message = ErrorMessages.ValidationError });

                var result = await _orderService.ResolveDisputeAsync(id, resolveDisputeDto, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = HttpResponseCodes.OK, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = HttpResponseCodes.BadRequest, Success = false, Message = result.Message });
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

        #endregion

    }
}
