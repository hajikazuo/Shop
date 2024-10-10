using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using Shop.Api.Repositories.Interfaces;
using Shop.Common.Models.DTO.Product;
using Shop.Common.Models.Entities;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICombProvider _comb;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ICombProvider comb, IMapper mapper)
        {
            _productRepository = productRepository;
            _comb = comb;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetProductsAsync();

            var response = _mapper.Map<List<ProductDto>>(products);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProductDto>(existingProduct);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDto request)
        {
            var product = _mapper.Map<Product>(request);
            product.ProductId = _comb.Create();

            await _productRepository.CreateProductAsync(product);

            var response = _mapper.Map<ProductDto>(product);

            return Ok(response);
        }
    }
}
