using MassTransit.Transports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class UpdateOrderHandlerTests
    {
        private readonly ILogger<UpdateOrderHandler> _logger;
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly UpdateOrderHandler _testedService;

        public UpdateOrderHandlerTests()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _logger = loggerFactory.CreateLogger<UpdateOrderHandler>();
            _testedService = new UpdateOrderHandler(_logger, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCommandInputOrderIdEqualsToZero_ShouldThrowArgumentException()
        {
            int invalidOrderId = 0;
            var request = OrderFixture.CreateUpdateOrderRequest();
            var command = new UpdateOrderCommand(invalidOrderId, request);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.Handle(
                command, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_WhenCommandOrderIdNotEqualToZeroAndRequestIsNotNull_ShouldCallUpdateOrderAsyncOnce()
        {
            var order = OrderFixture.CreateOrderModel();
            int orderId = 1;
            var request = OrderFixture.CreateUpdateOrderRequest();
            var command = new UpdateOrderCommand(orderId, request);
            _repositoryMock.Setup(repository => repository.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.UpdateOrderAsync(It.IsAny<OrderModel>()), Times.Once);
        }
    }
}
