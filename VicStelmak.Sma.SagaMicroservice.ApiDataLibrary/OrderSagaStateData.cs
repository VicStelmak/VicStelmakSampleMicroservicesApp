using MassTransit;

namespace VicStelmak.Sma.SagaMicroservice.ApiDataLibrary
{
    public class OrderSagaStateData : SagaStateMachineInstance, ISagaVersion
    {
        public int Apartment { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public string Email { get; set; }

        public Guid OrderCode { get; set; }

        public string PostalCode { get; set; }

        public int ProductId { get; set; }

        public int QuantityOfProducts { get; set; }

        public string Street { get; set; }

        public decimal Total { get; set; }

        public int Version { get; set; }
    }
}
