using Application.Dto_s.ClientDto_s;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Maintenance.Application.Dto_s.FreelancerDto_s;
using Maintenance.Application.Interfaces.ServiceInterfaces.FreelancerInterfaces;
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
        public async Task<IActionResult> GetBidsByFreelancer()
        {
            try
            {
                var result = await _freelancerService.GetBidsByFreelancerAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Bids = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion


        #region Get Bids by Freelancer
        [HttpGet("Bids/{freelancerId:guid}")]
        public async Task<IActionResult> GetBidsByFreelancer(Guid freelancerId)
        {
            try
            {
                var result = await _freelancerService.GetBidsByFreelancerAsync(freelancerId);

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Bids = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
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

                return result.Success
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
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

                return result.Success
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
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
                    return BadRequest(new { StatusCode = 400, Success = false, Message = "Invalid or Empty Id." });
                }

                var result = await _freelancerService.DeleteBidAsync(bidId);

                return result.Success
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : NotFound(new { StatusCode = 404, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
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
                // Call the service to approve the bid
                var result = await _freelancerService.ApproveBidAsync(id, bidRequestDto);

                return result.Success
                    ? Ok(new { StatusCode = 200, Success = true, Message = result.Message })
                    : BadRequest(new { StatusCode = 400, Success = false, Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Success = false, Message = $"Internal server error: {ex.Message}" });
            }
        }
        #endregion

    }
}
