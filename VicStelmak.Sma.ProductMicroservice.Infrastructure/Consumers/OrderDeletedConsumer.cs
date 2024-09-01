using MassTransit;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.ProductMicroservice.Application.Dtos;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;

namespace VicStelmak.Sma.ProductMicroservice.Infrastructure.Consumers
{
    public class OrderDeletedConsumer : IConsumer<OrderDeleted>
    {
        private readonly IProductService _productService;

        public OrderDeletedConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<OrderDeleted> context)
        {
            var messageContents = context.Message;

            if (messageContents is not null)
            {
                if (messageContents.ProductId is not default(int))
                {
                    var product = await _productService.GetProductByIdAsync(messageContents.ProductId);
                    var updateRequest = new UpdateProductDto(
                        product.AmountInStock + messageContents.QuantityOfProducts,
                        product.AmountSold - messageContents.QuantityOfProducts,
                        messageContents.Email,
                        product.Description,
                        product.ImageUri,
                        product.Name,
                        product.Price);

                    await _productService.UpdateProductAsync(product.Id, updateRequest);
                }
            }
        }
    }
}
