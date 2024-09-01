using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record PublishOrderCreatedCommand(PublishOrderCreatedRequest Request) : IRequest;

    internal class PublishOrderCreatedHandler : IRequestHandler<PublishOrderCreatedCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishOrderCreatedHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PublishOrderCreatedCommand command, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<OrderCreated>(new
            {
                Email = command.Request.Email,

                OrderCode = Guid.Parse(command.Request.OrderCode),

                ProductId = command.Request.ProductId,

                QuantityOfProducts = command.Request.QuantityOfProducts
            });
        }
    }

    internal record PublishOrderCreatedRequest(
        string Email,
        string OrderCode,
        int ProductId,
        int QuantityOfProducts);
}
