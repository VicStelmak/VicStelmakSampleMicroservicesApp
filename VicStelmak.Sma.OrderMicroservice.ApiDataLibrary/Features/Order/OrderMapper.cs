using VicStelmak.Sma.Events;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal static class OrderMapper
    {
        internal static CreateOrderRequest MapToCreateOrderRequest(this OrderCreating orderCreatingEvent)
        {
            return new CreateOrderRequest(
                orderCreatingEvent.Apartment,
                orderCreatingEvent.Building,
                orderCreatingEvent.City,
                orderCreatingEvent.Email,
                orderCreatingEvent.PostalCode,
                orderCreatingEvent.ProductId,
                orderCreatingEvent.QuantityOfProducts,
                OrderStatus.Pending.ToString(),
                orderCreatingEvent.Street,
                orderCreatingEvent.Total,
                true);
        }

        internal static GetOrderResponse MapToGetOrderResponse(this OrderModel order)
        {
            return new GetOrderResponse(
                order.CreatedAt, 
                order.CreatedBy,
                order.OrderCode, 
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
