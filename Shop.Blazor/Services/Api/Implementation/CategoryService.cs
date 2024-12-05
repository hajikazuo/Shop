using Shop.Blazor.Services.Api.Interface;
using Shop.Common.Models.DTO.Category;
using Shop.Common.Models.Entities;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net;
using System;

namespace Shop.Blazor.Services.Api.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CategoryService> _logger;
        private const string apiEndpoint = "/api/categories/";
        private readonly JsonSerializerOptions _options;

        private CategoryResponseDto? category;
        private IEnumerable<CategoryResponseDto>? categories;

        public CategoryService(IHttpClientFactory httpClientFactory,
            ILogger<CategoryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<CategoryResponseDto>> GetAll()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("Shop.Api");
                var result = await httpClient.GetFromJsonAsync<List<CategoryResponseDto>>(apiEndpoint);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao acessar categorias: {apiEndpoint} " + ex.Message);
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<CategoryResponseDto> GetById(Guid id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("Shop.Api");
                var response = await httpClient.GetAsync(apiEndpoint + id);

                if (response.IsSuccessStatusCode)
                {
                    category = await response.Content.ReadFromJsonAsync<CategoryResponseDto>();
                    return category;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Erro ao obter a categoria pelo id= {id} - {message}");
                    throw new Exception($"Status Code : {response.StatusCode} - {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a categoria pelo id={id} \n\n {ex.Message} ");
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<CategoryResponseDto> Create(CategoryRequestDto categoryDto)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");
            StringContent content = new StringContent(JsonSerializer.Serialize(categoryDto),
                                                 Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    category = await JsonSerializer
                               .DeserializeAsync<CategoryResponseDto>(apiResponse, _options);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    return null;
                }
            }

            return category;

        }

        public async Task<CategoryResponseDto> Update(Guid id, CategoryRequestDto categoryDto)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");

            CategoryResponseDto category = new CategoryResponseDto();

            using(var response = await httpClient.PutAsJsonAsync(apiEndpoint + id, categoryDto))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    category = await JsonSerializer.DeserializeAsync<CategoryResponseDto>(apiResponse, _options);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                return category;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("Shop.Api");

            using (var response = await httpClient.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            return false;
        }
    }
}
