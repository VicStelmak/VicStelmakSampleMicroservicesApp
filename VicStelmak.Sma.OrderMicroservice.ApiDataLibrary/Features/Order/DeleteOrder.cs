using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record DeleteOrderCommand(string DeletedBy, int orderId) : IRequest<DeleteOrderResponse>;

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(command.orderId);
            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(command.orderId);

            await _orderRepository.DeleteOrderAsync(command.orderId);
            
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
