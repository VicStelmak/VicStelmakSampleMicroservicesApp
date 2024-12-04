using MediatR;
using Microsoft.Extensions.Logging;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record UpdateOrderCommand(int OrderId, UpdateOrderRequest Request) : IRequest;

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly ILogger<UpdateOrderHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(ILogger<UpdateOrderHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.OrderId == 0) throw new ArgumentException("OrderId can't be equal to zero.");

            var orderBeforeUpdate = await _orderRepository.GetOrderByIdAsync(command.OrderId);
            var updatedOrder = command.Request.MapToOrder();
            updatedOrder.Id = command.OrderId;

            await _orderRepository.UpdateOrderAsync(updatedOrder);

            _logger.LogInformation("Order with code {orderCode} was updated by {userName} {date} at {time} Utc.",
                orderBeforeUpdate.OrderCode, command.Request.UpdatedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
        }
    }

    public record UpdateOrderRequest(
        int Apartment,
        string Building,
        string City,
        string PostalCode,
        int QuantityOfProducts, 
        string Status, 
        string Street,
        decimal Total, 
        string UpdatedBy);
}
