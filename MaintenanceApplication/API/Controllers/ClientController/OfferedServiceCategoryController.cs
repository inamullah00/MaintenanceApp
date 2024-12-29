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

        #region Get-Single-OfferedServiceCategory
        [HttpGet("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> GetOfferedServiceCategoryById(Guid Id)
        {
            try
            {
                var result = await _offeredServiceCategory.GetServiceCategoryByIdAsync(Id);
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

        #region Add-OfferedServiceCategory
        [HttpPost("OfferedServiceCategory")]
        public async Task<IActionResult> AddOfferedServiceCategory([FromBody] OfferedServiceCategoryRequestDto requestDto)
        {
            try
            {
                var result = await _offeredServiceCategory.AddServiceCategoryAsync(requestDto);
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

        #region Update-OfferedServiceCategory
        [HttpPut("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> UpdateOfferedServiceCategory([FromBody] OfferedServiceCategoryUpdateDto requestDto, Guid Id)
        {
            try
            {
               

                var result = await _offeredServiceCategory.UpdateServiceCategoryAsync(Id,requestDto);

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

        #region Delete-OfferedServiceCategory
        [HttpDelete("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> DeleteOfferedServiceCategory(Guid Id)
        {
            try
            {
                var result = await _offeredServiceCategory.DeleteServiceCategoryAsync(Id);
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

    }
}
