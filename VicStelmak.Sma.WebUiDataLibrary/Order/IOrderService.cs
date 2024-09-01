using VicStelmak.Sma.WebUiDataLibrary.Order.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Order.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Order
{
    public interface IOrderService
    {
        Task AddLineItemToOrderAsync(AddLineItemToOrderRequest request);
        Task<bool> CheckIfPendingOrderExistsAsync(string userEmail);
        Task CreateOrderAsync(CreateOrderRequest request);
        Task<FindPendingOrderResponse> FindPendingOrderByUserEmailAsync(string userEmail);
        Task SendOrderSubmittingEventAsync(SendOrderSubmittingEventRequest request);
        Task UpdateOrderAsync(int orderId, UpdateOrderRequest request);
    }
}