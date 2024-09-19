using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record GetOrdersQuery() : IRequest<List<GetOrderResponse>>;

    internal class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<GetOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderResponse>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersAsync();

            return orders.Select(order => order.MapToGetOrderResponse()).ToList();
        }
    }
}
