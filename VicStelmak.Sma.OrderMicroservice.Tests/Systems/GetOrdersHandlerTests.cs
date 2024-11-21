using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class GetOrdersHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly GetOrdersHandler _testedService;

        public GetOrdersHandlerTests()
        {
            _testedService = new GetOrdersHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrdersListIsEmpty_ShouldReturnEmptyList()
        {
            var emptyListOfOrders = new List<OrderModel> { };
            var query = new GetOrdersQuery();
            _repositoryMock.Setup(repository => repository.GetOrdersAsync()).ReturnsAsync(emptyListOfOrders);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.Equal(emptyListOfOrders.Select(order => order.MapToGetOrderResponse()).ToList(), actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenOrdersListIsNotEmpty_ShouldReturnListOfGetOrderResponses()
        {
            var orders = OrderFixture.CreateListOfOrderModels;
            var query = new GetOrdersQuery();
            _repositoryMock.Setup(repository => repository.GetOrdersAsync()).ReturnsAsync(orders);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.IsType<List<GetOrderResponse>>(actualTestResult);
        }
    }
}
