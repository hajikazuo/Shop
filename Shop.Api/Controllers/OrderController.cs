using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using Shop.Api.Services.Interface;
using Shop.Common.Context;
using Shop.Common.Models.DTO.Product;
using Shop.Common.Models.Entities;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICombProvider _comb;
        private readonly IBasketService _basketService;
        private readonly AppDbContext _context;

        public OrderController(ICombProvider comb, IBasketService basketService, AppDbContext context)
        {
            _comb = comb;
            _basketService = basketService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Guid UserId)
        {
            var order = new Order
            {
                OrderId = _comb.Create(),
                UserId = UserId,
                OrderDate = DateTime.Now,
            };

            var basket = _basketService.GetBasket();
            

            foreach(var item in basket.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = basket.Total;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            _basketService.ClearBasket();

            return Ok(order);
        }
    }
}
