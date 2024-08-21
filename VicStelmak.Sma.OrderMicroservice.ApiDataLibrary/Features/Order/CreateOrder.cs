using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.DeliveryAddress;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record CreateOrderCommand(CreateOrderRequest request) : IRequest;

    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private const string OrderCreatedBusQueue = "OrderCreated";
        private const string UserCreatingBusQueue = "UserCreating";

        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IBus bus, IOrderRepository orderRepository)
        {
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.request.UserExists != true)
            {
                var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + UserCreatingBusQueue));
                await endpoint.Send<UserCreating>(new
                {
                    Apartment = command.request.Apartment,

                    Building = command.request.Building,

                    City = command.request.City,

                    Email = command.request.CreatedBy,

                    OrderCode = Guid.NewGuid(),

                    PostalCode = command.request.PostalCode,

                    ProductId = command.request.ProductId,

                    QuantityOfProducts = command.request.QuantityOfProducts,

                    Street = command.request.Street,

                    Total = command.request.Total
                });
            }
            else
            {
                var address = command.request.MapToDeliveryAddress();
                var order = command.request.MapToOrder();
                order.OrderCode = Guid.NewGuid().ToString();

                _orderRepository.CreateOrder(address, order, command.request.ProductId);

                var endpoint = await _bus.GetSendEndpoint(new Uri("queue:" + OrderCreatedBusQueue));
                await endpoint.Send<OrderCreated>(new
                {
                    Email = command.request.CreatedBy,

                    OrderCode = Guid.Parse(order.OrderCode),

                    ProductId = command.request.ProductId,

                    QuantityOfProducts = command.request.QuantityOfProducts
                });
            }
        }
    }

    internal record CreateOrderRequest(
        int Apartment,
        string Building,
        string City,
        string CreatedBy,
        string PostalCode,
        int ProductId,
        int QuantityOfProducts,
        string Status,
        string Street,
        decimal Total,
        bool? UserExists);
}
