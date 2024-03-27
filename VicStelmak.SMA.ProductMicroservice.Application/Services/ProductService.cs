using Mapster;
using MapsterMapper;
using VicStelmak.SMA.ProductMicroservice.Application.DTOs;
using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;
using VicStelmak.SMA.ProductMicroservice.Domain.Models;

namespace VicStelmak.SMA.ProductMicroservice.Application.Services
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

        public Task CreateProduct(ProductCreatingDTO productDTO)
        {
            var product = _mapper.Map<ProductModel>(productDTO);
            return _productRepository.CreateProduct(product);
        }

        public Task DeleteProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
        }

        public async Task<ProductReadingDTO> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            return await _mapper.From(product).AdaptToTypeAsync<ProductReadingDTO>();
        }

        public async Task<List<ProductReadingDTO>> GetProductsList()
        {
            List<ProductModel> products = await _productRepository.GetProductsList();

            return await _mapper.From(products).AdaptToTypeAsync<List<ProductReadingDTO>>();
        }

        public Task UpdateProduct(int productId, ProductUpdatingDTO productDTO)
        {
            var product = _mapper.Map<ProductModel>(productDTO);
            product.Id = productId;
            return _productRepository.UpdateProduct(product);
        }
    }
}
