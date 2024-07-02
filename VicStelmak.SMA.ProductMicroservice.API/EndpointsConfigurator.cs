using Microsoft.AspNetCore.Authorization;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Domain.Enums;

namespace VicStelmak.Sma.ProductMicroservice.Api
{
    public static class EndpointsConfigurator
{
        public static void ConfigureApi(this WebApplication app)
        {
            // API endpoint mapping
            app.MapDelete("api/products/{productId}", DeleteProduct).RequireAuthorization(new AuthorizeAttribute() 
            { 
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User) 
            });
            app.MapPost("api/products", CreateProduct).RequireAuthorization(new AuthorizeAttribute()
            {
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User)
            });
            app.MapGet("api/products", GetProductsListAsync);
            app.MapGet("api/products/{productId}", GetProductByIdAsync);
            app.MapPut("api/products/{productId}", UpdateProduct).RequireAuthorization(new AuthorizeAttribute()
            {
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User)
            });
        }

        private static IResult DeleteProduct(int productId, IProductService productService)
        {
            try
            {
                productService.DeleteProduct(productId);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static IResult CreateProduct(CreateProductDto productDto, IProductService productService)
        {
            try
            {
                productService.CreateProduct(productDto);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetProductsListAsync(IProductService productService)
        {
            try
            {
                return Results.Ok(await productService.GetProductsListAsync());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetProductByIdAsync(int productId, IProductService productService)
        {
            try
            {
                var results = await productService.GetProductByIdAsync(productId);
                if (results == null) return Results.NotFound();
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static IResult UpdateProduct(int productId, UpdateProductDto product, IProductService productService)
        {
            try
            {
                productService.UpdateProduct(productId, product);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
