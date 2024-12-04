using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Application.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(ProductModel product);
        Task DeleteProductAsync(int productId);
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<List<ProductModel>> GetProductsListAsync();
        Task UpdateProductAsync(ProductModel product);
    }
}