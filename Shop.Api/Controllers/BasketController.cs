using Microsoft.AspNetCore.Mvc;
using Shop.Api.Services.Interface;
using Shop.Common.Models.DTO.Basket;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult GetBasket()
        {
            var basket = _basketService.GetBasket();
            return Ok(basket);
        }

        [HttpPost("add")]
        public IActionResult AddToBasket([FromBody] BasketDto request)
        {
            _basketService.AddToBasket(request.ProductId, request.Quantity);
            return NoContent();
        }

        [HttpDelete("remove/{productId}")]
        public IActionResult RemoveFromBasket(Guid productId, int quantity)
        {
            _basketService.RemoveFromBasket(productId, quantity);
            return NoContent();
        }
    }
}
