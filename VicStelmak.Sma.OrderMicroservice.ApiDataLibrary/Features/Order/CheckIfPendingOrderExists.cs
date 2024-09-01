using MediatR;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    public record CheckIfPendingOrderExistsQuery(string userEmail) : IRequest<bool>;

    internal class CheckIfPendingOrderExistsHandler : IRequestHandler<CheckIfPendingOrderExistsQuery, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckIfPendingOrderExistsHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(CheckIfPendingOrderExistsQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindPendingOrderByUserEmailAsync(query.userEmail);

            if (order is null) return false;
            else return true;
        }
    }
}
