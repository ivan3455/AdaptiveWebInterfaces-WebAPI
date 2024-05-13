using AdaptiveWebInterfaces_WebAPI.Models;
using AdaptiveWebInterfaces_WebAPI.Services.OrderDetail;
using Microsoft.AspNetCore.Mvc;

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
                var orderDetail = await _orderDetailService.GetOrderDetailAsync(orderId, goodId);
                if (orderDetail == null)
                {
                    return NotFound();
                }
                return Ok(orderDetail);
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
                var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
                return Ok(orderDetails);
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
                var createdOrderDetail = await _orderDetailService.CreateOrderDetailAsync(orderDetail);
                return CreatedAtAction(nameof(GetOrderDetail), new { orderCode = createdOrderDetail.OrderId, goodCode = createdOrderDetail.GoodId }, createdOrderDetail);
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
                var updatedOrderDetail = await _orderDetailService.UpdateOrderDetailAsync(orderId, goodId, orderDetail);
                return Ok(updatedOrderDetail);
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
                var result = await _orderDetailService.DeleteOrderDetailAsync(orderId, goodId);
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
