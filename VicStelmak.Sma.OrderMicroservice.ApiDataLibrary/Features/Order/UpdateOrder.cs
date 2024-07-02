using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order
{
    public record UpdateOrderCommand(int orderId, UpdateOrderRequest request) : IRequest;

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = command.request.MapToOrder();
            order.Id = command.orderId;

            var orderUpdatingResult = _orderRepository.UpdateOrder(order);

            return Task.CompletedTask;
        }
    }

    public record UpdateOrderRequest(int QuantityOfProducts, string Status, decimal Total, string UpdatedBy);
}
