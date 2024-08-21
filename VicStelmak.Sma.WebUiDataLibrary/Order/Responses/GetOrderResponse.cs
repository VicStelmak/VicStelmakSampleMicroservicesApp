namespace VicStelmak.Sma.WebUiDataLibrary.Order.Responses
{
    public record GetOrderResponse(
         DateTime CreatedAt,
         string CreatedBy,
         string OrderCode,
         int QuantityOfProducts,
         string Status,
         decimal Total,
         DateTime? UpdatedAt,
         string? UpdatedBy);
}
