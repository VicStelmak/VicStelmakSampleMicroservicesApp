namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record CreateOrderRequest(
        int Apartment,
        string Building,
        string City,
        string CreatedBy,
        decimal LineItemTotalPrice,
        string PostalCode,
        int ProductId,
        int QuantityOfProducts,
        string Street,
        decimal Total);
}
