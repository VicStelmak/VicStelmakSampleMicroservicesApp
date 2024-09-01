using MassTransit;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.SagaMicroservice.ApiDataLibrary
{
    public class OrderSagaStateMachine : MassTransitStateMachine<OrderSagaStateData>
    {
        public State OrderCreated { get; private set; }
        public State OrderCreating { get; private set; }
        public State OrderDeleted { get; private set; }
        public State OrderSubmitted { get; private set; }
        public State UserCreating { get; private set; }

        public Event<OrderCreated> OrderCreatedEvent { get; private set; }
        public Event<OrderCreating> OrderCreatingEvent { get; private set; }
        public Event<OrderDeleted> OrderDeletedEvent { get; private set; }
        public Event<OrderSubmitting> OrderSubmittingEvent { get; private set; }
        public Event<UserCreating> UserCreatingEvent { get; private set; }

        public OrderSagaStateMachine()
        {
            InstanceState(state => state.CurrentState);
            Event(() => OrderCreatedEvent, configurator => configurator.CorrelateById(state => state.Message.OrderCode));
            Event(() => OrderCreatingEvent, configurator => configurator.CorrelateById(state => state.Message.OrderCode));
            Event(() => OrderDeletedEvent, configurator => configurator.CorrelateById(state => state.Message.OrderCode));
            Event(() => OrderSubmittingEvent, configurator => configurator.CorrelateById(state => state.Message.OrderCode));
            Event(() => UserCreatingEvent, configurator => configurator.CorrelateById(state => state.Message.OrderCode));

            Initially(
                When(OrderDeletedEvent).Then(context => 
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.OrderCode = context.Message.OrderCode;
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.QuantityOfProducts = context.Message.QuantityOfProducts;
                }).Finalize(),
                When(OrderCreatedEvent).Then(context =>
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.OrderCode = context.Message.OrderCode;
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.QuantityOfProducts = context.Message.QuantityOfProducts;
                }).TransitionTo(OrderCreated),
                When(OrderSubmittingEvent).Then(context =>
                {
                    context.Saga.Apartment = context.Message.Apartment;
                    context.Saga.Building = context.Message.Building;
                    context.Saga.City = context.Message.City;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.OrderCode = context.Message.OrderCode;
                    context.Saga.PostalCode = context.Message.PostalCode;
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.QuantityOfProducts = context.Message.QuantityOfProducts;
                    context.Saga.Street = context.Message.Street;
                    context.Saga.Total = context.Message.Total;
                }).TransitionTo(OrderSubmitted).PublishAsync(context => context.Init<UserCreating>(new
                {
                    Apartment = context.Saga.Apartment,
                    Building = context.Saga.Building,
                    City = context.Saga.City,
                    Email = context.Saga.Email,
                    OrderCode = context.Saga.OrderCode,
                    PostalCode = context.Saga.PostalCode,
                    ProductId = context.Saga.ProductId,
                    QuantityOfProducts = context.Saga.QuantityOfProducts,
                    Street = context.Saga.Street,
                    Total = context.Saga.Total,
                })));

            During(OrderSubmitted,
                When(UserCreatingEvent).TransitionTo(UserCreating));

            During(UserCreating, 
                When(OrderCreatingEvent).TransitionTo(OrderCreating));

            During(OrderCreating,
                When(OrderCreatedEvent).TransitionTo(OrderCreated));

            During(OrderCreated,
               When(OrderDeletedEvent).Then(context => 
               {
                   context.Saga.Email = context.Message.Email;
                   context.Saga.OrderCode = context.Message.OrderCode;
                   context.Saga.ProductId = context.Message.ProductId;
                   context.Saga.QuantityOfProducts = context.Message.QuantityOfProducts;
               }).Finalize(),
               When(OrderCreatedEvent).Then(context =>
               {
                   context.Saga.Email = context.Message.Email;
                   context.Saga.OrderCode = context.Message.OrderCode;
                   context.Saga.ProductId = context.Message.ProductId;
                   context.Saga.QuantityOfProducts = context.Message.QuantityOfProducts;
               }).Finalize());

            SetCompletedWhenFinalized();
        }
    }
}
