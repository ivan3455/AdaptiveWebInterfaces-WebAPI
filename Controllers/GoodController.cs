using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Services.Good;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoodController : ControllerBase
    {
        private readonly IGoodService _goodService;

        public GoodController(IGoodService goodService)
        {
            _goodService = goodService;
        }

        [HttpGet("{goodId}")]
        public async Task<IActionResult> GetGood(int goodId)
        {
            try
            {
                var response = await _goodService.GetGoodAsync(goodId);
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
        public async Task<IActionResult> GetAllGoods()
        {
            try
            {
                var response = await _goodService.GetAllGoodsAsync();
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGood([FromBody] GoodModel good)
        {
            try
            {
                var response = await _goodService.CreateGoodAsync(good);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return CreatedAtAction(nameof(GetGood), new { goodId = response.Data.GoodId }, response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{goodId}")]
        public async Task<IActionResult> UpdateGood(int goodId, [FromBody] GoodModel good)
        {
            try
            {
                var response = await _goodService.UpdateGoodAsync(goodId, good);
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

        [HttpDelete("{goodId}")]
        public async Task<IActionResult> DeleteGood(int goodId)
        {
            try
            {
                var response = await _goodService.DeleteGoodAsync(goodId);
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
