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
            application.MapPost("api/orders/line-items", AddLineItemToOrderAsync);
            application.MapDelete("api/orders", DeleteOrderAsync);
            application.MapGet("api/orders/created-by", FindOrderByUserEmailAsync);
            application.MapGet("api/orders/exists", CheckIfOrderExistsAsync);
            application.MapPost("api/orders", CreateOrderAsync);
            application.MapGet("api/orders", GetOrdersListAsync);
            application.MapGet("api/orders/id", GetOrderAsync);
            application.MapPut("api/orders/{orderId}", UpdateOrderAsync);
        }

        private static async Task<IResult> AddLineItemToOrderAsync(AddLineItemToOrderRequest request, IMediator mediator)
        {
            try
            {
                await mediator.Send(new AddLineItemToOrderCommand(request));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CheckIfOrderExistsAsync(string orderStatus, string userEmail, IMediator mediator)
        {
            try
            {
                bool orderExists = await mediator.Send(new CheckIfOrderExistsQuery(orderStatus, userEmail));

                return Results.Ok(orderExists);
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
                await mediator.Send(new CreateOrderCommand(request));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteOrderAsync(int orderId, string orderStatus, IMediator mediator)
        {
            try
            {
                await mediator.Send(new DeleteOrderCommand(orderId, orderStatus));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> FindOrderByUserEmailAsync(string orderStatus, string userEmail, IMediator mediator)
        {
            try
            {
                var result = await mediator.Send(new FindOrderByUserEmailQuery(orderStatus, userEmail));
                if (result is null) return Results.NotFound();

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetOrderAsync(int orderId, string orderStatus, IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new GetOrderByIdQuery(orderId, orderStatus));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetOrdersListAsync(IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetOrdersListQuery()));
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
