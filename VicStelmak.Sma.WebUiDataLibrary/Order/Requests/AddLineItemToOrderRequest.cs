namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record AddLineItemToOrderRequest(
        int OrderId,
        string OrderUpdatedBy,
        int ProductId,
        int Quantity);
}
