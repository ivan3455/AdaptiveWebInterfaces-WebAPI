using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Order
{
    public interface IOrderService
    {
        Task<OrderModel> GetOrderAsync(int code);
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel> CreateOrderAsync(OrderModel order);
        Task<OrderModel> UpdateOrderAsync(int code, OrderModel order);
        Task<bool> DeleteOrderAsync(int code);
    }
}
