using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal record FindPendingOrderByUserEmailQuery(string UserEmail) : IRequest<FindPendingOrderResponse>;

    internal class FindPendingOrderByUserEmailHandler : IRequestHandler<FindPendingOrderByUserEmailQuery, FindPendingOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public FindPendingOrderByUserEmailHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<FindPendingOrderResponse> Handle(FindPendingOrderByUserEmailQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindPendingOrderByUserEmailAsync(query.UserEmail);

            // Unfortunately I had to return null because I require it if I want OrderEndpointsConfigurator to give a 404 response instead
            // of exception. I know it's a bad practice.
            if (order is null) return null;

            var lineItems = await _orderRepository.GetLineItemsByOrderIdAsync(order.Id);
            var lineItemsIds = lineItems.Select(lineItem => lineItem.ProductId).ToList();

            return new FindPendingOrderResponse(order.Id, lineItemsIds, order.QuantityOfProducts, order.Total);
        }
    }

    internal record FindPendingOrderResponse(int OrderId, List<int>? ProductsIds, int QuantityOfProducts, decimal Total);
}
