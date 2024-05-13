using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly List<OrderModel> _orders;

        public OrderService()
        {
            _orders = new List<OrderModel>
        {
            new OrderModel { OrderId = 1, UserId = 1, Date = DateTime.Now.AddDays(-2), Status = "Completed", TotalSum = 50 },
            new OrderModel { OrderId = 2, UserId = 2, Date = DateTime.Now.AddDays(-1), Status = "In Progress", TotalSum = 100 }
        };
        }

        public async Task<OrderModel> GetOrderAsync(int orderId)
        {
            return await Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == orderId));
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            return await Task.FromResult(_orders);
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            if (_orders.Any(o => o.OrderId == order.OrderId))
            {
                throw new Exception("Order with the same code already exists.");
            }

            _orders.Add(order);
            return await Task.FromResult(order);
        }

        public async Task<OrderModel> UpdateOrderAsync(int orderId, OrderModel order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder == null)
            {
                throw new Exception("Order not found.");
            }

            existingOrder.UserId = order.UserId;
            existingOrder.Date = order.Date;
            existingOrder.Status = order.Status;
            existingOrder.TotalSum = order.TotalSum;

            return await Task.FromResult(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder == null)
            {
                throw new Exception("Order not found.");
            }

            _orders.Remove(existingOrder);
            return await Task.FromResult(true);
        }
    }

}
