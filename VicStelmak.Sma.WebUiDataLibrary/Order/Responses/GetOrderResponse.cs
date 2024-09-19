namespace VicStelmak.Sma.WebUiDataLibrary.Order.Responses
{
    public record GetOrderResponse(
        int Id,
        int Apartment,
        string Building,
        string City,
        DateTime CreatedAt,
        string CreatedBy,
        string OrderCode,
        string PostalCode,
        int QuantityOfProducts,
        string Status,
        string Street,
        decimal Total,
        DateTime? UpdatedAt,
        string? UpdatedBy);
}
