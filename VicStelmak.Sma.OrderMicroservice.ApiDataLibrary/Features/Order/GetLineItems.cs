using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record GetLineItemsByOrderIdQuery(int OrderId) : IRequest<List<GetLineItemsResponse>>;

    internal class GetLineItemsByOrderIdHandler : IRequestHandler<GetLineItemsByOrderIdQuery, List<GetLineItemsResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetLineItemsByOrderIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetLineItemsResponse>> Handle(GetLineItemsByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(query.OrderId);

            return lineItems.Select(lineItem => lineItem.MapToGetLineItemsResponse()).ToList();
        }
    }

    internal record GetLineItemsResponse(int OrderId, int ProductId, int Quantity, decimal TotalPrice);
}
