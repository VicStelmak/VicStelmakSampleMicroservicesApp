namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record UpdateOrderRequest(
        int Apartment,
        string Building,
        string City,
        string PostalCode,
        int QuantityOfProducts,
        string Status,
        string Street,
        decimal Total,
        string UpdatedBy);
}
