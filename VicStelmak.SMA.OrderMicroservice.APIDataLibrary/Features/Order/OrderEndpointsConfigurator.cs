using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order
{
    public static class OrderEndpointsConfigurator
    {
        public static void ConfigureOrderEndpoints(this WebApplication app)
        {
            // API endpoint mapping
            app.MapGet("api/orders", GetOrdersList);
            app.MapGet("api/orders/{id}", GetOrder);
            app.MapPost("api/orders", CreateOrder);
            app.MapPut("api/orders", UpdateOrder);
            app.MapDelete("api/orders", DeleteOrder);
        }

        private static async Task<IResult> CreateOrder(CreateOrderRequest request, IMediator mediator)
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

        private static async Task<IResult> DeleteOrder(int orderId, IMediator mediator)
        {
            try
            {
                await mediator.Send(new DeleteOrderCommand(orderId));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetOrder(int orderId, IMediator mediator)
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

        private static async Task<IResult> GetOrdersList(IMediator mediator)
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

        private static async Task<IResult> UpdateOrder(int orderId, UpdateOrderRequest request, IMediator mediator)
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
