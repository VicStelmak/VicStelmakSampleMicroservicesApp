using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;
using VicStelmak.SMA.ProductMicroservice.Domain;

namespace VicStelmak.SMA.ProductMicroservice.Api
{
    public static class EndpointsConfigurator
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // API endpoint mapping
            app.MapGet("/Products", GetProductsList);
            app.MapGet("/Products/{id}", GetProduct);
            app.MapPost("/Products", CreateProduct);
            app.MapPut("/Products", UpdateProduct);
            app.MapDelete("/Products", DeleteProduct);
        }

        private static async Task<IResult> GetProductsList(IProductRepository productRepository)
        {
            try
            {
                return Results.Ok(await productRepository.GetProductsList());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetProduct(int id, IProductRepository productRepository)
        {
            try
            {
                var results = await productRepository.GetProductByIdAsync(id);
                if (results == null) return Results.NotFound();
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CreateProduct(ProductModel product, IProductRepository productRepository)
        {
            try
            {
                await productRepository.CreateProduct(product);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateProduct(ProductModel product, IProductRepository productRepository)
        {
            try
            {
                await productRepository.UpdateProduct(product);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteProduct(int id, IProductRepository productRepository)
        {
            try
            {
                await productRepository.DeleteProduct(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
