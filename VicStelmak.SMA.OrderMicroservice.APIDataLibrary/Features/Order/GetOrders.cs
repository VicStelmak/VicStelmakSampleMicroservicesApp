using MediatR;

namespace VicStelmak.SMA.OrderMicroservice.APIDataLibrary.Features.Order
{
    public record GetOrdersListQuery() : IRequest<List<GetOrderResponse>>;

    internal class GetOrdersListHandler : IRequestHandler<GetOrdersListQuery, List<GetOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersListHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderResponse>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersList();

            return orders.Select(order => order.MapToGetOrderResponse()).ToList();
        }
    }
}
