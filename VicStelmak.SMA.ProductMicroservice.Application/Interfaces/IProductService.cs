using VicStelmak.SMA.ProductMicroservice.Application.Dtos;

namespace VicStelmak.SMA.ProductMicroservice.Application.Interfaces
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