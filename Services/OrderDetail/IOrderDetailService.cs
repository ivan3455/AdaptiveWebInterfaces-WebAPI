using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.OrderDetail
{
    public interface IOrderDetailService
    {
        Task<OrderDetailModel> GetOrderDetailAsync(int orderCode, int goodCode);
        Task<IEnumerable<OrderDetailModel>> GetAllOrderDetailsAsync();
        Task<OrderDetailModel> CreateOrderDetailAsync(OrderDetailModel orderDetail);
        Task<OrderDetailModel> UpdateOrderDetailAsync(int orderCode, int goodCode, OrderDetailModel orderDetail);
        Task<bool> DeleteOrderDetailAsync(int orderCode, int goodCode);
    }
}
