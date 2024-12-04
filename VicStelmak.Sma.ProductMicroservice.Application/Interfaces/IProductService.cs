using VicStelmak.Sma.ProductMicroservice.Application.Dtos;

namespace VicStelmak.Sma.ProductMicroservice.Application.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductDto productDTO);
        Task DeleteProductAsync(int productId);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<List<ProductDto>> GetProductsListAsync();
        Task UpdateProductAsync(int productId, UpdateProductDto productDto);
    }
}