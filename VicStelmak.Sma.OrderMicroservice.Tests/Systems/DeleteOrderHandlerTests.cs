using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class DeleteOrderHandlerTests
    {
        private readonly ILogger<DeleteOrderHandler> _logger;
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly DeleteOrderHandler _testedService;

        public DeleteOrderHandlerTests()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _logger = loggerFactory.CreateLogger<DeleteOrderHandler>();
            _testedService = new DeleteOrderHandler(_logger, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldCallDeleteOrderAsyncOnce()
        {
            int orderId = 1;
            var deleteCommandIssuer = "test@email.com";
            var command = new DeleteOrderCommand(deleteCommandIssuer, orderId);
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.OrderId)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(command.OrderId)).ReturnsAsync(lineItems);

            await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.DeleteOrderAsync(command.OrderId), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnDeleteOrderResponse()
        {
            int orderId = 1;
            var deleteCommandIssuer = "test@email.com";
            var command = new DeleteOrderCommand(deleteCommandIssuer, orderId);
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var order = OrderFixture.CreateOrderModel();
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(command.OrderId)).ReturnsAsync(order);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(command.OrderId)).ReturnsAsync(lineItems);

            var actualResult = await _testedService.Handle(command, new CancellationToken());

            Assert.IsType<DeleteOrderResponse>(actualResult);
        }
    }
}
