namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models
{
    internal class OrderModel
    {
        public int Id { get; set; }

        public int Apartment { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string OrderCode { get; set; }

        public string PostalCode { get; set; }

        public int QuantityOfProducts { get; set; }

        public string Status { get; set; }

        public string Street { get; set; }

        public decimal Total { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
