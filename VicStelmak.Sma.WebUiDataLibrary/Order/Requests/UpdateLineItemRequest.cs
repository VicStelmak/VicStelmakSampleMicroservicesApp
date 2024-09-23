namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record UpdateLineItemRequest(
       string OrderCode,
       int ProductId,
       int Quantity,
       decimal TotalPrice,
       string UpdatedBy);
}
