using Shop.Common.Models.DTO.Category;

namespace Shop.Blazor.Services.API.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);

    }
}
