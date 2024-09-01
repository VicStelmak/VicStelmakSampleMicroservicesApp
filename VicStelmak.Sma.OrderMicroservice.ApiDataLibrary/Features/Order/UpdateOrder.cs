using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record UpdateOrderCommand(int orderId, UpdateOrderRequest request) : IRequest;

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = command.request.MapToOrder();
            order.Id = command.orderId;

            await _orderRepository.UpdateOrderAsync(order);
        }
    }

    public record UpdateOrderRequest(int QuantityOfProducts, string Status, decimal Total, string UpdatedBy);
}
