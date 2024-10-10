using Microsoft.EntityFrameworkCore;
using Shop.Api.Repositories.Interfaces;
using Shop.Common.Context;
using Shop.Common.Models.Entities;

namespace Shop.Api.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == product.ProductId);

            if (existingProduct is null)
            {
                return null;
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<Product> DeleteProductAsync(Guid id)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (existingProduct is null)
            {
                return null;
            }

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}
