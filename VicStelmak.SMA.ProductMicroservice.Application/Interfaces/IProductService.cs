using VicStelmak.SMA.ProductMicroservice.Application.DTOs;

namespace VicStelmak.SMA.ProductMicroservice.Application.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductCreatingDTO productDTO);
        Task DeleteProduct(int productId);
        Task<ProductReadingDTO> GetProductByIdAsync(int productId);
        Task<List<ProductReadingDTO>> GetProductsList();
        Task UpdateProduct(int productId, ProductUpdatingDTO product);
    }
}