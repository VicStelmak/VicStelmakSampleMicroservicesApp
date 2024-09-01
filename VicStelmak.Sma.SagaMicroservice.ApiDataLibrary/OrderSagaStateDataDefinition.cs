using MassTransit;
using VicStelmak.Sma.Events;

namespace VicStelmak.Sma.SagaMicroservice.ApiDataLibrary
{
    public sealed class OrderSagaStateDataDefinition : SagaDefinition<OrderSagaStateData>
    {
        private const int ConcurrencyLimit = 20; 

        public OrderSagaStateDataDefinition()
        {
            Endpoint(configurator =>
            {
                configurator.PrefetchCount = ConcurrencyLimit;
            });
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,  ISagaConfigurator<OrderSagaStateData> sagaConfigurator, 
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox(context);

            var partition = endpointConfigurator.CreatePartitioner(ConcurrencyLimit);

            sagaConfigurator.Message<OrderCreated>(configurator => configurator.UsePartitioner(partition, context => context.Message.OrderCode));
            sagaConfigurator.Message<OrderCreating>(configurator => configurator.UsePartitioner(partition, context => context.Message.OrderCode));
            sagaConfigurator.Message<OrderDeleted>(configurator => configurator.UsePartitioner(partition, context => context.Message.OrderCode));
            sagaConfigurator.Message<UserCreating>(configurator => configurator.UsePartitioner(partition, context => context.Message.OrderCode));
        }
    }
}
