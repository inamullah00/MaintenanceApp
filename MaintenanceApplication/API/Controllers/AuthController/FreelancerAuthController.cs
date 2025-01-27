using Application.Dto_s.UserDto_s;
using AutoMapper;
using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerAuthController : ControllerBase
    {

        #region Constructor & Fields

        private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public FreelancerAuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        #endregion


        #region Signup

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(CreateAdminRegistrationRequestDto request)
        {
            try
            {
                var result = await _serviceManager.RegisterationService.RegisterAsync(request);

                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}
