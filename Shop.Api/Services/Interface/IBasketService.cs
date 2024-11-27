using Shop.Common.Models.Entities;

namespace Shop.Api.Services.Interface
{
    public interface IBasketService
    {
        Basket GetBasket();
        void AddToBasket(Guid productId, int quantity);
        void RemoveFromBasket(Guid productId);
    }
}
