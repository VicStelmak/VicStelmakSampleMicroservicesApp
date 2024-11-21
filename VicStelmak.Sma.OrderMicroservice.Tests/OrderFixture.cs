using MassTransit.Transports;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Enums;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.Tests
{
    internal static class OrderFixture
    {
        internal static List<LineItemModel> CreateListOfLineItemModels()
        {
            return new List<LineItemModel>
            {
                new LineItemModel()
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 1,
                    TotalPrice = 7.99m,
                },
                new LineItemModel()
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 5,
                    TotalPrice = 5
                },
                new LineItemModel()
                {
                    OrderId = 1,
                    ProductId = 3,
                    Quantity = 7,
                    TotalPrice= 9
                },
            };
        }

        internal static List<OrderModel> CreateListOfOrderModels()
        {
            return new List<OrderModel>
            {
                new OrderModel()
                {
                    Id = 1,
                    Apartment = 3,
                    Building = "7A",
                    City = "Testville",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test@email.com",
                    OrderCode = Guid.NewGuid().ToString(),
                    PostalCode = "12345",
                    QuantityOfProducts = 7,
                    Status = OrderStatus.Pending.ToString(),
                    Street = "Test Road",
                    Total = 7.99m,
                    UpdatedAt = default(DateTime),
                    UpdatedBy = null
                },
                new OrderModel()
                {
                    Id = 2,
                    Apartment = 5,
                    Building = "5A",
                    City = "Testville",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test2@email.com",
                    OrderCode = Guid.NewGuid().ToString(),
                    PostalCode = "54321",
                    QuantityOfProducts = 9,
                    Status = OrderStatus.Pending.ToString(),
                    Street = "Test Ave.",
                    Total = 9,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "test2@email.com"
                },
                new OrderModel()
                {
                    Id = 3,
                    Apartment = 7,
                    Building = "7",
                    City = "Testville",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "test3@email.com",
                    OrderCode = Guid.NewGuid().ToString(),
                    PostalCode = "35790",
                    QuantityOfProducts = 5,
                    Status = OrderStatus.Pending.ToString(),
                    Street = "Test Street",
                    Total = 5,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "test@email.com"
                },
            };
        }

        internal static OrderModel CreateOrderModel()
        {
            return new OrderModel
            {
                Id = 1,
                Apartment = 3,
                Building = "7A",
                City = "Testville",
                CreatedAt = DateTime.Now,
                CreatedBy = "test@email.com",
                OrderCode = Guid.NewGuid().ToString(),
                PostalCode = "12345",
                QuantityOfProducts = 7,
                Status = OrderStatus.Pending.ToString(),
                Street = "Test Road",
                Total = 7.99m,
                UpdatedAt = default(DateTime),
                UpdatedBy = null
            };
        }

        internal static UpdateLineItemRequest CreateUpdateLineItemRequest()
        {
            return new UpdateLineItemRequest(
                Guid.NewGuid().ToString(),
                1,
                5,
                9,
                "test@email.com");
        }

        internal static UpdateOrderRequest CreateUpdateOrderRequest()
        {
            return new UpdateOrderRequest(
                3,
                "7A",
                "Testville",
                "12345",
                9,
                OrderStatus.Pending.ToString(),
                "Test Road",
                7,
                "test@email.com");
        }

        internal static CreateOrderRequest MakeCreateOrderRequest()
        {
            return new CreateOrderRequest(
                3,
                "7A", 
                "Testville", 
                "test@email.com", 
                9, 
                "12345",
                1, 
                7, 
                "Test Road", 
                9);
        }
    }
}
