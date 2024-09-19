namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record CreateLineItemRequest(
        int OrderId,
        string OrderUpdatedBy,
        int ProductId,
        int Quantity,
        decimal TotalPrice);
}
