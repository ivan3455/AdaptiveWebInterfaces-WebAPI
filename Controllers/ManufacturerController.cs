using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Services.Manufacturer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet("{manufacturerId}")]
        public async Task<IActionResult> GetManufacturer(int manufacturerId)
        {
            try
            {
                var response = await _manufacturerService.GetManufacturerAsync(manufacturerId);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManufacturers()
        {
            try
            {
                var response = await _manufacturerService.GetAllManufacturersAsync();
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturer([FromBody] ManufacturerModel manufacturer)
        {
            try
            {
                var response = await _manufacturerService.CreateManufacturerAsync(manufacturer);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return CreatedAtAction(nameof(GetManufacturer), new { manufacturerId = response.Data.ManufacturerId }, response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{manufacturerId}")]
        public async Task<IActionResult> UpdateManufacturer(int manufacturerId, [FromBody] ManufacturerModel manufacturer)
        {
            try
            {
                var response = await _manufacturerService.UpdateManufacturerAsync(manufacturerId, manufacturer);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{manufacturerId}")]
        public async Task<IActionResult> DeleteManufacturer(int manufacturerId)
        {
            try
            {
                var response = await _manufacturerService.DeleteManufacturerAsync(manufacturerId);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
