using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record DeleteOrderCommand(int orderId, string orderStatus) : IRequest;

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private const string OrderDeletedBusQueue = "OrderDeleted";

        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IBus bus, IOrderRepository orderRepository)
        {
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(command.orderId, command.orderStatus);
            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(command.orderId);

            _orderRepository.DeleteOrder(command.orderId);

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + OrderDeletedBusQueue));
            foreach (var item in lineItems)
            {
                await endpoint.Send<OrderDeleted>(new
                {
                    Email = order.CreatedBy,
                    OrderCode = Guid.Parse(order.OrderCode),
                    ProductId = item.ProductId,
                    QuantityOfProducts = item.Quantity
                });
            }
        }
    }
}
