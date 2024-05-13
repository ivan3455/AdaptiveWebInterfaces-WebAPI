namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    using AdaptiveWebInterfaces_WebAPI.Models;
    using AdaptiveWebInterfaces_WebAPI.Services.Manufacturer;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                var manufacturer = await _manufacturerService.GetManufacturerAsync(manufacturerId);
                if (manufacturer == null)
                {
                    return NotFound();
                }
                return Ok(manufacturer);
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
                var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
                return Ok(manufacturers);
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
                var createdManufacturer = await _manufacturerService.CreateManufacturerAsync(manufacturer);
                return CreatedAtAction(nameof(GetManufacturer), new { code = createdManufacturer.ManufacturerId }, createdManufacturer);
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
                var updatedManufacturer = await _manufacturerService.UpdateManufacturerAsync(manufacturerId, manufacturer);
                return Ok(updatedManufacturer);
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
                var result = await _manufacturerService.DeleteManufacturerAsync(manufacturerId);
                if (!result)
                {
                    return NotFound();
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
