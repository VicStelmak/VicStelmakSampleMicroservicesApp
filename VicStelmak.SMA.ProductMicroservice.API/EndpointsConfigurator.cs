﻿using VicStelmak.SMA.ProductMicroservice.Application.DTOs;
using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;

namespace VicStelmak.SMA.ProductMicroservice.Api
{
    public static class EndpointsConfigurator
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // API endpoint mapping
            app.MapGet("api/products", GetProductsList);
            app.MapGet("api/products/{id}", GetProduct);
            app.MapPost("api/products", CreateProduct);
            app.MapPut("api/products", UpdateProduct);
            app.MapDelete("api/products", DeleteProduct);
        }

        private static async Task<IResult> GetProductsList(IProductService productService)
        {
            try
            {
                return Results.Ok(await productService.GetProductsList());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetProduct(int productId, IProductService productService)
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

        private static async Task<IResult> CreateProduct(ProductCreatingDTO productDTO, IProductService productService)
        {
            try
            {
                await productService.CreateProduct(productDTO);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateProduct(int productId, ProductUpdatingDTO product, IProductService productService)
        {
            try
            {
                await productService.UpdateProduct(productId, product);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteProduct(int productId, IProductService productService)
        {
            try
            {
                await productService.DeleteProduct(productId);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
