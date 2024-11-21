using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class GetOrderByIdHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly GetOrderByIdHandler _testedService;

        public GetOrderByIdHandlerTests()
        {
            _testedService = new GetOrderByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrderDoesNotExists_ShouldReturnNull()
        {
            int nonExistentOrderId = 2;
            var query = new GetOrderByIdQuery(nonExistentOrderId);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(nonExistentOrderId)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.Null(actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnGetOrderResponse()
        {
            int orderId = 1;
            var order = OrderFixture.CreateOrderModel();
            var query = new GetOrderByIdQuery(orderId);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.IsType<GetOrderResponse>(actualTestResult);
        }
    }
}
