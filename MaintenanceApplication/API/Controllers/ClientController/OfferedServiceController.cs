﻿using Application.Dto_s.ClientDto_s;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferedServiceController : ControllerBase
    {
        private readonly IClientService _clientService;

        public OfferedServiceController(IClientService clientService)
        {
            _clientService = clientService;
        }

        #region List-OF-Services
        [HttpGet("Services")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {

                var result = await _clientService.GetServicesAsync();

                return Ok(new {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message, 
                    Services = result.Services 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Single-Service
        [HttpGet("Service/{Id:guid}")]
        public async Task<IActionResult> GetServiceById(Guid Id)
        {
            try
            {

                var result = await _clientService.GetServiceAsync(Id);

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,
                    Data = result.Service
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion




        #region Add-Service
        [HttpPost("Service")]
        public async Task<IActionResult> AddService([FromForm] OfferedServiceRequestDto serviceRequestDto)
        {
            try
            {

                var result = await _clientService.AddServiceAsync(serviceRequestDto);

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,
                    
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion


        #region Update-Service
        [HttpPut("Service/{Id:guid}")]
        public async Task<IActionResult> UpdateService([FromBody] OfferedUpdateRequestDto serviceRequestDto , Guid Id)
        {
            try
            {

                var result = await _clientService.UpdateServiceAsync(Id,serviceRequestDto);

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

        #region Delete-Service
        [HttpDelete("Service/{Id:guid}")]
        public async Task<IActionResult> DeleteService(Guid Id)
        {
            try
            {

                var result = await _clientService.DeleteServiceAsync(Id);

                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = result.Message,

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        #endregion

    }
}