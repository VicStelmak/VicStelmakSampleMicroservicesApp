using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VicStelmak.Sma.WebUiDataLibrary.Identity.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Identity.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public IdentityService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorage = localStorage;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddRoleToUserAsync(string roleName, string userId)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(roleName), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync($"api/users/roles/{userId}", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task<bool> CheckIfUserExistsByEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/users/email?email={email}");
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync("api/users", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                return JsonSerializer.Deserialize<CreateUserResponse>(apiResponseAsString, _options);
            }

            return new CreateUserResponse(true, null);
        }

        public async Task DeleteRolesFromUserAsync(string userId, IEnumerable<string> roles)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://localhost:7093/api/users/roles/{userId}"),
                Content = new StringContent(JsonSerializer.Serialize(roles), Encoding.UTF8, "application/json")
            };

            var apiResponse = await _httpClient.SendAsync(request);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var apiResponse = await _httpClient.DeleteAsync($"api/users/{userId}");
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }

        public async Task<List<GetUserResponse>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GetUserResponse>>("api/users");
        }

        public async Task<GetUserResponse> GetUserByIdAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<GetUserResponse>($"api/users/{userId}");
        }

        public async Task<LogInResponse> LogInAsync(LogInRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var authenticationResult = await _httpClient.PostAsync("api/users/logins", jsonContent);
            var authenticationResultAsString = await authenticationResult.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<LogInResponse>(authenticationResultAsString, _options);

            if (authenticationResult.IsSuccessStatusCode == false)
            {
                return new LogInResponse(apiResponse.ErrorMessage, null, false);
            }

            await _localStorage.SetItemAsync("authenticationToken", apiResponse.Jwt);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(apiResponse.Jwt);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", apiResponse.Jwt);

            return new LogInResponse(null, null, true);
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authenticationToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PutAsync($"api/users/{userId}", jsonContent);
            var apiResponseAsString = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode == false)
            {
                throw new ArgumentException(apiResponseAsString);
            }
        }
    }
}
