using Mapster;
using MapsterMapper;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper,IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public Task DeleteProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
        }

        public Task CreateProduct(CreateProductDto productDto)
        {
            var product = _mapper.Map<ProductModel>(productDto);
            return _productRepository.CreateProduct(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            return await _mapper.From(product).AdaptToTypeAsync<ProductDto>();
        }

        public async Task<List<ProductDto>> GetProductsListAsync()
        {
            List<ProductModel> products = await _productRepository.GetProductsListAsync();

            return await _mapper.From(products).AdaptToTypeAsync<List<ProductDto>>();
        }

        public Task UpdateProduct(int productId, UpdateProductDto productDto)
        {
            var product = _mapper.Map<ProductModel>(productDto);
            product.Id = productId;
            return _productRepository.UpdateProduct(product);
        }
    }
}
