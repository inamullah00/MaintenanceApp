using Application.Dto_s.ClientDto_s;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.Freelance.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.FreelancerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly IFreelancerService _freelancerService;

        public FreelancerController(IFreelancerService freelancerService)
        {
            _freelancerService = freelancerService;
        }


        #region Get Bids by Freelancer
        [HttpGet("Bids")]
        public async Task<IActionResult> GetBidsByFreelancer(CancellationToken cancellationToken, string? Keyword = "")
        {
            try
            {
               
                var result = await _freelancerService.GetBidsByFreelancerAsync(cancellationToken,Keyword);

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

        #region Get Bids by Freelancer
        [HttpGet("Bids/{freelancerId:guid}")]
        public async Task<IActionResult> GetBidsByFreelancer(Guid freelancerId)
        {
            try
            {

                if (freelancerId == Guid.Empty)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid or Empty Freelancer Id."
                    });
                }
                    var result = await _freelancerService.GetBidsByFreelancerAsync(freelancerId);

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

        #region Submit Bid
        [HttpPost("Bids")]
        public async Task<IActionResult> SubmitBid([FromBody] BidRequestDto bidRequestDto)
        {
            try
            {
                var result = await _freelancerService.SubmitBidAsync(bidRequestDto);

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

        #region Update Bid
        [HttpPut("Bids/{freelancerId:guid}")]
        public async Task<IActionResult> UpdateBid(Guid freelancerId,BidUpdateDto bidUpdateDto)
        {
            try
            {
                var result = await _freelancerService.UpdateBidAsync(bidUpdateDto ,freelancerId);

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

        #region Delete Bid
        [HttpDelete("Bids/{bidId:guid}")]
        public async Task<IActionResult> DeleteBid(Guid bidId)
        {
            try
            {

                if (bidId == Guid.Empty)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid or Empty Bid Id."
                    });
                }

                var result = await _freelancerService.DeleteBidAsync(bidId);
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

        #region Approve Bid Request
        [HttpPatch("Bids/{id:guid}")]
        public async Task<IActionResult> ApproveBid(Guid id, [FromBody] ApproveBidRequestDto bidRequestDto)
        {
            if (bidRequestDto == null || id == Guid.Empty)
            {
                return BadRequest(new { StatusCode = 400, Success = false, Message = "Invalid request data." });
            }

            try
            {
                var result = await _freelancerService.ApproveBidAsync(id, bidRequestDto);

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

    }
}
