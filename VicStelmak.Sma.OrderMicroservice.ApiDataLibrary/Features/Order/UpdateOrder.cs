using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record UpdateOrderCommand(int OrderId, UpdateOrderRequest Request) : IRequest;

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.OrderId == 0) throw new ArgumentException("OrderId can't be equal to zero.");

            var order = command.Request.MapToOrder();
            order.Id = command.OrderId;

            await _orderRepository.UpdateOrderAsync(order);
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
