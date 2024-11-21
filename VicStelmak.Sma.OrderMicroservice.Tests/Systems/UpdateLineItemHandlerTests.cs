using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class UpdateLineItemHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly UpdateLineItemHandler _testedService;

        public UpdateLineItemHandlerTests()
        {
            _testedService = new UpdateLineItemHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenInputOrderIdOfCommandEqualsToZero_ShouldThrowArgumentException()
        {
            int invalidOrderId = 0;
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var request = OrderFixture.CreateUpdateLineItemRequest();
            var command = new UpdateLineItemCommand(invalidOrderId, request);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(invalidOrderId)).ReturnsAsync(lineItems);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.Handle(
                command, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_WhenLineItemExists_ShouldCallUpdateLineItemAsyncOnce()
        {
            int orderId = 1;
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var request = OrderFixture.CreateUpdateLineItemRequest();
            var command = new UpdateLineItemCommand(1, request);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(orderId)).ReturnsAsync(lineItems);

            var actualTestResult = await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.UpdateLineItemAsync(It.IsAny<LineItemModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenLineItemExists_ShouldReturnUpdateLineItemResponse()
        {
            int orderId = 1;
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var request = OrderFixture.CreateUpdateLineItemRequest();
            var command = new UpdateLineItemCommand(1, request);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(orderId)).ReturnsAsync(lineItems);

            var actualTestResult = await _testedService.Handle(command, new CancellationToken());

            Assert.IsType<UpdateLineItemResponse>(actualTestResult);
        }
    }
}
