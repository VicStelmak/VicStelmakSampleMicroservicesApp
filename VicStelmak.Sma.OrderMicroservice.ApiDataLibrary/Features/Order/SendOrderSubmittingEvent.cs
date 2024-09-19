using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record SendOrderSubmittingEventCommand(SendOrderSubmittingEventRequest request) : IRequest;

    internal class SendOrderSubmittingEventHandler : IRequestHandler<SendOrderSubmittingEventCommand>
    {
        private const string SagaBusQueue = "OrderSagaStateData";

        private readonly IBus _bus;

        public SendOrderSubmittingEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(SendOrderSubmittingEventCommand command, CancellationToken cancellationToken)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + SagaBusQueue));
            await endpoint.Send<OrderSubmitting>(new
            {
                Apartment = command.request.Apartment,

                Building = command.request.Building,

                City = command.request.City,

                Email = command.request.CreatedBy,

                OrderCode = Guid.NewGuid(),

                PostalCode = command.request.PostalCode,

                ProductId = command.request.ProductId,

                QuantityOfProducts = command.request.QuantityOfProducts,

                Street = command.request.Street,

                Total = command.request.Total,
            });
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
