namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record CreateOrderRequest(
         int Apartment,
         string Building,
         string City,
         string CreatedBy,
         string PostalCode,
         int ProductId,
         int QuantityOfProducts,
         string Status,
         string Street,
         decimal Total,
         bool? UserExists);
}
