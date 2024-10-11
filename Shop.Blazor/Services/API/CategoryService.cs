using Shop.Blazor.Services.API.Interfaces;
using Shop.Common.Models.DTO.Category;
using System.Net.Http.Json;

namespace Shop.Blazor.Services.API
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CategoryService> _logger;
        private const string apiEndpoint = "/api/categories/";

        private CategoryDto? category;
        private IEnumerable<CategoryDto>? categories;

        public CategoryService(IHttpClientFactory httpClientFactory, ILogger<CategoryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ShopAPI");
                var response = await httpClient.GetFromJsonAsync<List<CategoryDto>>(apiEndpoint);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching categories from API: {apiEndpoint}" + ex.Message);
                throw new UnauthorizedAccessException();
            }
        }

        public Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
