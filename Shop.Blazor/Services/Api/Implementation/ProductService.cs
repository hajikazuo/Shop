using Shop.Blazor.Services.Api.Interface;
using Shop.Common.Models.DTO.Category;
using Shop.Common.Models.DTO.Product;
using Shop.Common.Models.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Shop.Blazor.Services.Api.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<ProductService> _logger;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;

        private ProductResponseDto? product;
        private IEnumerable<ProductResponseDto>? products;

        public ProductService(IHttpClientFactory httpClientFactory,
            ILogger<ProductService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ProductResponseDto>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");
            try
            {            
                return await httpClient.GetFromJsonAsync<List<ProductResponseDto>>(apiEndpoint);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao acessar: {apiEndpoint} " + ex.Message);
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<ProductResponseDto> GetById(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");
            var response = await httpClient.GetAsync(apiEndpoint + id);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro ao obter pelo id= {id} - {message}");
                throw new Exception($"Status Code : {response.StatusCode} - {message}");
            }

            return await response.Content.ReadFromJsonAsync<ProductResponseDto>();
        }

        public async Task<ProductResponseDto> Create(ProductRequestDto productDto)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");
            var content = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ProductResponseDto>(apiResponse, _options);
        }


        public async Task<ProductResponseDto> Update(Guid id, ProductRequestDto productDto)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");   

            var response = await httpClient.PutAsJsonAsync(apiEndpoint + id, productDto);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ProductResponseDto>(apiResponse, _options);
        }

        public async Task<bool> Delete(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");
            var response = await httpClient.DeleteAsync(apiEndpoint + id);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                return false;
            }

            return true;
        }
    }
}
