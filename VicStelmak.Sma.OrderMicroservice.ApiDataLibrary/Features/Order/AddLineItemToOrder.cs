using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record AddLineItemToOrderCommand(AddLineItemToOrderRequest request) : IRequest;

    internal class AddLineItemToOrderHandler : IRequestHandler<AddLineItemToOrderCommand>
    {
        private const string OrderCreatedBusQueue = "OrderCreated";

        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;

        public AddLineItemToOrderHandler(IBus bus, IOrderRepository orderRepository)
        {
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public async Task Handle(AddLineItemToOrderCommand command, CancellationToken cancellationToken)
        {
            var item = new LineItemModel() { OrderId = command.request.OrderId, ProductId = command.request.ProductId, Quantity = command.request.Quantity };
            var order = await _orderRepository.GetOrderByIdAsync(command.request.OrderId, OrderStatus.Pending.ToString());

            _orderRepository.AddLineItemToOrder(item);

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + OrderCreatedBusQueue));
            await endpoint.Send<OrderCreated>(new
            {
                Email = command.request.OrderUpdatedBy,

                OrderCode = order.OrderCode,

                ProductId = command.request.ProductId,

                QuantityOfProducts = command.request.Quantity
            });
        }
    }

    internal record AddLineItemToOrderRequest(
           int OrderId,
           string OrderUpdatedBy,
           int ProductId,
           int Quantity);
}
