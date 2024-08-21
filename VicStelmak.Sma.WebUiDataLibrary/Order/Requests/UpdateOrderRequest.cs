namespace VicStelmak.Sma.WebUiDataLibrary.Order.Requests
{
    public record UpdateOrderRequest(int QuantityOfProducts, string Status, decimal Total, string UpdatedBy);
}
