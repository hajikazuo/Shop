using Newtonsoft.Json;
using Shop.Api.Repositories.Interfaces;
using Shop.Api.Services.Interface;
using Shop.Common.Models.Entities;

namespace Shop.Api.Services.Implementation
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public Basket GetBasket()
        {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["basket"];
            if (string.IsNullOrEmpty(cookie))
            {
                return new Basket();
            }

            var basket = JsonConvert.DeserializeObject<Basket>(cookie);
            return basket ?? new Basket();
        }

        public void AddToBasket(Guid productId, int quantity)
        {
            var basket = GetBasket();

            var product = _productRepository.GetProductByIdAsync(productId).Result;

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var existingItem = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                basket.Items.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                });
            }

            var basketJson = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", basketJson, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });
        }

        public void RemoveFromBasket(Guid productId, int quantity)
        {
            var basket = GetBasket();
            var item = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (item != null)
            {
                item.Quantity -= quantity;

                if (item.Quantity <= 0)
                {
                    basket.Items.Remove(item);
                }

                var basketJson = JsonConvert.SerializeObject(basket);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", basketJson, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                });
            }

        }

        public void ClearBasket()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(new Basket()), new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7) 
            });
        }
    }
}
