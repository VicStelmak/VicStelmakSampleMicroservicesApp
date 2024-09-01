using MediatR;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record AddLineItemToOrderCommand(AddLineItemToOrderRequest Request) : IRequest<AddLineItemToOrderResponse>;

    internal class AddLineItemToOrderHandler : IRequestHandler<AddLineItemToOrderCommand, AddLineItemToOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public AddLineItemToOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<AddLineItemToOrderResponse> Handle(AddLineItemToOrderCommand command, CancellationToken cancellationToken)
        {
            var item = new LineItemModel() { OrderId = command.Request.OrderId, ProductId = command.Request.ProductId, 
                Quantity = command.Request.Quantity };
            var order = await _orderRepository.GetOrderByIdAsync(command.Request.OrderId);

            await _orderRepository.AddLineItemToOrderAsync(item);

            return new AddLineItemToOrderResponse(order.OrderCode, command.Request.OrderUpdatedBy, command.Request.ProductId, 
                command.Request.Quantity);
        }
    }

    internal record AddLineItemToOrderRequest(
           int OrderId,
           string OrderUpdatedBy,
           int ProductId,
           int Quantity);

    internal record AddLineItemToOrderResponse(
        string OrderCode,
        string OrderUpdatedBy,
        int ProductId,
        int Quantity);
}
