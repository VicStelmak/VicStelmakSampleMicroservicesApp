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
                orderCreatingEvent.Total,
                orderCreatingEvent.PostalCode,
                orderCreatingEvent.ProductId,
                orderCreatingEvent.QuantityOfProducts,
                orderCreatingEvent.Street,
                orderCreatingEvent.Total);
        }

        internal static GetLineItemsResponse MapToGetLineItemsResponse(this LineItemModel lineItem)
        {
            return new GetLineItemsResponse(lineItem.OrderId, lineItem.ProductId, lineItem.Quantity, lineItem.TotalPrice);
        }

        internal static GetOrderResponse MapToGetOrderResponse(this OrderModel order)
        {
            return new GetOrderResponse(
                order.Id,
                order.Apartment, 
                order.Building, 
                order.City,
                order.CreatedAt, 
                order.CreatedBy,
                order.OrderCode,
                order.PostalCode,
                order.QuantityOfProducts, 
                order.Status, 
                order.Street,
                order.Total,
                order.UpdatedAt,
                order.UpdatedBy);
        }

        internal static OrderModel MapToOrder(this CreateOrderRequest request)
        {
            return new OrderModel()
            {
                Apartment = request.Apartment,
                Building = request.Building,
                City = request.City,
                CreatedBy = request.CreatedBy,
                PostalCode = request.PostalCode,
                QuantityOfProducts = request.QuantityOfProducts,
                Street = request.Street,
                Total = request.Total
            };
        }

        internal static OrderModel MapToOrder(this UpdateOrderRequest request)
        {
            return new OrderModel()
            {
                Apartment = request.Apartment,
                Building = request.Building,
                City = request.City,
                PostalCode= request.PostalCode,
                QuantityOfProducts = request.QuantityOfProducts, 
                Status = request.Status,
                Street= request.Street,
                Total = request.Total,
                UpdatedBy = request.UpdatedBy
            };
        }

        internal static PublishOrderCreatedRequest MapToPublishOrderCreatedRequest(this CreateLineItemResponse response)
        {
            return new PublishOrderCreatedRequest(response.OrderUpdatedBy, response.OrderCode, response.ProductId, response.Quantity);
        }

        internal static PublishOrderCreatedRequest MapToPublishOrderCreatedRequest(this CreateOrderResponse response)
        {
            return new PublishOrderCreatedRequest(response.CreatedBy, response.OrderCode, response.ProductId, response.QuantityOfProducts);
        }
    }
}
