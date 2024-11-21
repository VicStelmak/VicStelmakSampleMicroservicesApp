using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Application.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductDto productDTO);
        Task DeleteProduct(int productId);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<List<ProductDto>> GetProductsListAsync();
        Task UpdateProductAsync(int productId, UpdateProductDto productDto);
    }
}