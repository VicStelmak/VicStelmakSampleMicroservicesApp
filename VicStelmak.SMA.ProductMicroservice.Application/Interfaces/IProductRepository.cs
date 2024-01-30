using VicStelmak.SMA.ProductMicroservice.Domain;

namespace VicStelmak.SMA.ProductMicroservice.Application.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(ProductModel product);
        Task DeleteProduct(int productId);
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<List<ProductModel>> GetProductsList();
        Task UpdateProduct(ProductModel product);
    }
}