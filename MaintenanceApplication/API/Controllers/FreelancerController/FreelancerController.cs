using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Common.Utility;
using Maintenance.Application.Dto_s.DashboardDtos.AdminOrderDtos;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.Freelance.Specification;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.FreelancerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<FreelancerController> _logger;

        public FreelancerController(IServiceManager serviceManager, ILogger<FreelancerController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }


        #region Get Total No of Bids by Freelancers
        [HttpGet("TotalBidsByFreelancers")]
        public async Task<IActionResult> GetBidsByFreelancer(CancellationToken cancellationToken, string? Keyword = "")
        {

            _logger.LogInformation("GetBidsByFreelancer called with Keyword: {Keyword}", Keyword);

            try
            {
               
                var result = await _serviceManager.FreelancerService.GetBidsByFreelancerAsync(cancellationToken,Keyword);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully fetched bids for Keyword: {Keyword}", Keyword);
                    //return Ok(new
                    //{
                    //    StatusCode = result.StatusCode,
                    //    Success = true,
                    //    Message = result.Message,
                    //    Data = result.Value
                    //});
                }
                _logger.LogWarning("Failed to fetch bids for Keyword: {Keyword}. Message: {Message}", Keyword, result.Message);


                return Helper.ProcessResult(result); // Using the static method
                //return StatusCode(result.StatusCode, new
                //{
                //    StatusCode = result.StatusCode,
                //    Success = false,
                //    Message = result.Message
                //});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching bids for Keyword: {Keyword}", Keyword);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Get Bids by Freelancer
        [HttpGet("Bids/{freelancerId:guid}")]
        public async Task<IActionResult> GetBidsByFreelancer(Guid freelancerId)
        {

            _logger.LogInformation("GetBidsByFreelancer called for FreelancerId: {FreelancerId}", freelancerId);

            try
            {               
                 var result = await _serviceManager.FreelancerService.GetBidsByFreelancerAsync(freelancerId);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully fetched bids for FreelancerId: {FreelancerId}", freelancerId);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                _logger.LogWarning("Failed to fetch bids for FreelancerId: {FreelancerId}. Message: {Message}", freelancerId, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching bids for FreelancerId: {FreelancerId}", freelancerId);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Submit Bid
        [HttpPost("Bids")]
        public async Task<IActionResult> SubmitBid([FromBody] BidRequestDto bidRequestDto)
        {
            _logger.LogInformation("SubmitBid called with BidDto: {BidDto}", bidRequestDto);
            try
            {
                var result = await _serviceManager.FreelancerService.SubmitBidAsync(bidRequestDto);

                if (result.IsSuccess)
                {

                    _logger.LogInformation("Bid submitted successfully. BidDto: {BidDto}", bidRequestDto);
                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }

                _logger.LogWarning("Failed to submit bid. BidDto: {BidDto}, Message: {Message}", bidRequestDto, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting bid. BidDto: {BidDto}", bidRequestDto);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Update Bid
        [HttpPut("Bids/{freelancerId:guid}")]
        public async Task<IActionResult> UpdateBid(Guid freelancerId,BidUpdateDto bidUpdateDto)
        {
            _logger.LogInformation("UpdateBid called for BidId: {BidId} with BidDto: {BidDto}", freelancerId, bidUpdateDto);

            try
            {
                var result = await _serviceManager.FreelancerService.UpdateBidAsync(bidUpdateDto ,freelancerId);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Bid updated successfully. BidId: {BidId}, BidDto: {BidDto}", freelancerId, bidUpdateDto);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to update bid. BidId: {BidId}, Message: {Message}", freelancerId, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating bid. BidId: {BidId}, BidDto: {BidDto}", freelancerId, bidUpdateDto);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Delete Bid
        [HttpDelete("Bids/{bidId:guid}")]
        public async Task<IActionResult> DeleteBid(Guid bidId)
        {
            _logger.LogInformation("DeleteBid called for BidId: {BidId}", bidId);

            try
            {
                var result = await _serviceManager.FreelancerService.DeleteBidAsync(bidId);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Bid deleted successfully. BidId: {BidId}", bidId);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to delete bid. BidId: {BidId}, Message: {Message}", bidId, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting bid. BidId: {BidId}", bidId);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Approve Bid Request by Client
        [HttpPatch("Bids/{id:guid}")]
        public async Task<IActionResult> ApproveBid(Guid id, [FromBody] ApproveBidRequestDto bidRequestDto)
        {
            _logger.LogInformation("Received request to approve bid with ID: {BidId}", id);

            try
            {
                var result = await _serviceManager.FreelancerService.ApproveBidAsync(id, bidRequestDto);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully approved bid with ID: {BidId}", id);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value
                    });
                }
                _logger.LogWarning("Failed to approve bid with ID: {BidId}. Status Code: {StatusCode}, Message: {Message}", id, result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while approving bid with ID: {BidId}", id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }
        #endregion

        #region Filter Freelancers by Bid Pricing and Rating
        [HttpGet("Freelancers/Filter")]
        public async Task<IActionResult> FilterFreelancers([FromQuery] FilterFreelancerRequestDto filterRequestDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to filter freelancers with criteria: {@FilterRequestDto}", filterRequestDto);

            try
            {
                var result = await _serviceManager.FreelancerService.FilterFreelancersAsync(filterRequestDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved {Count} freelancers matching the criteria.", result.Value.Count);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value // List of filtered freelancers
                    });
                }

                _logger.LogWarning("Failed to filter freelancers. Status Code: {StatusCode}, Message: {Message}", result.StatusCode, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while filtering freelancers.");

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
            _logger.LogInformation("Received request to update order status for Order ID: {OrderId}", id);

            try
            {
                var result = await _serviceManager.OrderService.UpdateOrderStatusAsync(id, updateOrderStatusDto, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully updated order status for Order ID: {OrderId}", id);

                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Message = result.Message
                    });
                 }
                
                    _logger.LogWarning("Failed to update order status for Order ID: {OrderId}. Message: {Message}", id, result.Message);

                    return BadRequest(
                        new { StatusCode = HttpResponseCodes.BadRequest,
                            Success = false,
                            Message = result.Message });
                
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

        #region Assigned Order To Freelancer and they start work on

        #region StartWork
        [HttpPut("Orders/{orderId:guid}/Start")]
        public async Task<IActionResult> StartWork(Guid orderId, [FromBody] UpdateOrderStatusDto startWorkDTO , CancellationToken cancellationToken)
        {

            _logger.LogInformation("StartWork endpoint called with OrderId: {OrderId}, Data: {@StartWorkDTO}", orderId, startWorkDTO);

            try
            {
                var result = await _serviceManager.OrderService.UpdateOrderStatusAsync(orderId, startWorkDTO, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order {OrderId} status updated successfully to 'In Progress'.", orderId);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value // Updated order details
                    });
                }

                _logger.LogWarning("Failed to update order {OrderId} status. Reason: {Reason}", orderId, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while starting work on Order {OrderId}.", orderId);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ErrorMessages.InternalServerError}: {ex.Message}"
                });
            }
        }

        #endregion

        #region MarkOrderAsCompleted

        [HttpPut("Orders/{orderId:guid}/Complete")]
        public async Task<IActionResult> MarkOrderAsCompleted(Guid orderId, [FromBody] CompleteWorkDTORequest completeWorkDTO , CancellationToken cancellationToken)
        {
            _logger.LogInformation("MarkOrderAsCompleted endpoint called with OrderId: {OrderId}, Data: {@CompleteWorkDTO}", orderId, completeWorkDTO);

            try
            {
                var result = await _serviceManager.OrderService.CompleteWorkAsync(orderId, completeWorkDTO, cancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Order {OrderId} marked as completed successfully.", orderId);

                    return Ok(new
                    {
                        StatusCode = result.StatusCode,
                        Success = true,
                        Message = result.Message,
                        Data = result.Value // Updated order details with completed status
                    });
                }

                _logger.LogWarning("Failed to mark order {OrderId} as completed. Reason: {Reason}", orderId, result.Message);

                return StatusCode(result.StatusCode, new
                {
                    StatusCode = result.StatusCode,
                    Success = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking Order {OrderId} as completed.", orderId);

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



        #region Freelancer Orders Screens


        #region Get Requested Services ( The Service Posted By Client and Freelancer sended Bid will be Display here)
        [HttpGet("requested-Orders")]
        public async Task<IActionResult> GetRequestedServices(CancellationToken cancellationToken, string? keyword = "")
        {
            _logger.LogInformation("Fetching requested services for freelancers with keyword: {Keyword}", keyword);
            try
            {
                var result = await _serviceManager.FreelancerService.GetRequestedServicesAsync(cancellationToken, keyword);

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
                _logger.LogError(ex, "Error fetching requested services");
                return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
            }
        }
        #endregion

        #region Ongoin-Completed-InProcess Orders

        #region Filtered Freelancer Orders
        [HttpGet("Filter-Freelancer-Orders")]
        public async Task<IActionResult> GetFreelancerOrders(OrderStatus orderStatus , CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching {Status} orders for FreelancerId: {FreelancerId}", orderStatus);

            var result = await _serviceManager.FreelancerService.GetOrdersByStatusAsync(orderStatus , cancellationToken);
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

        #endregion

        #region Cancel Service(Order) Request 
        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId, Guid freelancerId)
        {
            _logger.LogInformation("Freelancer {FreelancerId} canceling order {OrderId}", freelancerId, orderId);
            //var result = await _serviceManager.FreelancerService.CancelOrderAsync(orderId, freelancerId);
            //if (result.IsSuccess)
            //{
            //    return Ok(new
            //    {
            //        StatusCode = result.StatusCode,
            //        Success = true,
            //        Message = result.Message,
            //        Data = result.Value
            //    });
            //}

            //return StatusCode(result.StatusCode, new
            //{
            //    StatusCode = result.StatusCode,
            //    Success = false,
            //    Message = result.Message
            //});
            return null;
        }
        #endregion

        #region Get Order Details
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails(Guid orderId)
        {
            _logger.LogInformation("Fetching details for order {OrderId}", orderId);
            //var result = await _serviceManager.FreelancerService.GetOrderDetailsAsync(orderId);
            //if (result.IsSuccess)
            //{
            //    return Ok(new
            //    {
            //        StatusCode = result.StatusCode,
            //        Success = true,
            //        Message = result.Message,
            //        Data = result.Value
            //    });
            //}

            //return StatusCode(result.StatusCode, new
            //{
            //    StatusCode = result.StatusCode,
            //    Success = false,
            //    Message = result.Message
            //});
            return null;
        }
        #endregion

        #endregion



        #endregion
    }

}
