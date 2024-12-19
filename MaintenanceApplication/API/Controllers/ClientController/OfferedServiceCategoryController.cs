using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferedServiceCategoryController : ControllerBase
    {
        private readonly IOfferedServiceCategory _offeredServiceCategory;

        public OfferedServiceCategoryController(IOfferedServiceCategory offeredServiceCategory)
        {
            _offeredServiceCategory = offeredServiceCategory;
        }

        #region List-OF-OfferedServiceCategories
        [HttpGet("OfferedServiceCategories")]
        public async Task<IActionResult> GetAllOfferedServiceCategories()
        {
            try
            {
                var result = await _offeredServiceCategory.GetAllServiceCategoriesAsync();
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Services = result.Categories
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

        #region Get-Single-OfferedServiceCategory
        [HttpGet("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> GetOfferedServiceCategoryById(Guid Id)
        {
            try
            {
                var result = await _offeredServiceCategory.GetServiceCategoryByIdAsync(Id);
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Data = result.Category
                  
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

        #region Add-OfferedServiceCategory
        [HttpPost("OfferedServiceCategory")]
        public async Task<IActionResult> AddOfferedServiceCategory([FromBody] OfferedServiceCategoryRequestDto requestDto)
        {
            try
            {
                var result = await _offeredServiceCategory.AddServiceCategoryAsync(requestDto);
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Id = result.Servicecategory.Id,
                    Message = result.Message,
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

        #region Update-OfferedServiceCategory
        [HttpPut("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> UpdateOfferedServiceCategory([FromBody] OfferedServiceCategoryUpdateDto requestDto, Guid Id)
        {
            try
            {
                // Validate input
                if (Id == Guid.Empty || requestDto == null)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Invalid input. The category ID and request body must be provided."
                    });
                }

                var result = await _offeredServiceCategory.UpdateServiceCategoryAsync(Id,requestDto);
                
                // Check if the update was successful
                if (!result.Success)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = result.Message
                    });
                }

                // Return success response
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,
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

        #region Delete-OfferedServiceCategory
        [HttpDelete("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> DeleteOfferedServiceCategory(Guid Id)
        {
            try
            {
                var result = await _offeredServiceCategory.DeleteServiceCategoryAsync(Id);
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,
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

    }
}
