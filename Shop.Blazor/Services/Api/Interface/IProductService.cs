using Shop.Common.Models.DTO.Product;

namespace Shop.Blazor.Services.Api.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAll();
        Task<ProductResponseDto> GetById(Guid id);
        Task<ProductResponseDto> Create(ProductRequestDto productDto);
        Task<ProductResponseDto> Update(Guid id, ProductRequestDto productDto);
        Task<bool> Delete(Guid id);
    }
}
