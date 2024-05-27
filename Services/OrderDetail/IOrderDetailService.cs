using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.OrderDetail
{
    public interface IOrderDetailService
    {
        Task<ResponseModel<OrderDetailModel>> GetOrderDetailAsync(int orderCode, int goodCode);
        Task<ResponseModel<IEnumerable<OrderDetailModel>>> GetAllOrderDetailsAsync();
        Task<ResponseModel<OrderDetailModel>> CreateOrderDetailAsync(OrderDetailModel orderDetail);
        Task<ResponseModel<OrderDetailModel>> UpdateOrderDetailAsync(int orderCode, int goodCode, OrderDetailModel orderDetail);
        Task<ResponseModel<bool>> DeleteOrderDetailAsync(int orderCode, int goodCode);
    }
}
