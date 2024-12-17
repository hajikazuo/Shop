using Shop.Common.Models.Entities;

namespace Shop.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Guid userId, Basket basket, Guid orderId);
    }
}
