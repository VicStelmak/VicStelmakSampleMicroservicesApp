using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VicStelmak.Sma.WebUiDataLibrary.Product.Dtos;

namespace VicStelmak.Sma.WebUiDataLibrary.Product
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task DeleteProductAsync(int productId)
        {
            var apiResponse = await _httpClient.DeleteAsync($"api/products/{productId}");
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync("api/products", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");
        }

        public async Task<List<ProductDto>> GetProductsListAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products");
        }

        public async Task UpdateProductAsync(int productId, UpdateProductDto productDto)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PutAsync($"api/products/{productId}", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }
    }
}
