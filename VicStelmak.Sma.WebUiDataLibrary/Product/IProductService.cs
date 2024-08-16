using VicStelmak.Sma.WebUiDataLibrary.Product.Dtos;

namespace VicStelmak.Sma.WebUiDataLibrary.Product
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