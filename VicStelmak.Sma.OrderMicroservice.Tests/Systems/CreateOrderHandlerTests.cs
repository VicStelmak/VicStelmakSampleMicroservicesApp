using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class CreateOrderHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly CreateOrderHandler _testedService;

        public CreateOrderHandlerTests()
        {
            _testedService = new CreateOrderHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCommandInputPayloadIsNotNull_ShouldCallCreateOrderAsyncOnce()
        {
            var createRequest = OrderFixture.MakeCreateOrderRequest();
            var command = new CreateOrderCommand(createRequest, Guid.NewGuid());

            var actualResult = await _testedService.Handle(command, new CancellationToken());

            _repositoryMock.Verify(repository => repository.CreateOrderAsync(
                It.IsAny<OrderModel>(), command.Request.ProductId, command.Request.LineItemTotalPrice), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenCommandInputPayloadIsNotNull_ShouldReturnCreateOrderResponse()
        {
            var createRequest = OrderFixture.MakeCreateOrderRequest();
            var command = new CreateOrderCommand(createRequest, Guid.NewGuid());

            var actualResult = await _testedService.Handle(command, new CancellationToken());

            Assert.IsType<CreateOrderResponse>(actualResult);
        }

        [Fact]
        public async Task Handle_WhenInputOrderCodeOfCommandEqualsToDefaultValue_ShouldThrowArgumentException()
        {
            var createRequest = OrderFixture.MakeCreateOrderRequest();
            var command = new CreateOrderCommand(createRequest, default);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.Handle(
                command, new CancellationToken()));
        }
    }
}
