using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Order
{
    public interface IOrderService
    {
        Task<ResponseModel<OrderModel>> GetOrderAsync(int code);
        Task<ResponseModel<IEnumerable<OrderModel>>> GetAllOrdersAsync();
        Task<ResponseModel<OrderModel>> CreateOrderAsync(OrderModel order);
        Task<ResponseModel<OrderModel>> UpdateOrderAsync(int code, OrderModel order);
        Task<ResponseModel<bool>> DeleteOrderAsync(int code);
    }
}
