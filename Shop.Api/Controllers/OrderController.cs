using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using Shop.Api.Repositories.Interfaces;
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
        private readonly IOrderRepository _orderRepository;

        public OrderController(ICombProvider comb, IBasketService basketService,IOrderRepository orderRepository, AppDbContext context)
        {
            _comb = comb;
            _basketService = basketService;
            _orderRepository = orderRepository;   
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Guid UserId)
        {
            var basket = _basketService.GetBasket();

            if (basket.Items.Count == 0)
            {
                return BadRequest("Basket is empty");
            }

            var orderId = _comb.Create();                   

            var order = await _orderRepository.CreateOrderAsync(UserId, basket, orderId);
            _basketService.ClearBasket();

            return Ok(order);
        }
    }
}
