using Microsoft.EntityFrameworkCore;
using Shop.Api.Repositories.Interfaces;
using Shop.Common.Context;
using Shop.Common.Models.Entities;

namespace Shop.Api.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if (existingCategory is null)
            {
                return null;
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<Category> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existingCategory is null)
            {
                return null;
            }

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}
