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
                new OrderModel { OrderId = 2, UserId = 2, Date = DateTime.Now.AddDays(-1), Status = "In Progress", TotalSum = 100 },
                new OrderModel { OrderId = 3, UserId = 1, Date = DateTime.Now.AddDays(-5), Status = "Completed", TotalSum = 120 },
                new OrderModel { OrderId = 4, UserId = 2, Date = DateTime.Now.AddDays(-3), Status = "Completed", TotalSum = 80 },
                new OrderModel { OrderId = 5, UserId = 3, Date = DateTime.Now.AddDays(-1), Status = "In Progress", TotalSum = 200 },
                new OrderModel { OrderId = 6, UserId = 4, Date = DateTime.Now.AddDays(-7), Status = "In Progress", TotalSum = 150 },
                new OrderModel { OrderId = 7, UserId = 5, Date = DateTime.Now.AddDays(-2), Status = "Completed", TotalSum = 90 },
                new OrderModel { OrderId = 8, UserId = 6, Date = DateTime.Now.AddDays(-4), Status = "In Progress", TotalSum = 110 },
                new OrderModel { OrderId = 9, UserId = 7, Date = DateTime.Now.AddDays(-3), Status = "Completed", TotalSum = 70 },
                new OrderModel { OrderId = 10, UserId = 8, Date = DateTime.Now.AddDays(-6), Status = "In Progress", TotalSum = 180 },
                new OrderModel { OrderId = 11, UserId = 9, Date = DateTime.Now.AddDays(-2), Status = "In Progress", TotalSum = 220 },
                new OrderModel { OrderId = 12, UserId = 10, Date = DateTime.Now.AddDays(-5), Status = "Completed", TotalSum = 130 },
            };
        }

        public async Task<ResponseModel<OrderModel>> GetOrderAsync(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                return new ResponseModel<OrderModel>
                {
                    Data = order,
                    Success = true,
                    Message = "Order found."
                };
            }
            else
            {
                return new ResponseModel<OrderModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<OrderModel>>> GetAllOrdersAsync()
        {
            return new ResponseModel<IEnumerable<OrderModel>>
            {
                Data = _orders,
                Success = true,
                Message = "All orders retrieved."
            };
        }

        public async Task<ResponseModel<OrderModel>> CreateOrderAsync(OrderModel order)
        {
            if (_orders.Any(o => o.OrderId == order.OrderId))
            {
                return new ResponseModel<OrderModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order with the same code already exists."
                };
            }

            _orders.Add(order);
            return new ResponseModel<OrderModel>
            {
                Data = order,
                Success = true,
                Message = "Order added successfully."
            };
        }

        public async Task<ResponseModel<OrderModel>> UpdateOrderAsync(int orderId, OrderModel order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder == null)
            {
                return new ResponseModel<OrderModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order not found."
                };
            }

            existingOrder.UserId = order.UserId;
            existingOrder.Date = order.Date;
            existingOrder.Status = order.Status;
            existingOrder.TotalSum = order.TotalSum;

            return new ResponseModel<OrderModel>
            {
                Data = existingOrder,
                Success = true,
                Message = "Order updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteOrderAsync(int orderId)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Order not found."
                };
            }

            _orders.Remove(existingOrder);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Order deleted successfully."
            };
        }
    }
}
