using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record SendOrderSubmittingEventCommand(SendOrderSubmittingEventRequest Request) : IRequest;

    internal class SendOrderSubmittingEventHandler : IRequestHandler<SendOrderSubmittingEventCommand>
    {
        private const string SagaBusQueue = "OrderSagaStateData";

        private readonly IBus _bus;
        private readonly ILogger<SendOrderSubmittingEventHandler> _logger;

        public SendOrderSubmittingEventHandler(IBus bus, ILogger<SendOrderSubmittingEventHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task Handle(SendOrderSubmittingEventCommand command, CancellationToken cancellationToken)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + SagaBusQueue));

            await endpoint.Send<OrderSubmitting>(new
            {
                Apartment = command.Request.Apartment,

                Building = command.Request.Building,

                City = command.Request.City,

                Email = command.Request.CreatedBy,

                OrderCode = Guid.NewGuid(),

                PostalCode = command.Request.PostalCode,

                ProductId = command.Request.ProductId,

                QuantityOfProducts = command.Request.QuantityOfProducts,

                Street = command.Request.Street,

                Total = command.Request.Total,
            });

            _logger.LogInformation("OrderSubmitting event was sent by {userName} {date} to RabbitMQ message bus at {time} Utc.", 
                command.Request.CreatedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
        }
    }

    internal record SendOrderSubmittingEventRequest(
        int Apartment,
        string Building,
        string City,
        string CreatedBy,
        string PostalCode,
        int ProductId,
        int QuantityOfProducts,
        string Street,
        decimal Total);
}
