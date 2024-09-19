namespace VicStelmak.Sma.WebUiDataLibrary.Order.Responses
{
    public record GetLineItemsResponse(int OrderId, int ProductId, int Quantity, decimal TotalPrice);
}
