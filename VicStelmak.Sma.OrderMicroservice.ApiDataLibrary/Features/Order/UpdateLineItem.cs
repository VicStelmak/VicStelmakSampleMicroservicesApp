using MediatR;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record UpdateLineItemCommand(int OrderId, UpdateLineItemRequest Request) : IRequest<UpdateLineItemResponse>;

    internal class UpdateLineItemHandler : IRequestHandler<UpdateLineItemCommand, UpdateLineItemResponse>
    {
        private readonly ILogger<UpdateLineItemHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public UpdateLineItemHandler(ILogger<UpdateLineItemHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<UpdateLineItemResponse> Handle(UpdateLineItemCommand command, CancellationToken cancellationToken)
        {
            if (command.OrderId == 0) throw new ArgumentException("OrderId can't be null.");

            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(command.OrderId);

            int lineItemIndex = lineItems.FindIndex(lineItem => lineItem.ProductId == command.Request.ProductId);

            var lineItem = new LineItemModel()
            {
                OrderId = command.OrderId,
                ProductId = command.Request.ProductId,
                Quantity = lineItems[lineItemIndex].Quantity + command.Request.Quantity,
                TotalPrice = lineItems[lineItemIndex].TotalPrice + command.Request.TotalPrice,
            };

            await _orderRepository.UpdateLineItemAsync(lineItem);

            _logger.LogInformation("Line item with {lineItemId} was updated by {userName} {date} at {time} Utc.",
                command.Request.ProductId, command.Request.UpdatedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

            return new UpdateLineItemResponse(command.Request.OrderCode, command.Request.ProductId, command.Request.Quantity,
                command.Request.UpdatedBy);
        }
    }

    internal record UpdateLineItemRequest(
        string OrderCode,
        int ProductId,
        int Quantity,
        decimal TotalPrice,
        string UpdatedBy);

    internal record UpdateLineItemResponse(
        string OrderCode,
        int ProductId,
        int Quantity,
        string UpdatedBy);
}
