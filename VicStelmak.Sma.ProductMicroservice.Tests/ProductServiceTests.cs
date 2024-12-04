using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using VicStelmak.Sma.ProductMicroservice.Application;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Application.Services;
using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Tests
{
    public class ProductServiceTests
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper = MapsterInvoker.GetMapper();
        private readonly Mock<IProductRepository> _repositoryMock = new();
        private readonly ProductService _testedService;

        public ProductServiceTests()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _logger = loggerFactory.CreateLogger<ProductService>();
            _testedService = new ProductService(_logger, _mapper, _repositoryMock.Object);
        }

        [Fact]
        public async Task CreateProductAsync_WhenInputProductDtoIsNotNull_ShouldCallCreateProductAsync()
        {
            var createDtoRequest = ProductFixture.MakeCreateProductDto();

            await _testedService.CreateProductAsync(createDtoRequest);

            _repositoryMock.Verify(repository => repository.CreateProductAsync(It.IsAny<ProductModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_WhenInputProductIdIsEqualToZero_ShouldThrowArgumentException()
        {
            int invalidProductId = 0;

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.DeleteProductAsync(invalidProductId));
        }

        [Fact]
        public async Task DeleteProductAsync_WhenInputProductIdIsNotEqualToZero_ShouldCallDeleteProductAsync()
        {
            int productId = 1;

            await _testedService.DeleteProductAsync(productId);

            _repositoryMock.Verify(repository => repository.DeleteProductAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_WhenProductDoesNotExist_ShouldReturnNull()
        {
            int nonExistentId = 2;
            _repositoryMock.Setup(repository => repository.GetProductByIdAsync(nonExistentId)).ReturnsAsync((ProductModel) null);

            var actualTestResult = await _testedService.GetProductByIdAsync(nonExistentId);

            Assert.Null(actualTestResult);
        }

        [Fact]
        public async Task GetProductByIdAsync_WhenProductExists_ShouldReturnProductDto()
        {
            var product = ProductFixture.CreateProductModel();
            _repositoryMock.Setup(repository => repository.GetProductByIdAsync(product.Id)).ReturnsAsync(product);

            var actualResult = await _testedService.GetProductByIdAsync(product.Id);

            Assert.IsType<ProductDto>(actualResult);
        }

        [Fact]
        public async Task GetProductsListAsync_WhenProductsListIsEmpty_ShouldReturnEmptyList()
        {
            var emptyListOfProducts = new List<ProductModel> { };
            _repositoryMock.Setup(repository => repository.GetProductsListAsync()).ReturnsAsync(emptyListOfProducts);

            var actualTestResult = await _testedService.GetProductsListAsync();

            Assert.Equal(await _mapper.From(emptyListOfProducts).AdaptToTypeAsync<List<ProductDto>>(), actualTestResult);
        }

        [Fact]
        public async Task GetProductsListAsync_WhenProductsListIsNotEmpty_ShouldReturnListOfProductDtos()
        {
            var products = ProductFixture.CreateListOfProductModels();
            _repositoryMock.Setup(repository => repository.GetProductsListAsync()).ReturnsAsync(products);

            var actualTestResult = await _testedService.GetProductsListAsync();

            Assert.IsType<List<ProductDto>>(actualTestResult);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenInputProductIdEqualsToZero_ShouldThrowArgumentException()
        {
            int invalidProductId = 0;
            var updateRequest = ProductFixture.CreateUpdateProductDto();

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.UpdateProductAsync(invalidProductId, updateRequest));
        }

        [Fact]
        public async Task UpdateProductAsync_WhenInputProductIdIsNotEqualToZeroAndInputUpdateRequestIsNotNull_ShouldCallUpdateProduct()
        {
            int validProductId = 1;
            var updateRequest = ProductFixture.CreateUpdateProductDto();

            await _testedService.UpdateProductAsync(validProductId, updateRequest);

            _repositoryMock.Verify(repository => repository.UpdateProductAsync(It.IsAny<ProductModel>()), Times.Once);
        }
    }
}
