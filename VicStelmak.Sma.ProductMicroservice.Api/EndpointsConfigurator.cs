using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Domain.Enums;

namespace VicStelmak.Sma.ProductMicroservice.Api
{
    public static class EndpointsConfigurator
{
        public static void ConfigureApi(this WebApplication application)
        {
            // API endpoint mapping
            application.MapPost("api/products", CreateProductAsync).RequireAuthorization(new AuthorizeAttribute()
            {
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User)
            });
            application.MapDelete("api/products/{productId}", DeleteProductAsync).RequireAuthorization(new AuthorizeAttribute()
            {
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User)
            });
            application.MapGet("api/products", GetProductsListAsync);
            application.MapGet("api/products/{productId}", GetProductByIdAsync);
            application.MapPut("api/products/{productId}", UpdateProduct).RequireAuthorization(new AuthorizeAttribute()
            {
                Roles = nameof(Role.Administrator) + "," + nameof(Role.User)
            });
        }

        private async static Task<IResult> CreateProductAsync(CreateProductDto productDto, [FromServices] ILoggerFactory loggerFactory, 
            IProductService productService)
        {
            try
            {
                await productService.CreateProductAsync(productDto);

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(EndpointsConfigurator));    

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private async static Task<IResult> DeleteProductAsync([FromServices] ILoggerFactory loggerFactory, int productId, 
            IProductService productService)
        {
            try
            {
                await productService.DeleteProductAsync(productId);

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(EndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> GetProductsListAsync([FromServices] ILoggerFactory loggerFactory, IProductService productService)
        {
            try
            {
                return Results.Ok(await productService.GetProductsListAsync());
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(EndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> GetProductByIdAsync([FromServices] ILoggerFactory loggerFactory, int productId, 
            IProductService productService)
        {
            try
            {
                var results = await productService.GetProductByIdAsync(productId);

                if (results == null) return Results.NotFound();

                return Results.Ok(results);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(EndpointsConfigurator));

                logger.LogCritical(exception.ToString());

                return Results.Problem(exception.Message);
            }
        }

        private static async Task<IResult> UpdateProduct([FromServices] ILoggerFactory loggerFactory, int productId, UpdateProductDto product, 
            IProductService productService)
        {
            try
            {
                await productService.UpdateProductAsync(productId, product);

                return Results.Ok();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(EndpointsConfigurator));

                if (exception is ArgumentException)
                {
                    logger.LogError(exception.ToString());
                }
                else
                {
                    logger.LogCritical(exception.ToString());
                }
                
                return Results.Problem(exception.Message);
            }
        }
    }
}
