using MediatR;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.DeliveryAddress;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record CreateOrderCommand(CreateOrderRequest Request, Guid OrderCode) : IRequest<CreateOrderResponse>;

    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var deliveryAddress = command.Request.MapToDeliveryAddress();
            var order = command.Request.MapToOrder();

            order.OrderCode = command.OrderCode.ToString();
            order.Status = OrderStatus.Pending.ToString();

            await _orderRepository.CreateOrderAsync(deliveryAddress, order, command.Request.ProductId);

            return new CreateOrderResponse(order.CreatedBy, order.OrderCode, command.Request.ProductId, order.QuantityOfProducts);
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
        string Street,
        decimal Total);

    internal record CreateOrderResponse(
        string CreatedBy,
        string OrderCode,
        int ProductId,
        int QuantityOfProducts);
}
