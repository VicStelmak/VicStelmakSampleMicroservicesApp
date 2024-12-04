using MediatR;
using Microsoft.Extensions.Logging;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record DeleteOrderCommand(string DeletedBy, int OrderId) : IRequest<DeleteOrderResponse>;

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        private readonly ILogger<DeleteOrderHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(ILogger<DeleteOrderHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(command.OrderId);
            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(command.OrderId);
            
            await _orderRepository.DeleteOrderAsync(command.OrderId);

            _logger.LogInformation("Order with code {orderCode} was deleted by {userName} {date} at {time} Utc.", 
                order.OrderCode, command.DeletedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

            return new DeleteOrderResponse(command.DeletedBy, order.OrderCode, order.Status,
                lineItems.Select(c => (c.ProductId, c.Quantity)).ToList());
        }
    }

    internal record DeleteOrderResponse(
        string DeletedBy,
        string OrderCode,
        string OrderStatus,
        List<(int ProductId, int Quantity)> LineItems);
}
