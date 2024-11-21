using Mapster;
using MapsterMapper;
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
        private readonly IMapper _mapper = MapsterInvoker.GetMapper();
        private readonly Mock<IProductRepository> _repositoryMock = new();
        private readonly ProductService _testedService;

        public ProductServiceTests()
        {
            _testedService = new ProductService(_mapper, _repositoryMock.Object);
        }

        [Fact]
        public async Task CreateProduct_WhenInputProductDtoIsNotNull_ShouldCallCreateProduct()
        {
            var createDtoRequest = ProductFixture.MakeCreateProductDto();

            await _testedService.CreateProduct(createDtoRequest);

            _repositoryMock.Verify(repository => repository.CreateProduct(It.IsAny<ProductModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_WhenInputProductIdIsEqualToZero_ShouldThrowArgumentException()
        {
            int invalidProductId = 0;

            await Assert.ThrowsAsync<ArgumentException>(async () => await _testedService.DeleteProduct(invalidProductId));
        }

        [Fact]
        public void DeleteProduct_WhenInputProductIdIsNotEqualToZero_ShouldCallDeleteProduct()
        {
            int productId = 1;

            _testedService.DeleteProduct(productId);

            _repositoryMock.Verify(repository => repository.DeleteProduct(It.IsAny<int>()), Times.Once);
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
