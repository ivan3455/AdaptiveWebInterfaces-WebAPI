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
                new OrderDetailModel { OrderId = 2, GoodId = 2, Number = 1 },
                new OrderDetailModel { OrderId = 3, GoodId = 3, Number = 3 },
                new OrderDetailModel { OrderId = 4, GoodId = 4, Number = 2 },
                new OrderDetailModel { OrderId = 5, GoodId = 5, Number = 1 },
                new OrderDetailModel { OrderId = 6, GoodId = 6, Number = 3 },
                new OrderDetailModel { OrderId = 7, GoodId = 7, Number = 2 },
                new OrderDetailModel { OrderId = 8, GoodId = 8, Number = 1 },
                new OrderDetailModel { OrderId = 9, GoodId = 9, Number = 3 },
                new OrderDetailModel { OrderId = 10, GoodId = 10, Number = 2 },
                new OrderDetailModel { OrderId = 11, GoodId = 11, Number = 1 },
                new OrderDetailModel { OrderId = 12, GoodId = 12, Number = 3 },
                new OrderDetailModel { OrderId = 13, GoodId = 13, Number = 2 }
            };
        }

        public async Task<ResponseModel<OrderDetailModel>> GetOrderDetailAsync(int orderId, int goodId)
        {
            var orderDetail = _orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId);
            if (orderDetail != null)
            {
                return new ResponseModel<OrderDetailModel>
                {
                    Data = orderDetail,
                    Success = true,
                    Message = "Order detail found."
                };
            }
            else
            {
                return new ResponseModel<OrderDetailModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order detail not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<OrderDetailModel>>> GetAllOrderDetailsAsync()
        {
            return new ResponseModel<IEnumerable<OrderDetailModel>>
            {
                Data = _orderDetails,
                Success = true,
                Message = "All order details retrieved."
            };
        }

        public async Task<ResponseModel<OrderDetailModel>> CreateOrderDetailAsync(OrderDetailModel orderDetail)
        {
            if (_orderDetails.Any(od => od.OrderId == orderDetail.OrderId && od.GoodId == orderDetail.GoodId))
            {
                return new ResponseModel<OrderDetailModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order detail with the same order and good code already exists."
                };
            }

            _orderDetails.Add(orderDetail);
            return new ResponseModel<OrderDetailModel>
            {
                Data = orderDetail,
                Success = true,
                Message = "Order detail added successfully."
            };
        }

        public async Task<ResponseModel<OrderDetailModel>> UpdateOrderDetailAsync(int orderId, int goodId, OrderDetailModel orderDetail)
        {
            var existingOrderDetail = _orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId);
            if (existingOrderDetail == null)
            {
                return new ResponseModel<OrderDetailModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Order detail not found."
                };
            }

            existingOrderDetail.Number = orderDetail.Number;

            return new ResponseModel<OrderDetailModel>
            {
                Data = existingOrderDetail,
                Success = true,
                Message = "Order detail updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteOrderDetailAsync(int orderId, int goodId)
        {
            var existingOrderDetail = _orderDetails.FirstOrDefault(od => od.OrderId == orderId && od.GoodId == goodId);
            if (existingOrderDetail == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Order detail not found."
                };
            }

            _orderDetails.Remove(existingOrderDetail);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Order detail deleted successfully."
            };
        }
    }
}
