using VicStelmak.SMA.ProductMicroservice.Domain.Models;

namespace VicStelmak.SMA.ProductMicroservice.Application.Interfaces
{
    public interface IProductRepository
    {
        Task DeleteProduct(int productId);
        Task CreateProduct(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<List<ProductModel>> GetProductsListAsync();
        Task UpdateProduct(ProductModel product);
    }
}