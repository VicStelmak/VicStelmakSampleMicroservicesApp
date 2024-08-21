using MassTransit;
using VicStelmak.Sma.Events;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Requests;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Utils;

namespace VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Infrastructure
{
    internal class UserCreatingConsumer : IConsumer<UserCreating>
    {
        private readonly IUserService _userService;

        public UserCreatingConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserCreating> context)
        {
            var messageContents = context.Message;

            if (messageContents is not null)
            {
                string randomPassword = UserServiceUtils.GeneratePassword();

                var userCreatingRequest = new CreateUserRequest(messageContents.Email, null, null, randomPassword, randomPassword);

                var userCreatingResult = await _userService.CreateUserAsync(userCreatingRequest);

                if (userCreatingResult.ActionIsSuccessful == true)
                {
                    await context.Publish<OrderCreating>(new
                    {
                        Apartment = messageContents.Apartment,

                        Building = messageContents.Building,

                        City = messageContents.City,

                        Email = messageContents.Email,

                        OrderCode = messageContents.OrderCode,

                        PostalCode = messageContents.PostalCode,

                        ProductId = messageContents.ProductId,

                        QuantityOfProducts = messageContents.QuantityOfProducts,

                        Street = messageContents.Street,

                        Total = messageContents.Total
                    });
                }
            }
        }
    }
}
