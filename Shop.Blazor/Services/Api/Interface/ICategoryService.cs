using Shop.Common.Models.DTO.Category;

namespace Shop.Blazor.Services.Api.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAll();
        Task<CategoryResponseDto> GetById(Guid id);
        Task<CategoryResponseDto> Create(CategoryRequestDto categoryDto);
        Task<CategoryResponseDto> Update(Guid id, CategoryRequestDto categoryDto);
        Task<bool> Delete(Guid id);
    }
}
