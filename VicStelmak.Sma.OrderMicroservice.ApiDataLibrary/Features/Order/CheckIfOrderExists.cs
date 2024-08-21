using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record CheckIfOrderExistsQuery(string orderStatus, string userEmail) : IRequest<bool>;

    internal class CheckIfPendingOrderExistsHandler : IRequestHandler<CheckIfOrderExistsQuery, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckIfPendingOrderExistsHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(CheckIfOrderExistsQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindOrderByUserEmailAsync(query.orderStatus, query.userEmail);

            if (order is null) return false;
            else return true;
        }
    }
}
