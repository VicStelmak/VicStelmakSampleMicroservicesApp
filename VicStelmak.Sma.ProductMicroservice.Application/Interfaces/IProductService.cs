using VicStelmak.Sma.ProductMicroservice.Application.Dtos;

namespace VicStelmak.Sma.ProductMicroservice.Application.Interfaces
{
    public interface IProductService
    {
        Task DeleteProduct(int productId);
        Task CreateProduct(CreateProductDto productDTO);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<List<ProductDto>> GetProductsListAsync();
        Task UpdateProduct(int productId, UpdateProductDto product);
    }
}