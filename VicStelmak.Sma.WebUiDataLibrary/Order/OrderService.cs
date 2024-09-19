using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using VicStelmak.Sma.WebUiDataLibrary.Order.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Order.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Order
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateLineItemAsync(CreateLineItemRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync("api/orders/line-items", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync("api/orders", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task<bool> CheckIfPendingOrderExistsAsync(string userEmail)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/orders/order?userEmail={userEmail}");
        }

        public async Task DeleteOrderAsync(string deletedBy, int orderId)
        {
            var apiResponse = await _httpClient.DeleteAsync($"api/orders?deletedBy={deletedBy}&orderId={orderId}");
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task<List<GetOrderResponse>> FindOrdersByUserEmailAsync(string userEmail)
        {
            return await _httpClient.GetFromJsonAsync<List<GetOrderResponse>>($"api/orders/created-by?userEmail={userEmail}");
        }

        public async Task<FindPendingOrderResponse> FindPendingOrderByUserEmailAsync(string userEmail)
        {
            return await _httpClient.GetFromJsonAsync<FindPendingOrderResponse>($"api/orders/order/created-by?userEmail={userEmail}");
        }

        public async Task<List<GetLineItemsResponse>> GetLineItemsByOrderIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<List<GetLineItemsResponse>>($"api/orders/line-items?orderId={orderId}");
        }

        public async Task<GetOrderResponse> GetOrderByIdAsync(int orderId)
        {
            return await _httpClient.GetFromJsonAsync<GetOrderResponse>($"api/orders/id?orderId={orderId}");
        }

        public async Task<List<GetOrderResponse>> GetOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetOrderResponse>>("api/orders");
        }

        public async Task SendOrderSubmittingEventAsync(SendOrderSubmittingEventRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync("api/orders/events", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task UpdateOrderAsync(int orderId, UpdateOrderRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PutAsync($"api/orders/{orderId}", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }
    }
}
