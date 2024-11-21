using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class CreateLineItemHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly CreateLineItemHandler _testedService;

        public CreateLineItemHandlerTests()
        {
            _testedService = new CreateLineItemHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenInputOrderIdOfRequestInCommandEqualsToZero_ShouldThrowArgumentException()
        {
            int invalidOrderId = 0;
            var order = OrderFixture.CreateOrderModel();
            CreateLineItemRequest createRequest = new CreateLineItemRequest(invalidOrderId, order.CreatedBy, 1, 7, 9);
            var command = new CreateLineItemCommand(createRequest);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.Request.OrderId)).ReturnsAsync(order);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.Handle(
                command, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldCallCreateLineItemAsyncOnce()
        {
            var order = OrderFixture.CreateOrderModel();
            CreateLineItemRequest createRequest = new CreateLineItemRequest(order.Id, order.CreatedBy, 1, 7, 9);
            var command = new CreateLineItemCommand(createRequest);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.Request.OrderId)).ReturnsAsync(order);

            await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.CreateLineItemAsync(It.IsAny<LineItemModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnCreateLineItemResponse()
        {
            var order = OrderFixture.CreateOrderModel();
            CreateLineItemRequest createRequest = new CreateLineItemRequest(order.Id, order.CreatedBy, 1, 7, 9);
            var command = new CreateLineItemCommand(createRequest);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.Request.OrderId)).ReturnsAsync(order);

            var actualResult = await _testedService.Handle(command, new CancellationToken());

            Assert.IsType<CreateLineItemResponse>(actualResult);
        }
    }
}
