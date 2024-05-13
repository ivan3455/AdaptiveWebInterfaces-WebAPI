using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.OrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly List<OrderDetailModel> _orderDetails;

        public OrderDetailService()
        {
            _orderDetails = new List<OrderDetailModel>
        {
            new OrderDetailModel { OrderId = 1, GoodId = 1, Number = 2 },
            new OrderDetailModel { OrderId = 1, GoodId = 2, Number = 1 },
            new OrderDetailModel { OrderId = 2, GoodId = 3, Number = 3 }
        };
        }

        public async Task<OrderDetailModel> GetOrderDetailAsync(int orderId, int goodId)
        {
            return await Task.FromResult(_orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId));
        }

        public async Task<IEnumerable<OrderDetailModel>> GetAllOrderDetailsAsync()
        {
            return await Task.FromResult(_orderDetails);
        }

        public async Task<OrderDetailModel> CreateOrderDetailAsync(OrderDetailModel orderDetail)
        {
            if (_orderDetails.Any(od => od.OrderId == orderDetail.OrderId && od.OrderId == orderDetail.OrderId))
            {
                throw new Exception("Order detail with the same order and good code already exists.");
            }

            _orderDetails.Add(orderDetail);
            return await Task.FromResult(orderDetail);
        }

        public async Task<OrderDetailModel> UpdateOrderDetailAsync(int orderId, int goodId, OrderDetailModel orderDetail)
        {
            var existingOrderDetail = _orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId);
            if (existingOrderDetail == null)
            {
                throw new Exception("Order detail not found.");
            }

            existingOrderDetail.Number = orderDetail.Number;

            return await Task.FromResult(existingOrderDetail);
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderId, int goodId)
        {
            var existingOrderDetail = _orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId);
            if (existingOrderDetail == null)
            {
                throw new Exception("Order detail not found.");
            }

            _orderDetails.Remove(existingOrderDetail);
            return await Task.FromResult(true);
        }
    }

}
