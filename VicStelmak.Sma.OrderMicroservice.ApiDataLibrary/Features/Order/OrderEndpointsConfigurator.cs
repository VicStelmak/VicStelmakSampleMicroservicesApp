using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;

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
            application.MapPut("api/orders/line-items/{orderId}", UpdateLineItemAsync);
            application.MapPut("api/orders/{orderId}", UpdateOrderAsync);
        }

        private static async Task<IResult> CheckIfPendingOrderExistsAsync(string userEmail, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                bool orderExists = await mediator.Send(new CheckIfPendingOrderExistsQuery(userEmail));

                return Results.Ok(orderExists);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));
                
                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> CreateLineItemAsync(CreateLineItemRequest request, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var lineItemsAdditionResponse = await mediator.Send(new CreateLineItemCommand(request));

                await mediator.Send(new PublishOrderCreatedCommand(lineItemsAdditionResponse.MapToPublishOrderCreatedRequest()));

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> CreateOrderAsync(CreateOrderRequest request, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var orderCreatingResponse = await mediator.Send(new CreateOrderCommand(request, Guid.NewGuid()));

                await mediator.Send(new PublishOrderCreatedCommand(orderCreatingResponse.MapToPublishOrderCreatedRequest()));

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else 
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> DeleteOrderAsync(string deletedBy, [FromServices] ILoggerFactory loggerFactory, IMediator mediator,
            int orderId)
        {
            try
            {
                var orderDeletingResponse = await mediator.Send(new DeleteOrderCommand(deletedBy, orderId));

                if (orderDeletingResponse.OrderStatus != OrderStatus.Delivered.ToString())
                {
                    foreach (var lineItem in orderDeletingResponse.LineItems)
                    {
                        var publishEventRequest = new PublishOrderDeletedRequest(orderDeletingResponse.DeletedBy, orderDeletingResponse.OrderCode,
                            lineItem.ProductId, lineItem.Quantity);

                        await mediator.Send(new PublishOrderDeletedCommand(publishEventRequest));
                    }
                }
                
                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> FindOrdersByUserEmailAsync(string userEmail, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new FindOrdersByUserEmailQuery(userEmail));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> FindPendingOrderByUserEmailAsync(string userEmail, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var result = await mediator.Send(new FindPendingOrderByUserEmailQuery(userEmail));

                if (result is null) return Results.NotFound();

                return Results.Ok(result);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> GetLineItemsByOrderIdAsync(int orderId, [FromServices] ILoggerFactory loggerFactory,
            IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetLineItemsByOrderIdQuery(orderId)));
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> GetOrderByIdAsync(int orderId, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new GetOrderByIdQuery(orderId));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> GetOrdersAsync([FromServices] ILoggerFactory loggerFactory, IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetOrdersQuery()));
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> SendOrderSubmittingEventAsync(SendOrderSubmittingEventRequest request, IMediator mediator)
        {
           
            await mediator.Send(new SendOrderSubmittingEventCommand(request));

            return Results.Ok();
        }

        private static async Task<IResult> UpdateLineItemAsync(int orderId, UpdateLineItemRequest request, [FromServices] ILoggerFactory loggerFactory, 
            IMediator mediator)
        {
            try
            {
                var lineItemUpdatingRequest = await mediator.Send(new UpdateLineItemCommand(orderId, request));

                await mediator.Send(new PublishOrderCreatedCommand(lineItemUpdatingRequest.MapToPublishOrderCreatedRequest()));

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> UpdateOrderAsync(int orderId, UpdateOrderRequest request, [FromServices] ILoggerFactory loggerFactory,
             IMediator mediator)
        {
            try
            {
                await mediator.Send(new UpdateOrderCommand(orderId, request));

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(OrderEndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }
    }
}
