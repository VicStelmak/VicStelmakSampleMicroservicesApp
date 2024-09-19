using MediatR;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record CreateLineItemCommand(CreateLineItemRequest Request) : IRequest<CreateLineItemResponse>;

    internal class CreateLineItemHandler : IRequestHandler<CreateLineItemCommand, CreateLineItemResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateLineItemHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateLineItemResponse> Handle(CreateLineItemCommand command, CancellationToken cancellationToken)
        {
            var item = new LineItemModel() 
            {
                OrderId = command.Request.OrderId, 
                ProductId = command.Request.ProductId, 
                Quantity = command.Request.Quantity, 
                TotalPrice = command.Request.TotalPrice 
            };
            var order = await _orderRepository.GetOrderByIdAsync(command.Request.OrderId);

            await _orderRepository.CreateLineItemAsync(item);

            return new CreateLineItemResponse(order.OrderCode, command.Request.OrderUpdatedBy, command.Request.ProductId, 
                command.Request.Quantity);
        }
    }

    internal record CreateLineItemRequest(
           int OrderId,
           string OrderUpdatedBy,
           int ProductId,
           int Quantity,
           decimal TotalPrice);

    internal record CreateLineItemResponse(
        string OrderCode,
        string OrderUpdatedBy,
        int ProductId,
        int Quantity);
}
