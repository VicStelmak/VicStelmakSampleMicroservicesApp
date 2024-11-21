using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class FindPendingOrderByUserEmailHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly FindPendingOrderByUserEmailHandler _testedService;

        public FindPendingOrderByUserEmailHandlerTests()
        {
            _testedService = new FindPendingOrderByUserEmailHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrderDoesNotExist_ShouldReturnNull()
        {
            var userEmail = "test@email.com";
            var query = new FindPendingOrderByUserEmailQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindPendingOrderByUserEmailAsync(userEmail)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.Null(actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenOrderExist_ShouldCallGetLineItemsByOrderIdAsyncOnce()
        {
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            var userEmail = "test@email.com";
            var query = new FindPendingOrderByUserEmailQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindPendingOrderByUserEmailAsync(userEmail)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(order.Id)).ReturnsAsync(lineItems);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            _repositoryMock.Verify(repository => repository.GetLineItemsByOrderIdAsync(order.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnFindPendingOrderResponse()
        {
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            var userEmail = "test@email.com";
            var query = new FindPendingOrderByUserEmailQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindPendingOrderByUserEmailAsync(userEmail)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(order.Id)).ReturnsAsync(lineItems);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.IsType<FindPendingOrderResponse>(actualTestResult);
        }
    }
}
