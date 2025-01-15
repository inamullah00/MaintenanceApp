using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.FeedbackDto;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.DashboardDtos.ContentDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos;
using Maintenance.Application.Dto_s.DashboardDtos.DisputeDtos.DisputeResolvedDto;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.FeedbackSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification;
using Maintenance.Application.Wrapper;
using Maintenance.Infrastructure.Repositories.ServiceImplemention.DashboardServiceImplemention;
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
        private readonly IContentService _contentService;

        private readonly IAdminFreelancerService _adminFreelancerService;
        private readonly IFeedbackService _feedbackService;

        public DashboardController(IOrderService orderService , IDisputeService disputeService ,
            IContentService contentService ,IAdminFreelancerService adminFreelancerService , IFeedbackService feedbackService)
        {
            _orderService = orderService;
            _disputeService = disputeService;
            _contentService = contentService;
            _adminFreelancerService = adminFreelancerService;
            _feedbackService = feedbackService;
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

        #region Content Management

        #region Get All Content
        [HttpGet("Content")]
        public async Task<IActionResult> GetAllContent(CancellationToken cancellationToken, string Keyword = "")
        {
            try
            {
                var result = await _contentService.GetAllContentAsync(cancellationToken, Keyword);
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
                var result = await _contentService.GetContentByIdAsync(Id, cancellationToken);

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
                var result = await _contentService.CreateContentAsync(createContentDto, cancellationToken);

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
                var result = await _contentService.UpdateContentAsync(id, updateContentDto, cancellationToken);

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
                var result = await _contentService.DeleteContentAsync(id, cancellationToken);

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
                var result = await _feedbackService.GetAllFeedbackAsync(cancellationToken, keyword);
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
                var result = await _feedbackService.GetFeedbackByIdAsync(id, cancellationToken);

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
                var result = await _feedbackService.CreateFeedbackAsync(createFeedbackDto, cancellationToken);

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
                var result = await _feedbackService.UpdateFeedbackAsync(id, updateFeedbackDto, cancellationToken);

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
                var result = await _feedbackService.DeleteFeedbackAsync(id, cancellationToken);

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
                var result = await _adminFreelancerService.SetOrderLimitAsync(freelancerId, RequestDto, cancellationToken);
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
                var result = await _adminFreelancerService.CanAcceptNewOrderAsync(freelancerId, cancellationToken);
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
                var result = await _adminFreelancerService.GeneratePerformanceReportAsync(freelancerId, startDate, endDate, cancellationToken);

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
                var result = await _adminFreelancerService.GenerateTopPerformersReportAsync(month, cancellationToken);
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



    }
}
