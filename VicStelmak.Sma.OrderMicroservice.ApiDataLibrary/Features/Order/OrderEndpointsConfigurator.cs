using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public static class OrderEndpointsConfigurator
    {
        public static void ConfigureOrderEndpoints(this WebApplication application)
        {
            // API endpoint mapping
            application.MapGet("api/orders/order", CheckIfPendingOrderExistsAsync);
            application.MapPost("api/orders/line-items", CreateLineItemAsync);
            application.MapPost("api/orders", CreateOrderAsync);
            application.MapDelete("api/orders", DeleteOrderAsync);
            application.MapGet("api/orders/created-by", FindOrdersByUserEmailAsync);
            application.MapGet("api/orders/order/created-by", FindPendingOrderByUserEmailAsync);
            application.MapGet("api/orders/line-items", GetLineItemsByOrderIdAsync);
            application.MapGet("api/orders/id", GetOrderByIdAsync);
            application.MapGet("api/orders", GetOrdersAsync);
            application.MapPost("api/orders/events", SendOrderSubmittingEventAsync);
            application.MapPut("api/orders/{orderId}", UpdateOrderAsync);
        }

        private static async Task<IResult> CheckIfPendingOrderExistsAsync(string userEmail, IMediator mediator)
        {
            try
            {
                bool orderExists = await mediator.Send(new CheckIfPendingOrderExistsQuery(userEmail));

                return Results.Ok(orderExists);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CreateLineItemAsync(CreateLineItemRequest request, IMediator mediator)
        {
            try
            {
                var lineItemsAdditionResponse = await mediator.Send(new CreateLineItemCommand(request));

                await mediator.Send(new PublishOrderCreatedCommand(lineItemsAdditionResponse.MapToPublishOrderCreatedRequest()));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CreateOrderAsync(CreateOrderRequest request, IMediator mediator)
        {
            try
            {
                var orderCreatingResponse = await mediator.Send(new CreateOrderCommand(request, Guid.NewGuid()));

                await mediator.Send(new PublishOrderCreatedCommand(orderCreatingResponse.MapToPublishOrderCreatedRequest()));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteOrderAsync(string deletedBy, int orderId, IMediator mediator)
        {
            try
            {
                var orderDeletingResponse = await mediator.Send(new DeleteOrderCommand(deletedBy, orderId));

                foreach (var lineItem in orderDeletingResponse.LineItems) 
                {
                    var publishEventRequest = new PublishOrderDeletedRequest(orderDeletingResponse.DeletedBy, orderDeletingResponse.OrderCode, 
                        lineItem.ProductId, lineItem.Quantity);

                    await mediator.Send(new PublishOrderDeletedCommand(publishEventRequest));
                }
                
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> FindOrdersByUserEmailAsync(string userEmail, IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new FindOrdersByUserEmailQuery(userEmail));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> FindPendingOrderByUserEmailAsync(string userEmail, IMediator mediator)
        {
            try
            {
                var result = await mediator.Send(new FindPendingOrderByUserEmailQuery(userEmail));
                if (result is null) return Results.NotFound();

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetLineItemsByOrderIdAsync(int orderId, IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetLineItemsByOrderIdQuery(orderId)));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetOrderByIdAsync(int orderId, IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new GetOrderByIdQuery(orderId));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetOrdersAsync(IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetOrdersQuery()));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> SendOrderSubmittingEventAsync(SendOrderSubmittingEventRequest request, IMediator mediator)
        {
            try
            {
                await mediator.Send(new SendOrderSubmittingEventCommand(request));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateOrderAsync(int orderId, UpdateOrderRequest request, IMediator mediator)
        {
            try
            {
                await mediator.Send(new UpdateOrderCommand(orderId, request));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
