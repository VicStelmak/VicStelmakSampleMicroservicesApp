using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;
using VicStelmak.SMA.ProductMicroservice.Domain.Models;

namespace VicStelmak.SMA.ProductMicroservice.Infrastructure.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDbAccess _dbAccess;

        public ProductRepository(ISqlDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Task CreateProduct(ProductModel product) => _dbAccess.SaveDataAsync("spproducts_addproduct", new
        {
            arg_amount_in_stock = product.AmountInStock,
            arg_amount_sold = product.AmountSold,
            arg_created_by = product.CreatedBy,
            arg_description = product.Description,
            arg_image_uri = product.ImageUri,
            arg_name = product.Name,
            arg_price = product.Price
        });

        public Task DeleteProduct(int productId) => _dbAccess.SaveDataAsync("spproducts_deleteproduct", new { arg_id = productId });

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            var getProductResult = await _dbAccess.LoadDataAsync<ProductModel, dynamic>("SELECT * FROM funcproducts_getproductbyid(:arg_id)",
                new { arg_id = productId });

            return getProductResult.FirstOrDefault();
        }
        
        public async Task<List<ProductModel>> GetProductsList() => 
            await _dbAccess.LoadDataAsync<ProductModel, dynamic> ("SELECT * FROM funcproducts_getproducts()", new { });

        public Task UpdateProduct(ProductModel product) => _dbAccess.SaveDataAsync("spproducts_updateproduct", new
        {
            arg_id = product.Id,
            arg_amount_in_stock = product.AmountInStock,
            arg_amount_sold = product.AmountSold,
            arg_description = product.Description,
            arg_image_uri = product.ImageUri,
            arg_name = product.Name,
            arg_price = product.Price,
            arg_updated_by = product.UpdatedBy
        });
    }
}
