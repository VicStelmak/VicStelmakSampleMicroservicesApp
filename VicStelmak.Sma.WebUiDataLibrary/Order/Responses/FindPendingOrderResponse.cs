namespace VicStelmak.Sma.WebUiDataLibrary.Order.Responses
{
    public record FindPendingOrderResponse(int OrderId, List<int>? ProductsIds, int QuantityOfProducts, decimal Total);
}
