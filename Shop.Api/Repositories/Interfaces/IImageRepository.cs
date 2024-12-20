using Shop.Common.Models.Entities;

namespace Shop.Api.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAll();
        Task<Image> Upload(IFormFile file, Image image);
    }
}
