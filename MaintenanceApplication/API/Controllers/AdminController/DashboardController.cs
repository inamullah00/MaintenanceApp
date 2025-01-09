using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification;
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
        private readonly IDisputeService _disputeService;

        public DashboardController(IOrderService orderService , IDisputeService disputeService)
        {
            _orderService = orderService;
            _disputeService = disputeService;
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

        #endregion

        #region Order Dispute Management

        #region Get All Disputes
        [HttpGet("Disputes")]
        public async Task<IActionResult> GetAllDisputes(CancellationToken cancellationToken, string Keyword = "")
        {
            try
            {
                var result = await _disputeService.GetAllDisputesAsync(cancellationToken, Keyword);
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

        #region Get Dispute by ID
        [HttpGet("Dispute/{id:guid}")]
        public async Task<IActionResult> GetDisputeById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _disputeService.GetDisputeByIdAsync(id, cancellationToken);

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

        #region Create Dispute
        [HttpPost("Dispute")]
        public async Task<IActionResult> CreateDispute([FromBody] CreateDisputeRequest createDisputeDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _disputeService.CreateDisputeAsync(createDisputeDto, cancellationToken);

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

        #region Resolve Dispute
        [HttpPost("Resolve-Dispute/{id:guid}")]
        public async Task<IActionResult> ResolveDispute(Guid id, [FromBody] CreateDisputeResolveDto DisputeResolveRequest, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _disputeService.ResolveDisputeAsync(id, DisputeResolveRequest, cancellationToken);

                return result.IsSuccess
                    ? Ok(new { StatusCode = HttpResponseCodes.OK, Success = true, Message = result.Message })
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

        #region Delete Dispute
        [HttpDelete("Dispute/{id:guid}")]
        public async Task<IActionResult> DeleteDispute(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _disputeService.DeleteDisputeAsync(id, cancellationToken);

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

    }
}
