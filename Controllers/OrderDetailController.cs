using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Services.OrderDetail;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("{orderId}/{goodId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId, int goodId)
        {
            try
            {
                var response = await _orderDetailService.GetOrderDetailAsync(orderId, goodId);
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
        public async Task<IActionResult> GetAllOrderDetails()
        {
            try
            {
                var response = await _orderDetailService.GetAllOrderDetailsAsync();
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailModel orderDetail)
        {
            try
            {
                var response = await _orderDetailService.CreateOrderDetailAsync(orderDetail);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return CreatedAtAction(nameof(GetOrderDetail), new { orderId = response.Data.OrderId, goodId = response.Data.GoodId }, response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{orderId}/{goodId}")]
        public async Task<IActionResult> UpdateOrderDetail(int orderId, int goodId, [FromBody] OrderDetailModel orderDetail)
        {
            try
            {
                var response = await _orderDetailService.UpdateOrderDetailAsync(orderId, goodId, orderDetail);
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

        [HttpDelete("{orderId}/{goodId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int goodId)
        {
            try
            {
                var response = await _orderDetailService.DeleteOrderDetailAsync(orderId, goodId);
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
