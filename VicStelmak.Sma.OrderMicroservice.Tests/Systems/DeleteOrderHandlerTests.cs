using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class DeleteOrderHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly DeleteOrderHandler _testedService;

        public DeleteOrderHandlerTests()
        {
            _testedService = new DeleteOrderHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldCallDeleteOrderAsyncOnce()
        {
            int orderId = 1;
            var deleteCommandIssuer = "test@email.com";
            var command = new DeleteOrderCommand(deleteCommandIssuer, orderId);
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.orderId)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(command.orderId)).ReturnsAsync(lineItems);

            await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.DeleteOrderAsync(command.orderId), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnDeleteOrderResponse()
        {
            int orderId = 1;
            var deleteCommandIssuer = "test@email.com";
            var command = new DeleteOrderCommand(deleteCommandIssuer, orderId);
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.orderId)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(command.orderId)).ReturnsAsync(lineItems);

            var actualResult = await _testedService.Handle(command, new CancellationToken());

            Assert.IsType<DeleteOrderResponse>(actualResult);
        }
    }
}
