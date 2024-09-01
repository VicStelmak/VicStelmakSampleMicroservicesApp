using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record PublishOrderDeletedCommand(PublishOrderDeletedRequest Request) : IRequest;

    internal class PublishOrderDeletedHandler : IRequestHandler<PublishOrderDeletedCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishOrderDeletedHandler(IPublishEndpoint publishEndpoint)
        {
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
        }
    }

    internal record PublishOrderDeletedRequest(
        string DeletedBy,
        string OrderCode,
        int ProductId,
        int QuantityOfProducts);
}
