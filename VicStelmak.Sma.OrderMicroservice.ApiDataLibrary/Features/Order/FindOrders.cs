using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record FindOrdersByUserEmailQuery(string EmailAddress) : IRequest<List<GetOrderResponse>>;

    internal class FindOrdersByUserEmailHandler : IRequestHandler<FindOrdersByUserEmailQuery, List<GetOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public FindOrdersByUserEmailHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderResponse>> Handle(FindOrdersByUserEmailQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.FindOrdersByUserEmailAsync(query.EmailAddress);

            return orders.Select(order => order.MapToGetOrderResponse()).ToList();
        }
    }
}
