namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    using AdaptiveWebInterfaces_WebAPI.Models;
    using AdaptiveWebInterfaces_WebAPI.Services.Good;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                var good = await _goodService.GetGoodAsync(goodId);
                if (good == null)
                {
                    return NotFound();
                }
                return Ok(good);
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
                var goods = await _goodService.GetAllGoodsAsync();
                return Ok(goods);
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
                var createdGood = await _goodService.CreateGoodAsync(good);
                return CreatedAtAction(nameof(GetGood), new { code = createdGood.GoodId }, createdGood);
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
                var updatedGood = await _goodService.UpdateGoodAsync(goodId, good);
                return Ok(updatedGood);
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
                var result = await _goodService.DeleteGoodAsync(goodId);
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
