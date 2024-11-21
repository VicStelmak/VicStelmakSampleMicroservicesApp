using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class FindOrdersByUserEmailHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly FindOrdersByUserEmailHandler _testedService;

        public FindOrdersByUserEmailHandlerTests()
        {
            _testedService = new FindOrdersByUserEmailHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrdersListIsEmpty_ShouldReturnEmptyList()
        {
            var userEmail = "test@email.com";
            var emptyListOfOrders = new List<OrderModel> { };
            var query = new FindOrdersByUserEmailQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindOrdersByUserEmailAsync(userEmail)).ReturnsAsync(emptyListOfOrders);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.Equal(emptyListOfOrders.Select(order => order.MapToGetOrderResponse()).ToList(), actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenOrdersListIsNotEmpty_ShouldReturnListOfGetOrderResponses()
        {
            var userEmail = "test@email.com";
            var orders = OrderFixture.CreateListOfOrderModels;
            var query = new FindOrdersByUserEmailQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindOrdersByUserEmailAsync(userEmail)).ReturnsAsync(orders);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.IsType<List<GetOrderResponse>>(actualTestResult);
        }
    }
}
