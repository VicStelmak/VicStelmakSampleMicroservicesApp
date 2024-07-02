using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order
{
    internal static class OrderMapper
    {
        internal static GetOrderResponse MapToGetOrderResponse(this OrderModel order)
        {
            return new GetOrderResponse(
                order.CreatedAt, 
                order.CreatedBy,
                order.ExternalId, 
                order.QuantityOfProducts, 
                order.Status, 
                order.Total,
                order.UpdatedAt,
                order.UpdatedBy);
        }

        internal static OrderModel MapToOrder(this CreateOrderRequest request)
        {
            return new OrderModel()
            {
                CreatedBy = request.CreatedBy,
                QuantityOfProducts = request.QuantityOfProducts,
                Status = request.Status,
                Total = request.Total
            };
        }

        internal static OrderModel MapToOrder(this UpdateOrderRequest request)
        {
            return new OrderModel()
            {
                QuantityOfProducts = request.QuantityOfProducts, 
                Status = request.Status,
                Total = request.Total,
                UpdatedBy = request.UpdatedBy
            };
        }
    }
}
