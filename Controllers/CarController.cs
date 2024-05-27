using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Services.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCar(int carId)
        {
            try
            {
                var car = await _carService.GetCarAsync(carId);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var response = await _carService.GetAllCarsAsync();
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarModel car)
        {
            try
            {
                var response = await _carService.CreateCarAsync(car);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return CreatedAtAction(nameof(GetCar), new { carId = response.Data.CarId }, response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(int carId, [FromBody] CarModel car)
        {
            try
            {
                var response = await _carService.UpdateCarAsync(carId, car);
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

        [HttpDelete("{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                var response = await _carService.DeleteCarAsync(carId);
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
