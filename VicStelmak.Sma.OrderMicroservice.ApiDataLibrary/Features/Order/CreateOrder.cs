using MediatR;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.DeliveryAddress;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record CreateOrderCommand(CreateOrderRequest request) : IRequest;

    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var address = command.request.MapToDeliveryAddress();
            var order = command.request.MapToOrder();
            order.OrderCode = Guid.NewGuid().ToString();
            
            var orderCreatingResult = _orderRepository.CreateOrder(address, order);

            return Task.CompletedTask;
        }
    }

    public record CreateOrderRequest(
        int Apartment, 
        string Building,
        string City, 
        string CreatedBy,
        string PostalCode,
        int QuantityOfProducts,
        string Status,
        string Street,
        decimal Total);
}
