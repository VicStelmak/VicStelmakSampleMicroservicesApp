namespace VicStelmak.Sma.WebUiDataLibrary.Order.Responses
{
    public record FindOrderResponse(int OrderId, List<int>? ProductsIds, int QuantityOfProducts);
}
