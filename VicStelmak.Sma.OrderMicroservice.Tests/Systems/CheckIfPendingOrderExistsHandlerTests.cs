using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class CheckIfPendingOrderExistsHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly CheckIfPendingOrderExistsHandler _testedService;

        public CheckIfPendingOrderExistsHandlerTests()
        {
            _testedService = new CheckIfPendingOrderExistsHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrderDoesNotExists_ShouldReturnFalse()
        {
            var userEmail = "test@email.com";
            var query = new CheckIfPendingOrderExistsQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindPendingOrderByUserEmailAsync(userEmail)).ReturnsAsync(() => null);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.True(actualTestResult == false);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnTrue()
        {
            var order = OrderFixture.CreateOrderModel();
            var userEmail = "test@email.com";
            var query = new CheckIfPendingOrderExistsQuery(userEmail);
            _repositoryMock.Setup(repository => repository.FindPendingOrderByUserEmailAsync(userEmail)).ReturnsAsync(order);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.True(actualTestResult == true);
        }

        [Fact]
        public async Task Handle_WhenQueryPayloadIsNull_ShouldThrowArgumentException()
        {
            string userEmail = null;
            var query = new CheckIfPendingOrderExistsQuery(userEmail);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _testedService.Handle(
                query, new CancellationToken()));
        }
    }
}
