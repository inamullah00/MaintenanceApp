using Application.Dto_s.ClientDto_s;
using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Services.OffereServiceCategory;
using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferedServiceCategoryController : ControllerBase
    {
        private readonly IOfferedServiceCategory _offeredServiceCategory;
        private readonly IServiceManager _serviceManager;

        public OfferedServiceCategoryController(IServiceManager serviceManager)
        {

            _serviceManager = serviceManager;
        }

        #region List-OF-OfferedServiceCategories
        [HttpGet("OfferedServiceCategories")]
        public async Task<IActionResult> GetAllOfferedServiceCategories()
        {
            try
            {
                var result = await _serviceManager.OfferedServiceCategory.GetAllServiceCategoriesAsync();
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

        #region Get-Single-OfferedServiceCategory
        [HttpGet("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> GetOfferedServiceCategoryById(Guid Id)
        {
            try
            {
                var result = await _serviceManager.OfferedServiceCategory.GetServiceCategoryByIdAsync(Id);
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

        #region Add-OfferedServiceCategory
        [HttpPost("OfferedServiceCategory")]
        public async Task<IActionResult> AddOfferedServiceCategory([FromBody] OfferedServiceCategoryRequestDto requestDto)
        {
            try
            {
                var result = await _serviceManager.OfferedServiceCategory.AddServiceCategoryAsync(requestDto);
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

        #region Update-OfferedServiceCategory
        [HttpPut("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> UpdateOfferedServiceCategory([FromBody] OfferedServiceCategoryUpdateDto requestDto, Guid Id)
        {
            try
            {
                var result = await _serviceManager.OfferedServiceCategory.UpdateServiceCategoryAsync(Id,requestDto);

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

        #region Delete-OfferedServiceCategory
        [HttpDelete("OfferedServiceCategory/{Id:guid}")]
        public async Task<IActionResult> DeleteOfferedServiceCategory(Guid Id)
        {
            try
            {
                var result = await _serviceManager.OfferedServiceCategory.DeleteServiceCategoryAsync(Id);
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

    }
}
