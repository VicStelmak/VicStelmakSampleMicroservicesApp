namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models
{
    internal class LineItemModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
