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

        public async Task AddLineItemToOrderAsync(AddLineItemToOrderRequest request)
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
            return await _httpClient.GetFromJsonAsync<bool>($"api/orders/exists?userEmail={userEmail}");
        }

        public async Task<FindPendingOrderResponse> FindPendingOrderByUserEmailAsync(string userEmail)
        {
            return await _httpClient.GetFromJsonAsync<FindPendingOrderResponse>($"api/orders/created-by?userEmail={userEmail}");
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
