using Shop.Common.Models.DTO.Category;

namespace Shop.Blazor.Services.Api.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetCategories();
        Task<CategoryResponseDto> GetCategory(Guid id);
        Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryDto);
        Task<CategoryResponseDto> UpdateCategory(Guid id, CategoryRequestDto categoryDto);
    }
}
