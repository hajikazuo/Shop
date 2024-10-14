using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using Shop.Blazor.Services.API.Interfaces;
using Shop.Common.Models.DTO.Auth;
using System.Net.Http;
using System.Net.Http.Json;

namespace Shop.Blazor.Services.API
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var httpClient = _httpClientFactory.CreateClient("ShopAPI");
            var response = await httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    await _localStorage.SetItemAsync("authToken", loginResponse.Token);
                }

                return loginResponse;
            }

            throw new Exception("Login failed");
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }
    }
}
