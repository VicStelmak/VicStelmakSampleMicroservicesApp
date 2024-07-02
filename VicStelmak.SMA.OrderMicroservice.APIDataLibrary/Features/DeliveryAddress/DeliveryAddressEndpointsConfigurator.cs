using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress
{
    public static class DeliveryAddressEndpointsConfigurator
    {
        public static void ConfigureDeliveryAddressEndpoints(this WebApplication app)
        {
            // API endpoint mapping
            app.MapGet("api/delivery-addresses/{id}", GetDeliveryAddress);
            app.MapPut("api/delivery-addresses", UpdateDeliveryAddress);
        }

        private static async Task<IResult> GetDeliveryAddress(int orderId, IMediator mediator)
        {
            try
            {
                var results = await mediator.Send(new GetDeliveryAddressByOrderIdQuery(orderId));
                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateDeliveryAddress(int orderId, UpdateDeliveryAddressRequest request, IMediator mediator)
        {
            try
            {
                await mediator.Send(new UpdateDeliveryAddressCommand(orderId, request));

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
