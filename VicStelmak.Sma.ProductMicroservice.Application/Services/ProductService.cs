using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IMapper mapper, IProductRepository productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<ProductModel>(productDto);

            await _productRepository.CreateProductAsync(product);

            _logger.LogInformation("Product with name {productName} and costing {productPrice} credits was created {date} by {userName} at " +
                "{time} Utc.", 
                product.Name, product.Price, DateTime.UtcNow.ToShortDateString(), product.CreatedBy, DateTime.UtcNow.ToLongTimeString());
        }

        public async Task DeleteProductAsync(int productId)
        {
            if (productId == 0) throw new ArgumentException();
            
            await _productRepository.DeleteProductAsync(productId);

            _logger.LogInformation("Product with Id {productId} was deleted {date} at {time} Utc.",
                productId, DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());
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

        public async Task UpdateProductAsync(int productId, UpdateProductDto productDto)
        {
            if (productId == 0) throw new ArgumentException(); 

            var product = _mapper.Map<ProductModel>(productDto);
            product.Id = productId;

            await _productRepository.UpdateProductAsync(product);

            _logger.LogInformation("Product with Id {productId} was updated {date} by {userName} at {time} Utc.",
                product.Id, DateTime.UtcNow.ToShortDateString(), product.UpdatedBy, DateTime.UtcNow.ToLongTimeString());
        }
    }
}
