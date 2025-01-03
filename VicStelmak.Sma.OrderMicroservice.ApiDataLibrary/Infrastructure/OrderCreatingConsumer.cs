﻿using MassTransit;
using MediatR;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Infrastructure
{
    public class OrderCreatingConsumer : IConsumer<OrderCreating>
    {
        private readonly IMediator _mediator;

        public OrderCreatingConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderCreating> context)
        {
            var messageContents = context.Message;

            if (messageContents is not null)
            {
                var request = messageContents.MapToCreateOrderRequest();

                var orderCreatingResponse = await _mediator.Send(new CreateOrderCommand(request, messageContents.OrderCode));

                await _mediator.Send(new PublishOrderCreatedCommand(orderCreatingResponse.MapToPublishOrderCreatedRequest()));
            }
        }
    }
}
