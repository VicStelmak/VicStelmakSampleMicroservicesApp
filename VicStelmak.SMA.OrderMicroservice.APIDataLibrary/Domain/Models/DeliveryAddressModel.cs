namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Domain.Models
{
    internal class DeliveryAddressModel
    {
        public int Id { get; set; }

        public int Apartment { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public int OrderId { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }
    }
}
