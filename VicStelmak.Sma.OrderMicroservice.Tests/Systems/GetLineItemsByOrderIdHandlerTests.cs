using Moq;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests.Systems
{
    public class GetLineItemsByOrderIdHandlerTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly GetLineItemsByOrderIdHandler _testedService;

        public GetLineItemsByOrderIdHandlerTests()
        {
            _testedService = new GetLineItemsByOrderIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenLineItemsListIsEmptyAndOrderDoesNotExist_ShouldReturnEmptyList()
        {
            int nonExistentOrderId = 2;
            var emptyListOfLineItems = new List<LineItemModel>();
            var query = new GetLineItemsByOrderIdQuery(nonExistentOrderId);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(nonExistentOrderId)).ReturnsAsync(emptyListOfLineItems);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.Equal(emptyListOfLineItems.Select(lineItem => lineItem.MapToGetLineItemsResponse()).ToList(), actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenOrderExists_ShouldReturnListOfGetLineItemsResponses()
        {
            int orderId = 1;
            var lineItems = OrderFixture.CreateListOfLineItemModels();
            var query = new GetLineItemsByOrderIdQuery(orderId);
            _repositoryMock.Setup(repository => repository.GetLineItemsByOrderIdAsync(orderId)).ReturnsAsync(lineItems);

            var actualTestResult = await _testedService.Handle(query, new CancellationToken());

            Assert.IsType<List<GetLineItemsResponse>>(actualTestResult);
        }

        [Fact]
        public async Task Handle_WhenQueryInputOrderIdEqualsToZero_ShouldThrowArgumentException()
        {
            var incorrectOrderId = 0;
            var query = new GetLineItemsByOrderIdQuery(incorrectOrderId);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.Handle(
                query, new CancellationToken()));
        }
    }
}
