using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order
{
    public record DeleteOrderCommand(int orderId) : IRequest;

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDeletionResult = _orderRepository.DeleteOrder(request.orderId);

            return Task.CompletedTask;
        }
    }
}
