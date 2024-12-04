using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record PublishOrderDeletedCommand(PublishOrderDeletedRequest Request) : IRequest;

    internal class PublishOrderDeletedHandler : IRequestHandler<PublishOrderDeletedCommand>
    {
        private readonly ILogger<PublishOrderDeletedHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishOrderDeletedHandler(ILogger<PublishOrderDeletedHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PublishOrderDeletedCommand command, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<OrderDeleted>(new
            {
                Email = command.Request.DeletedBy,

                OrderCode = Guid.Parse(command.Request.OrderCode),

                ProductId = command.Request.ProductId,

                QuantityOfProducts = command.Request.QuantityOfProducts
            });

            _logger.LogInformation("OrderDeleted event was sent to RabbitMQ message bus by {userName} {date} at {time} Utc.", 
                command.Request.DeletedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
        }
    }

    internal record PublishOrderDeletedRequest(
        string DeletedBy,
        string OrderCode,
        int ProductId,
        int QuantityOfProducts);
}
