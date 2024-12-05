using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using Shop.Api.Repositories.Interfaces;
using Shop.Common.Models.DTO.Category;
using Shop.Common.Models.Entities;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICombProvider _comb;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, ICombProvider comb, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _comb = comb;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            var response = _mapper.Map<List<CategoryResponseDto>>(categories);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CategoryResponseDto>(existingCategory);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
        {
            var category = _mapper.Map<Category>(request);
            category.CategoryId = _comb.Create();

            await _categoryRepository.CreateCategoryAsync(category);

            var response = _mapper.Map<CategoryResponseDto>(category); 
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, CategoryRequestDto request)
        {
            var category = _mapper.Map<Category>(request); 
            category.CategoryId = id;

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CategoryResponseDto>(updatedCategory); 
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<CategoryResponseDto>(category);

            return Ok(response);
        }

    }
}
