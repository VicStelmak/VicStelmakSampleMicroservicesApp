using MediatR;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record CreateOrderCommand(CreateOrderRequest Request, Guid OrderCode) : IRequest<CreateOrderResponse>;

    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(ILogger<CreateOrderHandler> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.OrderCode == default(Guid)) throw new ArgumentException("Default Guid is not a valid OrderCode property value.");

            var order = command.Request.MapToOrder();

            order.OrderCode = command.OrderCode.ToString();
            order.Status = OrderStatus.Pending.ToString();

            await _orderRepository.CreateOrderAsync(order, command.Request.ProductId, command.Request.LineItemTotalPrice);

            _logger.LogInformation("Order with code {orderCode} was created by {userName} {date} at {time} Utc.", 
                order.OrderCode, order.CreatedBy, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

            return new CreateOrderResponse(order.CreatedBy, order.OrderCode, command.Request.ProductId, order.QuantityOfProducts);
        }
    }

    internal record CreateOrderRequest(
        int Apartment,
        string Building,
        string City,
        string CreatedBy,
        decimal LineItemTotalPrice,
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
