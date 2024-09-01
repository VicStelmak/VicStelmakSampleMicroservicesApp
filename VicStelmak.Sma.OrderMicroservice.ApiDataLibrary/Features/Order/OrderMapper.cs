using VicStelmak.Sma.Events;
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
                orderCreatingEvent.Street,
                orderCreatingEvent.Total);
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

        internal static PublishOrderCreatedRequest MapToPublishOrderCreatedRequest(this AddLineItemToOrderResponse response)
        {
            return new PublishOrderCreatedRequest(response.OrderUpdatedBy, response.OrderCode, response.ProductId, response.Quantity);
        }

        internal static PublishOrderCreatedRequest MapToPublishOrderCreatedRequest(this CreateOrderResponse response)
        {
            return new PublishOrderCreatedRequest(response.CreatedBy, response.OrderCode, response.ProductId, response.QuantityOfProducts);
        }
    }
}
