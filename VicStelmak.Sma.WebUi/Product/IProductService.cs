using VicStelmak.Sma.WebUi.Product.Dtos;

namespace VicStelmak.Sma.WebUi.Product
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductDto productDto);
        Task DeleteProductAsync(int ProductId);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<List<ProductDto>> GetProductsListAsync();
        Task UpdateProductAsync(int productId, UpdateProductDto productDto);
    }
}