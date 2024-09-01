namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record SendOrderSubmittingEventRequest(
        int Apartment,
        string Building,
        string City,
        string CreatedBy,
        string PostalCode,
        int ProductId,
        int QuantityOfProducts,
        string Street,
        decimal Total,
        bool? UserExists);
}
