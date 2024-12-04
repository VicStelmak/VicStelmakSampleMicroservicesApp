using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record PublishOrderCreatedCommand(PublishOrderCreatedRequest Request) : IRequest;

    internal class PublishOrderCreatedHandler : IRequestHandler<PublishOrderCreatedCommand>
    {
        private readonly ILogger<PublishOrderCreatedHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishOrderCreatedHandler(ILogger<PublishOrderCreatedHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
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

            _logger.LogInformation("OrderCreated event was sent to RabbitMQ message bus by {userName} {date} at {time} Utc.", 
                command.Request.Email, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
        }
    }

    internal record PublishOrderCreatedRequest(
        string Email,
        string OrderCode,
        int ProductId,
        int QuantityOfProducts);
}
