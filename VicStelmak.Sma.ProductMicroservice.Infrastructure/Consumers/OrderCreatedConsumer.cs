using MassTransit;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;

namespace VicStelmak.Sma.ProductMicroservice.Infrastructure.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IProductService _productService;

        public OrderCreatedConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var messageContents = context.Message;

            if (messageContents is not null)
            {
                var product = await _productService.GetProductByIdAsync(messageContents.ProductId);
                var updateRequest = new UpdateProductDto(
                    product.AmountInStock - messageContents.QuantityOfProducts,
                    product.AmountSold + messageContents.QuantityOfProducts,
                    messageContents.Email,
                    product.Description,
                    product.ImageUri,
                    product.Name,
                    product.Price);

                _productService.UpdateProduct(product.Id, updateRequest);
            }
        }
    }
}
