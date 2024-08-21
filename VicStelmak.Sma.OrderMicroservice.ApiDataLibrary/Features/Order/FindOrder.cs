using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record FindOrderByUserEmailQuery(string orderStatus, string userEmail) : IRequest<FindOrderResponse>;

    internal class FindOrderByUserEmailHandler : IRequestHandler<FindOrderByUserEmailQuery, FindOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public FindOrderByUserEmailHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<FindOrderResponse> Handle(FindOrderByUserEmailQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindOrderByUserEmailAsync(query.orderStatus, query.userEmail);

            // Unfortunately I had to return null because I require it if I want OrderEndpointsConfigurator to give a 404 response instead of exception.
            // I know it's a bad practice.
            if (order is null) return null;

            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(order.Id);
            var lineItemsIds = lineItems.Select(lineItem => lineItem.ProductId).ToList();

            return new FindOrderResponse(order.Id, lineItemsIds, order.QuantityOfProducts);
        }
    }

    internal record FindOrderResponse(
        int OrderId,
        List<int>? ProductsIds,
        int QuantityOfProducts);
}
