using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Domain.Models;

namespace VicStelmak.Sma.ProductMicroservice.Tests
{
    internal static class ProductFixture
    {
        internal static List<ProductModel> CreateListOfProductModels()
        {
            return new List<ProductModel>
            {
                new ProductModel()
                {
                    Id = 1,
                    AmountInStock = 1,
                    AmountSold = 7,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test@email.com",
                    Description = "Some description",
                    ImageUri = null,
                    Name = "Tester",
                    Price = 7.99m,
                    UpdatedAt = default(DateTime),
                    UpdatedBy = null
                },
                new ProductModel()
                {
                    Id = 2,
                    AmountInStock = 2,
                    AmountSold = 9,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test2@email.com",
                    Description = "Another description",
                    ImageUri = "https://m.media-amazon.com/images/I/61tsskwGaNL._AC_SL1500_.jpg",
                    Name = "Screwdriver",
                    Price = 9,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "test@email.com"
                },
                new ProductModel()
                {
                    Id = 3,
                    AmountInStock = 5,
                    AmountSold = 3,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test3@email.com",
                    Description = "Another random description",
                    ImageUri = null,
                    Name = "Axe",
                    Price = 19.05m,
                    UpdatedAt = default(DateTime),
                    UpdatedBy = null
                },
                new ProductModel()
                {
                    Id = 4,
                    AmountInStock = 7,
                    AmountSold = 5,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test4@email.com",
                    Description = "And another random description",
                    ImageUri = null,
                    Name = "Boomerang",
                    Price = 17,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "test4@email.com"
                },
                new ProductModel()
                {
                    Id = 5,
                    AmountInStock = 9,
                    AmountSold = 300,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test@email.com",
                    Description = "And another one super random description",
                    ImageUri = null,
                    Name = "Cards Deck",
                    Price = 6,
                    UpdatedAt = default(DateTime),
                    UpdatedBy = null
                },
            };
        }

        internal static ProductModel CreateProductModel()
        {
            return new ProductModel
            { 
                Id = 1,
                AmountInStock = 1,
                AmountSold = 7,
                CreatedAt = DateTime.Now,
                CreatedBy = "test@email.com",
                Description = "Some description",
                ImageUri = null,
                Name = "Tester",
                Price = 7.99m,
                UpdatedAt = default(DateTime),
                UpdatedBy = null
            };
        }

        internal static UpdateProductDto CreateUpdateProductDto()
        {
            return new UpdateProductDto(
                1,
                1,
                "test@email.com",
                "Some description",
                null,
                "Tester",
                7.99m);
        }

        internal static CreateProductDto MakeCreateProductDto()
        {
            return new CreateProductDto(
                1,
                1,
                "test@email.com",
                "Some description",
                null,
                "Tester",
                7.99m);
        }
    }
}
